using Coruk.CharacterStats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test_StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat { 
        get { return _stat; }
        set {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name {
        get { return _name; }
        set {
            _name = value;
            nameText.text = _name.ToLower();
        }
    }

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI valueText;

    [SerializeField] Test_StatTooltip _tooltip;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        nameText = texts[0];
        valueText = texts[1];

        if (_tooltip == null)
        {
            _tooltip = FindObjectOfType<Test_StatTooltip>(); // FindObjectOFType only works if object is active on hierarchy
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }

}
