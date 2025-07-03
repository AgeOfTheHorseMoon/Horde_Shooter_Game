using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using Coruk.CharacterStats;

public class Test_StatTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _statNameText;
    [SerializeField] TextMeshProUGUI _statModifiersLabelText;
    [SerializeField] TextMeshProUGUI _statModifiersText;

    private StringBuilder _stringBuilder = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName)
    {
        _statNameText.text = GetStatTopText(stat, statName);
        
        _statModifiersText.text = GetStatModifiersText(stat);
        
        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStat stat, string statName)
    {
        _stringBuilder.Length = 0;
        _stringBuilder.Append(statName);
        _stringBuilder.Append(" ");
        _stringBuilder.Append(stat.Value);

        if (stat.Value != stat.BaseValue)
        {
            _stringBuilder.Append(" (");
            _stringBuilder.Append(stat.BaseValue);

            if (stat.Value > stat.BaseValue)
            {
                _stringBuilder.Append("+");
            }
            
            _stringBuilder.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
            _stringBuilder.Append(")");
        }

        return _stringBuilder.ToString();
    }

    private string GetStatModifiersText(CharacterStat stat)
    {
        _stringBuilder.Length = 0;

        foreach (StatModifier modifier in stat.StatModifiers)
        {
            if (_stringBuilder.Length > 0) // if not the first stat, add line to tooltip
            {
                _stringBuilder.AppendLine(); 
            }

            if (modifier.Value > 0)
            {
                _stringBuilder.Append("+");
            }

            if (modifier.Type == StatModifierType.Flat)
            {

                _stringBuilder.Append(modifier.Value);
            }
            else
            {
                _stringBuilder.Append(modifier.Value);
                _stringBuilder.Append("%");
            }

            Test_EquippableItem item = modifier.Source as Test_EquippableItem; // if source variable is of type "EquippableItem" returns it, else assigns null to item

            if (item != null)
            {
                _stringBuilder.Append(" ");
                _stringBuilder.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifier is not an EquippableItem!");
            }
        }

        return _stringBuilder.ToString();
    }
}
