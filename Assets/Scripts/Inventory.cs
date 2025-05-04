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
        coinsText.text = ": " + coinsCount.ToString(); // Инициализируем текст с текущим количеством монет
        items = new List<Item>();
        Debug.Log("Start work");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(col.gameObject))
        {
            var coin = GameManager.Instance.coinContainer[col.gameObject];
            if (!coin.IsCollected)
            {
                coinsCount++; // Увеличиваем количество монет
                UpdateCoinsText(); // Обновляем текст с количеством монет

                coin.IsCollected = true;
                coin.StartDestroy(); // Удаляем объект монеты

            }

            // Debug.Log("кол-во монет: " + coinsCount);


        }
        if (GameManager.Instance.itemsContainer.ContainsKey(col.gameObject))
        {
            var itemsComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemsComponent.Item);
            itemsComponent.Destroy(col.gameObject);


        }
    }
    public void UpdateCoinsText()
    {
        coinsText.text = ": " + coinsCount.ToString(); // Обновляем текстовое поле с количеством монет
    }
 


  
}



