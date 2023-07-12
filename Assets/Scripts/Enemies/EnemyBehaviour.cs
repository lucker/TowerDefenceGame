using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData;
    public bool _hasSlowEffacte = false;
    private GameManager _gameManager;
    private int _pointNumber = 0;
    private Animator _animator;
    private EnemyStates _state;
 

    private void Start()
    {
        _gameManager = GameManager.GetInstance();
        _pointNumber = 0;
        _state = EnemyStates.Alive;
        _enemyData._diedEvent += _gameManager.AddGold;
        _enemyData._diedEvent += SetDiedState;
    
        if (TryGetComponent<Animator>(out Animator animator))
        {
            _animator = animator;
        }
    }

    private void FixedUpdate()
    {
        Move();
        _hasSlowEffacte = false;
    }


    private void Move()
    {
        //Debug.Log("Enemy position: " + transform.position);
        if (_state == EnemyStates.Dead)
        {
            return;
        }

        if (_animator != null)
        {
            _animator.SetBool("walking", true);
        }

        Vector3 moveTo = _gameManager._movemantPoints[_pointNumber].transform.position - transform.position;
        //rotate enemy
        Quaternion lookQagternion = Quaternion.LookRotation(moveTo);
        Vector3 lookRotation = lookQagternion.eulerAngles;
        //transform.rotation = Quaternion.Euler(0f, lookRotation.y - 180, 0f);
        transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
        //Debug.Log(lookRotation);

        //moving
        Vector3 moveWithoutY = new Vector3(moveTo.normalized.x, 0, moveTo.normalized.z);

        if (_hasSlowEffacte)
        {
            transform.position += Time.fixedDeltaTime * moveWithoutY.normalized * (_enemyData._speed/2);
        }
        else
        {
            transform.position += Time.fixedDeltaTime * moveWithoutY.normalized * _enemyData._speed;
        }

        if (moveTo.magnitude <= 1f)
        {
            _pointNumber++;
        }

        if (_pointNumber == _gameManager._movemantPoints.Length)
        {
            _gameManager.IncreaseEscapedEnemy();
            Destroy(gameObject);
        }
    }

    public void SetDiedState(int _gold)
    {
        _state = EnemyStates.Dead;

        if (_animator != null)
        {
            _animator.SetBool("died", true);
            Debug.Log("deid animation");
        } else
        {
            Destroy(gameObject);
        }

        //Destroy(gameObject);
        //Debug.Log("DIED");
    }

    public void SetEnemyData(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }

    public EnemyData GetEnemyData()
    {
        return _enemyData;
    }

    public void SetSlowedState()
    {
        _state = EnemyStates.Slowed;
    }

    public void SetAlivedState()
    {
        _state = EnemyStates.Alive;
    }

    public EnemyStates GetState()
    {
        return _state;
    }
}

public enum EnemyStates
{
    Alive,
    Dead,
    Slowed
}
