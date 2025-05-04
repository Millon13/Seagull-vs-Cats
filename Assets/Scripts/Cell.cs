using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Cell : MonoBehaviour

{
    public Action UpdateCell;
    [SerializeField] private Image icon;
    //[SerializeField] private ItemComponent itemComponent;
    private Item item;
    public bool isAD;
    private void Awake()
    {
        icon.sprite = null;
    }

    public void Init(Item item)
    {
        this.item = item;
        if (item == null)
            icon.sprite = null;
        else
            icon.sprite = item.Icon;
    }
    public void OnClickCell()
    {
        if (item == null)
            return;
        GameManager.Instance.inventory.Items.Remove(item);
        Buff buff = new Buff
        {
            type = item.Type,
            
            additiveBonus = item.Value
        };
        GameManager.Instance.inventory.buffReciever.AddBuff(buff);
        if (UpdateCell != null)
            UpdateCell();
       // isAD = (item.Type == BuffType.Health);
         
    }
   
}
