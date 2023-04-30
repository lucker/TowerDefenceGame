using System;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    public float _speed;
    [SerializeField] private float _bulletAtack;
    //public delegate void ChangedAtack(float atack);
    //public event ChangedAtack _changedAtackNotify;

    public BulletData(float speed, float bulletAtack)
	{
        _speed = speed;
        _bulletAtack = bulletAtack;
    }

    public float GetBulletAtack()
    {
        return _bulletAtack;
    }
}

