using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Wave
{
    public string _waveName;
    public List<EnemyGroup> _enemyGroups; // a list of groups of enemies to spawn in this wave
    public int _waveQuota = 10; // total number of the enemies ti spawn in this wave
    public float _spawnInterval = 3f; //the interval at which to spawn enemies
    public int _spawnCount; //the number of enemies already spawned in this wave
}

[System.Serializable]
public class EnemyGroup
{
    public string _enemyName;
    public int _enemyCount; // the number of enemies to spawn in this wave
    public float _spawnCount; // the number of enemies of this type already spawned in this wave
    public GameObject _enemyPrefab;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Endless Mode Enemies")]
    [SerializeField] GameObject[] _enemyPrefabs;

    public List<Wave> _waves; //a list of all the waves in the game

    [SerializeField] Transform _player;
    [SerializeField] float _endlessRate;

    // Spawn Map Config
    [Header("Map Configuration")]
    [SerializeField] float _mapBorder = 9.5f; // Adjust this value based on your map size.
    [SerializeField] float _minDistanceFromPlayer = 3.0f; // Minimum distance from the player.

    [Header("Game Mode is")]
    private bool _ModeValue;
    bool _endlessMode = false;
    bool _waveMode = false;

    [Header("Spawner Attributes")]
    public int _enemiesAlive;
    public int _maxEnemiesAllowed; //the maximum number of enemies allowed on the map at once
    public bool _maxEnemiesReached; // a flag indicating if the maximum number of enemis has been reached
    public float _waveInterval; //the interval between each wave
    public int _currentWaveCount; // the index of the current wave

    float _spawnTimer; //timer use to determine when to spawn next enemy
    public float _enemiesSpawnedInWave;
    Vector2 _spawnLocation;
    GameObject spawnPointMarker;
    [SerializeField] GameObject _spawnPointMarkerPrefab;

    private bool waveEndFlag;
    [SerializeField]
    private GameObject pausePanel;

    // Singleton
    public static EnemySpawner Instance { get; private set; }

    // Events
    public event EventHandler OnWavePassed;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        CalculateWaveQuota();

        //Debug.Log(GameManager.Instance.endlessMode);

