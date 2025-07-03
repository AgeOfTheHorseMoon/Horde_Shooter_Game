using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coruk.CharacterStats;

public class Test_StatPanel : MonoBehaviour
{
    [SerializeField] Test_StatDisplay[] _statDisplays;
    [SerializeField] string[] _statNames;

    private CharacterStat[] _stats;

    private void OnValidate()
    {
        _statDisplays = GetComponentsInChildren<Test_StatDisplay>();
        UpdateStatNames();
    }

    public void SetStats(params CharacterStat[] characterStats)
    {
        _stats = characterStats;

        if (_stats.Length > _statDisplays.Length)
        {
            Debug.LogError("Not enough Stat Displays!");
            return;
        }

        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].gameObject.SetActive(i < _stats.Length);

            if (i < _stats.Length)
            {
                _statDisplays[i].Stat = _stats[i];
            }
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            _statDisplays[i].UpdateStatValue();
        }
    }
    
    public void UpdateStatNames()
    {
        for (int i = 0; i < _statNames.Length; i++)
        {
            _statDisplays[i].Name = _statNames[i];
        }
    }
}
