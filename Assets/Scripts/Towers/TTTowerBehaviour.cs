using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTowerBehaviour : BaseTowerBehaviour
{
    private TTTowerBehaviour()
    {
        _atackSpeed = 0.1f;
        _range = 5.0f;
        _time = 0;
    }

    protected override void Atack(int enemyIndex)
    {
        //Debug.Log("Atack");
        GameManager gameManager = GameManager.GetInstance();
        for (int i = 0; i < _bulletPoints.Length; i++)
        {
            GameObject bulletObject = Instantiate(
                _currentBulletPrefub,
                _bulletPoints[i].position,
                Quaternion.identity
            );

            if (bulletObject.TryGetComponent<BulletBehaviour>(out BulletBehaviour bullet))
            {
                //bullet.SetBulletData();
                bullet.SetEnemy(enemyIndex);
                //Debug.Log("BULLET ATACK: " + bullet.GetBulletData().GetBulletAtack());
            }
        }
    }

    protected override void Idle()
    {
        
    }
}