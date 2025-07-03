using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelSystemAnimated
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    LevelSystem _levelSystem;
    bool _isAnimating;

    float _updateTimer, _updateTimerMax;

    int _level;
    float _experience; 

    public int Level { get => _level; private set => _level = value; }
    public float Experience { get => _experience; private set => _experience = value; }

    public LevelSystemAnimated(LevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);

        _updateTimerMax = .016f; // 60 FPS

        FunctionUpdater.Create(() => Update());
    }

    private void SetLevelSystem(LevelSystem levelSystem)
    {
        _levelSystem = levelSystem;

        _level = _levelSystem.Level;
        _experience = _levelSystem.Experience;

        _levelSystem.OnExperienceChanged += _levelSystem_OnExperienceChanged;
        _levelSystem.OnLevelChanged += _levelSystem_OnLevelChanged;
    }

    private void _levelSystem_OnExperienceChanged(object sender, EventArgs e)
    {
        _isAnimating = true;
    }
    private void _levelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        _isAnimating = true;
    }

    void Update()
    {
        if (_isAnimating)
        {
            _updateTimer += Time.deltaTime;
            while (_updateTimer > _updateTimerMax)
            {
                _updateTimer -= _updateTimerMax;
                UpdateAddExperience();
            }
        }
        //Debug.Log(Level +" "+ Experience);
    }

    void UpdateAddExperience()
    {
        if (_level < _levelSystem.Level)
        {
            // Local level under target level
            AddExperience();
        }
        else
        {
            // Local level equals the target level
            if (_experience < _levelSystem.Experience)
            {
                AddExperience();
            }
            else
            {
                _isAnimating = false;
            }
        }
    }

    void AddExperience()
    {
        _experience++;
        if(_experience >= _levelSystem.GetExperienceToNextLevel(Level))
        {
            _level++;
            _experience = 0;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetExperienceNormalized()
    {
        return (float)_experience / _levelSystem.GetExperienceToNextLevel(Level);
    }
}
