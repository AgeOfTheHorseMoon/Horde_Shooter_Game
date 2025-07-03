using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_LevelBar : MonoBehaviour
{
    TextMeshProUGUI _levelText;
    Image _experienceBarImage;
    LevelSystem _levelSystem;
    LevelSystemAnimated _levelSystemAnimated;

    private void Awake()
    {
        _levelText = transform.Find("Level Text").GetComponent<TextMeshProUGUI>();
        _experienceBarImage = transform.Find("Bar Image").GetComponent<Image>();
    }

    void SetLevelNumber(int levelNumber)
    {
        _levelText.SetText("Level " + (levelNumber + 1)); // starts 0
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        _levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        // Set the LevelSystemAnimated object
        _levelSystemAnimated = levelSystemAnimated;
        
        // Update starting values
        SetLevelNumber(_levelSystemAnimated.Level);
        SetExperienceBarSize(_levelSystemAnimated.GetExperienceNormalized());

        // Subscribe to events
        _levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        _levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }
    
    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(_levelSystemAnimated.Level);
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(_levelSystemAnimated.GetExperienceNormalized());
    }
    void SetExperienceBarSize(float experienceNormalized)
    {
        _experienceBarImage.fillAmount = experienceNormalized;
    }
}
