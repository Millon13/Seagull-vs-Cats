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
   // public List<UpgradeCount> upgradeCount;
   // public List<Buffs> buffShop;
   // public int initialCoinsCount;
    public bool isUpgraded;
    [SerializeField] private UpgradeBar upgradeBar;

    public void Start()
    {

         if (inventory != null)
         {
            //initialCoinsCount = inventory.coinsCount;
         }
        /* buffShop = new List<Buffs>
         {
            new Buff(BuffType.Speed, 10),
            new Buff(BuffType.Force, 5),
            new Buff(BuffType.Health, 20),
            new Buff(BuffType.Damage, 15)
         }*/
    }
    /*public void AddBuff(BuffType buffType, int amount)
    {
        buffs.Add(new Buff(buffType, amount));
    }

    public void UpdateBuffAmount(BuffType buffType, int newAmount)
    {
        foreach (var buff in buffs)
        {
            if (buff.buffType == buffType)
            {
                buff.amount = newAmount;
                break;
            }
        }
    }*/

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

               // var buff = buffReciever.Buffs.Find(b => b.type == buffType);
                //if (buff != null)
                {
                    //upgradeCount += upgradeAmount;
                    //buff.additiveBonus = upgradeCount;
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
        if (isUpgraded) // »спользуем coinsCount из inventory
        {
            inventory.coinsCount -= 10;
            inventory.UpdateCoinsText();
        }

    }
    #region Save and Load
    public void Save(ref UpdateSaveData data)
    {
        //data.upgradeAmountList = upgradeCount;
    }
    public void Load(UpdateSaveData data)
    {
        //upgradeCount = Convert.ToInt32(data.upgradeAmountList);
    }
    #endregion
}
[System.Serializable]
public struct UpdateSaveData
{
   // public List<UpgrateList> upgradeAmountList;
}
