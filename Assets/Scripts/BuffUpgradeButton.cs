using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BuffUpgradeButton : MonoBehaviour
{

    public Inventory inventory; // —сылка на объект Inventory
    public BuffType buffType;
    public BuffReciever buffReciever;
    public float upgradeAmount;
    public int initialCoinsCount;
    public bool isUpgraded=false;
    [SerializeField] private UpgradeBar upgradeBar;

    public void Start()
    {
         if (inventory != null)
         {
        
            initialCoinsCount = inventory.coinsCount;
        
         }
    }
   
    public void OnUpgradeButtonClicked()
    {

         ChargeMoney();
         UpgradeBuff(buffType, upgradeAmount);
         upgradeBar.UpdateButtonState();
         
         
         

    }

    public void UpgradeBuff(BuffType buffType, float upgradeAmount)
    {
        if (SceneManager.GetActiveScene().name == "UpdateShop")
        {

            if (inventory.coinsCount >= 10) // »спользуем coinsCount из inventory
            {

                isUpgraded = true;
               
                

                var buff = buffReciever.Buffs.Find(b => b.type == buffType);
                if (buff != null)
                {
                    buff.additiveBonus += upgradeAmount;
                    upgradeBar.ApplyBuff();
                }
                

            } 
            else
            {
                isUpgraded = false;
                Debug.Log("Not enough coins to upgrade.");
            }
           
        }
    }
    public void ChargeMoney()
    {
        if (inventory.coinsCount >= 10) // »спользуем coinsCount из inventory
        {

            inventory.coinsCount -= 10;
            inventory.UpdateCoinsText();
        }

    }
}