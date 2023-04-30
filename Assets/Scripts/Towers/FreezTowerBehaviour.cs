using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreezTowerBehaviour : BaseTowerBehaviour
{
    const float SPEED = 0.1f;
    const float LIFETIME = 15f;
    //private Dictionary<int, bool> _alreadyAtacked = new Dictionary<int, bool>();

    private FreezTowerBehaviour()
    {
        _atackSpeed = 0f;
    }

    private void Start()
    {
        GameManager gameManager = GameManager.GetInstance();
        List<GameObject> enemy = gameManager.GetEnemies();
    }

    private void FixedUpdate()
    {
        GameManager gameManager = GameManager.GetInstance();
        List<GameObject> enemy = gameManager.GetEnemies();

        for (int i = 0; i < enemy.Count; i++)
        {
            if (enemy[i] == null)
            {
                continue;
            }

            var pos = enemy[i].transform.position;
            EnemyBehaviour enemyBehaviour = enemy[i].GetComponent<EnemyBehaviour>();
            //Debug.Log("Magnitued: " + (enemy[i].transform.position - pos).magnitude + "range: " + _range);
            if ((enemy[i].transform.position - gameObject.transform.position).magnitude <= _range)
            {
                enemyBehaviour._hasSlowEffacte = true;
            }
        }
    }

    override public float GetAtack()
    {
        return 0;
    }

    protected override void Atack(int enemyIndex)
    {
    }

    protected override void Idle()
    {
    }
}

