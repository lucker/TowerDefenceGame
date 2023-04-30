using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseTowerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _towerTop;
    [SerializeField] protected Transform[] _bulletPoints;
    [SerializeField] protected GameObject _currentBulletPrefub;
    [SerializeField] protected UpgradeSystem _upgradeSystem;
    [SerializeField] protected int _towerLVL = 1;
    [SerializeField] protected float _atackSpeed = 0.1f;
    [SerializeField] protected float _range = 5.0f;
    protected float _time = 0;


    public GameObject GetCurrentBulletPrefub()
    {
        return _currentBulletPrefub;
    }

    public void SetCurrentBulletPrefub(GameObject bulletPrefub)
    {
        _currentBulletPrefub = bulletPrefub;
    }

    void FixedUpdate()
    {
        bool isAtacking = false;
        _time -= Time.fixedDeltaTime;
        GameManager gameManager = GameManager.GetInstance();
        List<GameObject> enemyList = gameManager.GetEnemies();
        for (int enemyIndex = 0; enemyIndex < enemyList.Count; enemyIndex++)
        {
            if (enemyList[enemyIndex] == null)
            {
                continue;
            }

            if (enemyList[enemyIndex].GetComponent<EnemyBehaviour>().GetState() == EnemyStates.Dead)
            {
                continue;
            }

            Vector3 distance = enemyList[enemyIndex].transform.position - transform.position;

            if (distance.magnitude <= _range && _time <= 0)
            {
                Vector3 lookRotation = Quaternion.LookRotation(distance).eulerAngles;
                _towerTop.transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
                Atack(enemyIndex);
                _time = _atackSpeed;
                isAtacking = true;
                break;
            }
        }

        //Debug.Log(_time);
        if (_time <= 0)
        {
            _time = _atackSpeed;
        }

        if (isAtacking == false)
        {
            Idle();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public float GetRange()
    {
        return _range;
    }

    virtual public float GetAtack()
    {
        return _currentBulletPrefub.GetComponent<BulletBehaviour>().GetBulletData().GetBulletAtack();
    }

    public void OnMouseDown()
    {
        var towerStats = TowerStatsUIBehaviour.GetInstance();
        towerStats.SetTower(gameObject);

        //Debug.Log("Mouse Down Tower");
    }

    virtual public BaseTowerBehaviour UpgradeTower()
    {
        GameManager gameManager = GameManager.GetInstance();

        if (gameManager.GetGold() >= _upgradeSystem._cost)
        {
            GameObject tower = _upgradeSystem._tower;
            GameObject newTower = Instantiate(tower, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.AddGold(-_upgradeSystem._cost);

            return newTower.GetComponent<BaseTowerBehaviour>();
        }

        return null;
    }

    public bool CanBeUpgraded()
    {
        return _upgradeSystem._tower != null;
    }

    public int GetCostForUpgrade()
    {
        return _upgradeSystem._cost;
    }

    protected abstract void Atack(int enemyIndex);

    protected abstract void Idle();
}
