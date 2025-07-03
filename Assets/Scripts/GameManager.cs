using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool endlessMode;
    public static GameManager Instance{ get; private set; }

    public LevelSystem PlayerLevelSystem { get; private set; }
    public LevelSystemAnimated PlayerLevelSystemAnimated { get; private set; }
    
    [SerializeField] private UI_LevelBar _playerLevelBar;

    // Scaling
    public float DifficultyMultiplier { get; private set; } = 1; // Initialize with value

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Level System
        PlayerLevelSystem = new LevelSystem();
        PlayerLevelSystemAnimated = new LevelSystemAnimated(PlayerLevelSystem);
    }

    private void Start()
    {
        InitializeUILevelBar();

       EnemySpawner.Instance.OnWavePassed += UpgradeMultiplier;
    }

    public void UpgradeMultiplier(object sender, EventArgs eventArgs)
    {
        DifficultyMultiplier += 0.25f;
        Debug.Log("New Difficulty Multiplier : " + DifficultyMultiplier);
    }

    public void InitializeUILevelBar()
    {
        // Level UI Bar 
        _playerLevelBar.SetLevelSystem(PlayerLevelSystem);
        _playerLevelBar.SetLevelSystemAnimated(PlayerLevelSystemAnimated);
    }
}
