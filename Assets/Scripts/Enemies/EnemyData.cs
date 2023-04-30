using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    [SerializeField]private float _health;
    public int _gold;
    public float _speed;
    public delegate void diedEvent(int gold);

    public event diedEvent _diedEvent;

    public void SetHealth(float health)
    {
        _health = health;

        if (_health <= 0)
        {
            _diedEvent?.Invoke(_gold);
        }
    }

    public float GetHealth()
    {
        return _health;
    }
}