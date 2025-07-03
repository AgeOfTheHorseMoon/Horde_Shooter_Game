using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] Item _item;
    SpriteRenderer _spriteRenderer;
    TextMeshPro _textAmount;

    public ItemData GetItemData => _item.ItemData;

    private void Awake()
    {
        if (_item == null)
        {
            Destroy(this.gameObject);
        }

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _textAmount = GetComponentInChildren<TextMeshPro>();
        //if (_textMeshPro != null)
        //{
        //    Debug.Log("Found Component");
        //}
    }

    private void Start()
    {
        _spriteRenderer.sprite = _item.ItemData.Sprite;
        if (_item.ItemData.Amount > 1)
        {
            _textAmount.SetText(_item.ItemData.Amount.ToString());
        }
        else
        {
            _textAmount.SetText("");
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
