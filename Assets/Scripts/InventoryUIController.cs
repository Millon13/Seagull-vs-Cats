using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Cell[] cell;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;
    void Init()
    {
        cell = new Cell[cellCount];
        for(int i = 0; i < cellCount; i++)
        {
            cell[i] = Instantiate(cellPrefab, rootParent);
            cell[i].UpdateCell += UpdateInventory;
           
        }
        cellPrefab.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (cell == null || cell.Length <= 0)
            Init();
        UpdateInventory();

    }
    public void UpdateInventory()
    {
        var inventory = GameManager.Instance.inventory;
        foreach(var cel in cell)
        {
            cel.Init(null);
        }
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cell.Length) 
                cell[i].Init(inventory.Items[i]);
        }

    }
}
