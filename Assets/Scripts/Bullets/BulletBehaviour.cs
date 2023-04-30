using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private int _enemyIndex = 0;
    [SerializeField] private BulletData _bulletData;

    private void Awake()
    {
        //_bulletData = new BulletData(10f, 5f);
        //Debug.Log("BULLET AWAKE: " + _bulletData.GetBulletAtack());
    }

    public void SetSpeed(float speed)
    {
        _bulletData._speed = speed;
    }

    public void SetEnemy(int enemyIndex)
    {
        _enemyIndex = enemyIndex;
    }

    public BulletData GetBulletData()
    {
        return _bulletData;
    }

    public void SetBulletData()
    {
        _bulletData = new BulletData(10f, 5f);
    }

    private void FixedUpdate()
    {
        GameManager gameManager = GameManager.GetInstance();
        GameObject enemy = gameManager.GetEnemies()[_enemyIndex];

        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();

        if (enemyBehaviour.GetState() == EnemyStates.Dead)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 movementVector = enemy.transform.position - transform.position;

        /*Debug.Log("Bullet position: " + transform.position + "movemant point " + movementVector);
        Debug.Log("movespeed: " + Time.fixedDeltaTime * _bulletData._speed);
        Debug.Log("distance:" + movementVector.magnitude);*/

        if (movementVector.magnitude >= Time.fixedDeltaTime * _bulletData._speed)
        {
            transform.position += Time.fixedDeltaTime * _bulletData._speed * movementVector.normalized;
        }
        else
        {
            float health = enemyBehaviour.GetEnemyData().GetHealth();
            int gold = enemyBehaviour.GetEnemyData()._gold;
            enemyBehaviour.GetEnemyData().SetHealth(health - _bulletData.GetBulletAtack());
            //Debug.Log("Health :" + health + " index: " + _enemyIndex);

            Destroy(gameObject);
        }
    }
}
