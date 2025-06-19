using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BuffUpgradeButton : MonoBehaviour
{ 
    public Inventory inventory; // Ññûëêà íà îáúåêò Inventory
    [SerializeField] private BuffType buffType;
    public BuffReciever buffReciever;
    public int upgradeAmount;
    private int upgradeCount;
   
    public bool isUpgraded;
    [SerializeField] private UpgradeBar upgradeBar;

    public void Start()
    {
        GameManager.Instance.upgrade = this;
        if (inventory != null)
        {
          //inventory = GameManager.Instance.inventory;
        }
        
        StartCoroutine(CoinsXCheck());
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

        ÑoinsCheck();
        buffReciever = GetComponent<BuffReciever>();
        if (buffReciever == null)
        {
            Debug.LogError("BuffReciever is not attached to the GameObject.");
        }
    }
    public void ÑoinsCheck()
    {
        if (inventory.coinsCount >= 10) // Èñïîëüçóåì coinsCount èç inventory
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
            ÑoinsCheck();
            
            
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
        if (isUpgraded) // Èñïîëüçóåì coinsCount èç inventory
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
        if (data.upgradeAmountList == null || data.upgradeAmountList.Count == 0)
        {
            Debug.LogError("No upgrade data to load.");
            return;
        }
        foreach (var upgrade in data.upgradeAmountList)
        {
            var buff = buffReciever.Buffs.Find(b => b.type == upgrade.buffType);
            if (buff != null)
            {
                buff.shopAmount = upgrade.shopAmount; 
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
