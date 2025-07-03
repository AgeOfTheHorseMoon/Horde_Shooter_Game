using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem  
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    int _level;
    float _experience;

    // Might make it scalable with base experience * multiplier etc...
    static readonly int[] _experiencePerLevel = { 100, 150, 200, 250, 300, 350, 400, 450, 500 };

    public int Level { get => _level; private set => _level = value; }
    public float Experience { get => _experience; private set => _experience = value; }
    public bool IsMaxLevel { get => Level == _experiencePerLevel.Length - 1;}

    public LevelSystem()
    {
        Level = 0;
        _experience = 0;
    }

    public bool IMaxLevel(int level)
    {
        return level == _experiencePerLevel.Length - 1;
    }

    public void AddExperience(float amount)
    {
        if (!IsMaxLevel)
        {
            _experience += amount;
            while (_experience >= GetExperienceToNextLevel(Level))
            {
                _experience -= GetExperienceToNextLevel(Level);
                Level++;
                OnLevelChanged?.Invoke(this, EventArgs.Empty);
            }
            OnExperienceChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetExperienceNormalized()
    {
        if (IsMaxLevel)
        {
            return 1f;
        }
        else
        {
            return (float)_experience / GetExperienceToNextLevel(Level);
        }
    }

    public int GetExperienceToNextLevel(int level)
    {
        if (level < _experiencePerLevel.Length)
        {
            return _experiencePerLevel[level];
        }
        else
        {
            // Level invalid
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }
}
