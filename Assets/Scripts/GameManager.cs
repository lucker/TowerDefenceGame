using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    WaitingWaveSpawn,
    RoundStarted
}

public class GameManager : MonoBehaviour
{
    private const int _lives = 20;

    [SerializeField] public Transform _startPoint;
    [SerializeField] public Transform _endPoint;
    [SerializeField] public Transform[] _movemantPoints;
    [SerializeField] private TextMeshProUGUI _textForGold;
    [SerializeField] private TextMeshProUGUI _textForEnemies;
    [SerializeField] private TextMeshProUGUI _textForEscapedEnemies;
    private static GameManager _instance;

    public delegate void GoldChanged(int gold);
    public event GoldChanged _notifyGoldChanged;

    public delegate void EnemyEscaped(int enemies);
    public event EnemyEscaped _notifyEnemyEscaped;


    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _bullets = new List<GameObject>();
    private int _gold = 0;
    private int _escapedEnemy = 0;
    //private int _diedEnemies = 0;
    private GameState _gameState = GameState.WaitingWaveSpawn;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _notifyGoldChanged += ShowGoldInGUI;
        _notifyEnemyEscaped += ShowEnemyEscapedInGUI;
        _notifyEnemyEscaped += GameOver;
        
        AddGold(100);
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public List<GameObject> GetEnemies()
    {
        return _enemies;
    }

    public void AddGold(int gold)
    {

        _gold += gold;
        _notifyGoldChanged?.Invoke(gold);
        Debug.Log("GameManager " + _gold);
    }

    private void ShowGoldInGUI(int gold)
    {
        _textForGold.text = _gold.ToString("0");
    }

    public void IncreaseEscapedEnemy()
    {
        _escapedEnemy++;
        _notifyEnemyEscaped?.Invoke(_escapedEnemy);
    }

    private void GameOver(int enemies)
    {
        if (_escapedEnemy >= _lives)
        {
            //SceneManager.LoadScene("GameOver");
        }
    }

    private void ShowEnemyEscapedInGUI(int enemies)
    {
        _textForEscapedEnemies.text = (_lives - _escapedEnemy).ToString("0");
    }

    public void EnemyDied()
    {
        
    }

    public int GetGold()
    {
        return _gold;
    }

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public GameState GetGameState()
    {
        return _gameState;
    }
}
