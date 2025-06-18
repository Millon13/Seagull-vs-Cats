using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BuffUpgradeButton : MonoBehaviour
{

    public Inventory inventory; // Ссылка на объект Inventory
    public BuffType buffType;
    public BuffReciever buffReciever;
    public int upgradeAmount;
    private int upgradeCount;
   // public List<Buffs> buffShop;
   // public int initialCoinsCount;
    public bool isUpgraded;
    [SerializeField] private UpgradeBar upgradeBar;

    public void Start()
    {
        GameManager.Instance.upgrade = this;
        StartCoroutine(CoinsXCheck());
        buffReciever = GetComponent<BuffReciever>();
        if (inventory != null)
         {
            //initialCoinsCount = inventory.coinsCount;
         }

    }
  
    public void OnUpgradeButtonClicked()
    {

         ChargeMoney();
         
         UpgradeBuff(buffType, upgradeAmount);
         upgradeBar.UpdateButtonState();
    }
    public IEnumerator CoinsXCheck()
    {
        yield return new WaitForEndOfFrame();

        coinsCheck();
    }
    public void coinsCheck()
    {
        if (inventory.coinsCount >= 10) // Используем coinsCount из inventory
        {
            isUpgraded = true;
        }
        else if (inventory.coinsCount < 10)
            isUpgraded = false;
    }
    public void UpgradeBuff(BuffType buffType, int upgradeAmount)
    {
        if (SceneManager.GetActiveScene().name == "UpdateShop")
        {
            
            
            if (isUpgraded)
            {
                var buff = buffReciever.Buffs.Find(b => b.type == buffType);
                if (buff != null)
                {
                    upgradeCount += upgradeAmount;
                    buff.shopAmount += 5;
                    upgradeBar.ApplyBuff();
                }
            }
            
            
            if (!isUpgraded)
                Debug.Log("Not enough coins to upgrade.");
            
        }
    }
    public void ChargeMoney()
    {
        if (isUpgraded) // Используем coinsCount из inventory
        {
            inventory.coinsCount -= 10;
            inventory.UpdateCoinsText();
        }

    }
    #region Save and Load
    public void Save(ref UpdateSaveData data)
    {
        if (buffReciever == null)
        {
            Debug.LogError("buffReciever is null.");
            return;
        }

        if (buffReciever.Buffs == null)
        {
            Debug.LogError("buffReciever.Buffs is null.");
            return;
        }
        data.upgradeAmountList = new List<UpgrateList>();
        foreach (var buff in buffReciever.Buffs)
        {
            data.upgradeAmountList.Add(new UpgrateList(buff.type, buff.shopAmount));
        }
    }
    public void Load(UpdateSaveData data)
    {
        foreach (var upgrade in data.upgradeAmountList)
        {
            var buff = buffReciever.Buffs.Find(b => b.type == upgrade.buffType);
            if (buff != null)
            {
                buff.shopAmount = upgrade.shopAmount; // Восстанавливаем shopAmount из сохраненных данных
            }
        }
    }
    #endregion
}
[System.Serializable]

public struct UpgrateList
{
    public BuffType buffType;
    public float shopAmount;

    public UpgrateList(BuffType buffType, float shopAmount)
    {
        this.buffType = buffType;
        this.shopAmount = shopAmount;
    }
}

[System.Serializable]

public struct UpdateSaveData
{
   public List<UpgrateList> upgradeAmountList;
}
