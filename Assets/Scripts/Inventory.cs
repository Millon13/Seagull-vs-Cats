using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{  
    [SerializeField]  public int coinsCount ;
    //[SerializeField] Player player;
    [SerializeField] private TMP_Text coinsText;
    public BuffReciever buffReciever;
    private List<Item> items;
  
    public List<Item> Items
    {
        get { return items; }
    }

    public void Start()
    {
        GameManager.Instance.inventory = this;
        //coinsCount = player.totalLevelCoins;
        coinsText.text = ": " + coinsCount.ToString(); // Инициализируем текст с текущим количеством монет
        items = new List<Item>();
       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        {
            var coin = GameManager.Instance.coinContainer[col.gameObject];
            if (!coin.IsCollected)
            {
                coinsCount++;
                

                

                UpdateCoinsText();

                coin.IsCollected = true;
                coin.StartDestroy(); 

            }

            // Debug.Log("кол-во монет: " + coinsCount);


        }
        coinsCount += GameManager.Instance.extraLevelCoins;
        if (GameManager.Instance.itemsContainer.ContainsKey(col.gameObject))
        {
            var itemsComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemsComponent.Item);
            itemsComponent.Destroy(col.gameObject);


        }
    }
    public void UpdateCoinsText()
    {
        coinsText.text = ": " + coinsCount.ToString(); 
    }

    #region Save and Load
    public void Save(ref InventorySaveData data)
    {
        data.levelCoinsCount = coinsCount;
        
    }
    public void Load(InventorySaveData data)
    {
        coinsCount = Convert.ToInt32(data.levelCoinsCount);
    }
    #endregion


}
[System.Serializable]


public struct InventorySaveData
{
    public float levelCoinsCount;
  

}