using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnTimerGUI;
    [SerializeField] private TextMeshProUGUI _spawnText;
    [SerializeField] private List<SpawnData> _enemiesSpawnOrder;

    private const float _spawnTimer = 1.0f;
    private const float _roundTimer = 60.0f;
    private float _timeCountDown = _spawnTimer;
    private float _roundTimeCountDown = _roundTimer;
    private int _waveNumber = 0;
    private bool _spawnEnded = false;
   
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.GetInstance();
        SetWaveNumber(0);
    }

    private void FixedUpdate()
    {
        if (_enemiesSpawnOrder.Count == _waveNumber)
        {
            //Debug.Log("No waves");
            return;
        }

        if (_gameManager.GetGameState() == GameState.WaitingWaveSpawn)
        {
            removeAllEnemiesBeforeNextRound();
            _spawnTimerGUI.text = _timeCountDown.ToString("0");
            _timeCountDown -= Time.deltaTime;

            if (_timeCountDown <= 0)
            {
                _timeCountDown = _spawnTimer;
                _gameManager.SetGameState(GameState.RoundStarted);
                StartCoroutine(SpawnEnemies());
            }
        }

        if (_gameManager.GetGameState() == GameState.RoundStarted)
        {
            _spawnTimerGUI.text = _roundTimeCountDown.ToString("0");
            _roundTimeCountDown -= Time.deltaTime;

            if (_roundTimeCountDown <= 0 || AllEnemiesDied())
            {
                _roundTimeCountDown = _roundTimer;
                _gameManager.SetGameState(GameState.WaitingWaveSpawn);
                SetWaveNumber(_waveNumber+1);
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        _spawnEnded = false;
        int numberOfEnemies = _enemiesSpawnOrder[_waveNumber]._enemyCount;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            var enemy = _enemiesSpawnOrder[_waveNumber];
            var enemyPrefub = enemy._enemyPrefub;
            GameObject enemyObject = (GameObject)Instantiate(enemyPrefub, _gameManager._startPoint.position, Quaternion.identity);

            var _enemies = _gameManager.GetEnemies();
            _enemies.Add(enemyObject);

            yield return new WaitForSeconds(_enemiesSpawnOrder[_waveNumber]._timeBetweenSpawnsEnemies);
        }

        _spawnEnded = true;
    }

    private bool AllEnemiesDied()
    {
        if (!_spawnEnded)
        {
            return false;
        }

        bool result = true;

        for (int i = 0; i < _gameManager.GetEnemies().Count; i++)
        {
            if (_gameManager.GetEnemies()[i] == null)
            {
                continue;
            }

            var eB = _gameManager.GetEnemies()[i].GetComponent<EnemyBehaviour>();

            if (eB.GetState() != EnemyStates.Dead)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private void removeAllEnemiesBeforeNextRound()
    {

        for (int i = 0; i < _gameManager.GetEnemies().Count; i++)
        {
            Destroy(_gameManager.GetEnemies()[i]);
        }
        _gameManager.GetEnemies().Clear();
    }

    public void SetWaveNumber(int waveNumber)
    {
        _waveNumber = waveNumber;
        _spawnText.text = (_waveNumber+1) + "/" + _enemiesSpawnOrder.Count;
    }
}

[System.Serializable]
public class SpawnData
{
    public GameObject _enemyPrefub;
    public int _enemyCount;
    public float _timeBetweenSpawnsEnemies;
}