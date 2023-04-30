using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LazerTowerBehaviour : BaseTowerBehaviour
{
    private float _timeLazer = 0.0f;
    private float _lazerAtackSpeed = 0.1f;
    private float _lazerDamage = 10.0f;
    
    public LazerTowerBehaviour()
    {
        _atackSpeed = 0.0f;
    }

    protected override void Atack(int enemyIndex)
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        GameManager gameManager = GameManager.GetInstance();
        List<GameObject> enemyList = gameManager.GetEnemies();

        lineRenderer.SetPosition(0, _bulletPoints[0].transform.position);
        lineRenderer.SetPosition(1, enemyList[enemyIndex].transform.position);

        EnemyBehaviour enemyBehaviour = enemyList[enemyIndex].GetComponent<EnemyBehaviour>();

        if (_timeLazer <= 0)
        {
            float health = enemyBehaviour.GetEnemyData().GetHealth();
            int gold = enemyBehaviour.GetEnemyData()._gold;
            enemyBehaviour.GetEnemyData().SetHealth(health - _lazerDamage);
            _timeLazer = _lazerAtackSpeed;

            //Debug.Log("Lazer damage:" + _lazerDamage + "health: " + enemyBehaviour.GetEnemyData().GetHealth());
        }

        _timeLazer -= Time.fixedDeltaTime;
    }

    protected override void Idle()
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}