        if (GameManager.Instance.endlessMode)
        {
            _endlessMode = true;
            _waveMode = false;
        }
        else
        {
            _waveMode = true;
            _endlessMode = false;
        }

    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {

        }
        if (_endlessMode)
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }

            //StartCoroutine(EndlessWave());
            _spawnTimer += Time.deltaTime;

            //check if its time to spawn the next enemy
            if (_spawnTimer >= _endlessRate)
            {
                _spawnTimer = 0f;
                SpawnEndless();
            }
        }
        else if (_waveMode)
        {
            if (_currentWaveCount < _waves.Count)
            {
                if (Input.GetKeyUp(KeyCode.P))
                {
                    pausePanel.SetActive(true);
                    Time.timeScale = 0;
                }

                // Spawning Mobs
                _spawnTimer += Time.deltaTime;

                //check if its time to spawn the next enemy
                if (_spawnTimer >= _waves[_currentWaveCount]._spawnInterval)
                {
                    //Debug.Log("enemy spawned");
                    _spawnTimer = 0f;
                    StartSpawnEnemies();
                }

                // Wave End Check
                if (_enemiesAlive == 0 && _waves[_currentWaveCount]._waveQuota == _enemiesSpawnedInWave && !waveEndFlag)
                {
                    waveEndFlag = true;

                    Debug.Log("wave check ");

                    OnWavePassed?.Invoke(this, EventArgs.Empty);
                    StartCoroutine(StartWave());
                }

            }
            else
            {
                Debug.Log("game  end ");
                // All waves are completed, you can handle game victory here.
                // Probably an event to GameManager on all waves completed
                SceneManager.LoadScene("Game End");
            }
        }
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_waveInterval);

        if (_currentWaveCount < _waves.Count )
        {
            _currentWaveCount++;    // Index the next wave
            CalculateWaveQuota();   // Calculate mob limit
            //StartSpawnEnemies();
        }

        waveEndFlag = false;
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
        {
            currentWaveQuota += enemyGroup._enemyCount;
        }
        _waves[_currentWaveCount]._waveQuota = currentWaveQuota;
        //Debug.LogWarning(currentWaveQuota);
    }

    void StartSpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }


    void SpawnEndless() // Missing Mob Scaling
    {
        int rand = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        GameObject enemyToSpawn = _enemyPrefabs[rand];
        _spawnLocation = CalculateSpawnPosition();
        //MarkPoint(_spawnLocation);
        Instantiate(enemyToSpawn, _spawnLocation, Quaternion.identity);
    }

    IEnumerator SpawnEnemiesCoroutine() // Missing Mob Scaling
    {
        //check if the minimum number of enemies in the wave have been spawned
        if (_waves[_currentWaveCount]._spawnCount < _waves[_currentWaveCount]._waveQuota && !_maxEnemiesReached)
        {
            //spawn each type of enemies untill the quota is filled
            foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
            {

                // check if the minumum number of the enemies of this type has been spawned
                if (enemyGroup._spawnCount < enemyGroup._enemyCount)
                {
                    //limit the number of enemies that can be spawned
                    if (_enemiesAlive >= _maxEnemiesAllowed)
                    {
                        _maxEnemiesReached = true;
                        break;
                    }
                    //_spawnLocation = new Vector2(Random.Range(-9.5f, 9.5f), Random.Range(-9.5f, 9.5f));
                    Vector2 _spawnLocation = CalculateSpawnPosition();

                    //StartCoroutine(MarkPoint(_spawnLocation));

                    Debug.Log("Before Instantiate ");
                    spawnPointMarker = Instantiate(_spawnPointMarkerPrefab, _spawnLocation, Quaternion.identity);

                    float spawntimer = 0f;
                    float time = 1f;
                    while (spawntimer < time)
                    {

                        spawntimer += Time.deltaTime;
                        yield return null;
                    }

                    Debug.Log("Before Destroy ");
                    if (spawnPointMarker != null)
                    {
                        Destroy(spawnPointMarker);
                    }


                    //Debug.Log("bu log kac saniyede bir calisiyor");
                    Instantiate(enemyGroup._enemyPrefab, _spawnLocation, Quaternion.identity);

                    enemyGroup._spawnCount++;
                    _waves[_currentWaveCount]._spawnCount++;
                    _enemiesAlive++;
                    _enemiesSpawnedInWave++;
                }
            }
        }
        // reset the _maxEnemiesReached flag if the number alive has dropped below the maximum amount
        if (_enemiesAlive < _maxEnemiesAllowed)
        {
            _maxEnemiesReached = false;
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 spawnPosition;
        do
        {
            float randomX = UnityEngine.Random.Range(-_mapBorder, _mapBorder);
            float randomY = UnityEngine.Random.Range(-_mapBorder, _mapBorder);

            spawnPosition = new Vector2(randomX, randomY);
        } while (Vector2.Distance(spawnPosition, _player.position) < _minDistanceFromPlayer);

        

        return spawnPosition;
    }

    //call this function when enemey is killed
    public void EnemyKilled()
    {
        //decrement the number of enemies alive
        _enemiesAlive--;
        //Debug.Log(_enemiesAlive);
    }

    //IEnumerator MarkPoint(Vector2 spawnPosition)
    //{
    //    Debug.Log("Before Instantiate Marker");
    //    spawnPointMarker = Instantiate(_spawnPointMarkerPrefab, spawnPosition, Quaternion.identity);
    //    Debug.Log("After Instantiate Marker");

    //    // Delayed destruction of the spawn point marker
    //    //yield return new WaitForSecondsRealtime(2); // Subtracting 2 seconds to align with the enemy spawn delay

    //    float spawntimer = 0f;
    //    float time = 2f;
    //    while (spawntimer < time)
    //    {
            
    //        spawntimer += Time.deltaTime;
    //        yield return null;
    //    }

    //    Debug.Log("Before Destroy Marker");
    //    Destroy(spawnPointMarker.gameObject);
    //    Debug.Log("After Destroy Marker");
    //}

    public EnemySpawner(bool value)
    {
        _ModeValue = value;
    }

}