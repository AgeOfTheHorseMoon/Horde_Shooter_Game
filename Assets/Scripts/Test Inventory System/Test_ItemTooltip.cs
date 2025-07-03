using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Test_ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _itemNameText;
    [SerializeField] TextMeshProUGUI _itemSlotText;
    [SerializeField] TextMeshProUGUI _itemStatsText;

    private StringBuilder _stringBuilder = new StringBuilder();

    public void ShowTooltip(Test_EquippableItem item)
    {
        _itemNameText.text = item.ItemName;
        _itemSlotText.text = item.EquipmentType.ToString();

        _stringBuilder.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.SpeedBonus, "Speed");
        AddStat(item.HealthBonus, "Health");
        AddStat(item.DamageBonus, "Damage");
        
        AddStat(item.StrengthPercentBonus, "Strength", true);
        AddStat(item.SpeedPercentBonus, "Speed", true);
        AddStat(item.HealthPercentBonus, "Health", true);
        AddStat(item.DamagePercentBonus, "Damage", true);

        _itemStatsText.text = _stringBuilder.ToString();

        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.AppendLine();
            }

            if (value > 0)
            {
                _stringBuilder.Append("+");
            }

            _stringBuilder.Append(value);
            
            if (isPercent)
            {
                _stringBuilder.Append("% ");
                //_stringBuilder.Append(value);
            }
            else
            {
                _stringBuilder.Append(" ");
            }

            _stringBuilder.Append(statName);
        }
    }
}
