using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BuffUpgradeButton : MonoBehaviour
{
    [SerializeField] public ButtonUpgrade buttonUpgrade;
    public Inventory inventory; // Ññûëêà íà îáúåêò Inventory
    private BuffType currentbuffType;
    [SerializeField] public BuffReciever currentbuffReciever;
    public int upgradeAmount;
    private int upgradeCount;
    public bool isUpgraded;
    public bool oneInizialize=true;
    [SerializeField] private UpgradeBar upgradeBar;

    public void Start()
    {
       
        
        GameManager.Instance.upgrade = this;
        //BuffType currentbuffType = buttonUpgrade.buffType;
        //currentbuffReciever = buttonUpgrade.buffReciever;
        if (inventory != null)
        {
          //inventory = GameManager.Instance.inventory;
        }
        //buffReciever = GetComponent<BuffReciever>();
        if (currentbuffReciever == null)
        {
            Debug.LogError("BuffReciever is not attached to the GameObject.");
        }
        //StartCoroutine(Initialization());
        StartCoroutine(CoinsXCheck());
    }
  
    public void OnUpgradeButtonClicked()
    {
        
        ChargeMoney(); 
        UpgradeBuff(currentbuffType,upgradeAmount);
        upgradeBar.UpdateButtonState();
         
    }
    public IEnumerator CoinsXCheck()
    {
        yield return new WaitForEndOfFrame();
        ÑoinsCheck();
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
    public IEnumerator Initialization()
    {

        yield return new WaitForEndOfFrame();
        /*if (oneInizialize)
        {
            currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Damage, shopAmount = 0 });
            currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Force, shopAmount = 0 });
            currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Speed, shopAmount = 0 });
            currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Health, shopAmount = 0 });
            oneInizialize = false;
        }*/
    }
    public void UpgradeBuff(BuffType currentbuffType, int upgradeAmount)
    {
        //StartCoroutine(Initialization());
        currentbuffType = buttonUpgrade.buffType;
      
        Debug.Log("currentbuffType:" + currentbuffType);
        if (SceneManager.GetActiveScene().name == "UpdateShop")
        {
            
            if (isUpgraded)
            {
                Debug.Log("True");
                
                if (currentbuffReciever == null )
                {
                    Debug.LogError("BuffReciever  is null.");
                    return;
                }
                if ( currentbuffReciever.Buffs == null)
                {
                    Debug.LogError(" Buffs is null.");
                    return;
                }

                Debug.Log($"Number of Buffs: {currentbuffReciever.Buffs.Count}");

                /*for (int i = 0; i < currentbuffReciever.Buffs.Count; i++)
                {
                    Buff currentbuff = currentbuffReciever.Buffs[i];
                    Debug.Log($"Buff type: {currentbuff.type}");
                }*/
                
                foreach (var cbuff in currentbuffReciever.Buffs)
                {
                    Debug.Log($"Available Buff type: {cbuff.type}");
                } 
                currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Force, shopAmount = 0 });
                currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Damage, shopAmount = 0 });
                currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Speed, shopAmount = 0 });
                currentbuffReciever.Buffs.Add(new Buff { type = BuffType.Health, shopAmount = 0 });

                var buff = currentbuffReciever.Buffs.Find(b => b.type == currentbuffType);//buff=null

                if (buff != null)
                {
                    //upgradeCount += upgradeAmount;
                    //buff.shopAmount = upgrade.shopAmount;
                    buff.shopAmount += 5;
                    upgradeBar.ApplyBuff();
                    Debug.Log("shopAmount"+buff.shopAmount);
                }
                else if (buff == null)
                {
                    Debug.Log("buff == null");
                }
            }
            if (!isUpgraded)
                Debug.Log("Not enough coins to upgrade.");
            ÑoinsCheck();
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
        if (currentbuffReciever == null)
        {
            Debug.LogError("buffReciever is null.");
            return;
        }
        if (currentbuffReciever.Buffs == null)
        {
            Debug.LogError("buffReciever.Buffs is null.");
            return;
        }
        data.upgradeAmountList = new List<UpgrateList>();
        foreach (var buff in currentbuffReciever.Buffs)
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
            var buff = currentbuffReciever.Buffs.Find(b => b.type == upgrade.buffType);
            if (buff != null)
            {
                buff.shopAmount = upgrade.shopAmount; 
            }
            else
            {
                Debug.Log("Buff with type " + upgrade.buffType + " not found during load.");
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
