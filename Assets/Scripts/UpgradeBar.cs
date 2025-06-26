using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UpgradeBar : MonoBehaviour
 {
    [SerializeField] public ButtonUpgrade buttonUpgrade;
    [SerializeField] private Image updateBar;// Ссылка на Image компонента прогресс-бара
    [SerializeField] private Button mainButton;
    [SerializeField] private Button other1Button;
    [SerializeField] private Button other2Button;
    [SerializeField] private Button other3Button;
    private float currentProgress = 0f; // Текущий прогресс
    private float targetProgress; // Целевой прогресс
    [SerializeField] private float deltaProgress = 0.1428f;
    [SerializeField] private BuffUpgradeButton buffUpgradeButton;
    public bool isUpdating;
    public bool firstInizialize;
    private BuffType thisBuffType;
    public float thisAmount;
    [SerializeField] private BuffReciever buffReciever;
    void Start()
    {
        
        GameManager.Instance.bar = this;
        StartCoroutine(UpdateButtState());
        //UpdateButtonState();
        if (updateBar == null)
        {
            Debug.LogError("UpdateBar Image is not assigned!");
        }
        if (mainButton != null)
        {
            // Подписываемся на событие нажатия кнопки
            mainButton.onClick.AddListener(ButtonClick);
        }
    }

   
    public IEnumerator UpdateButtState()
    {
        yield return new WaitForEndOfFrame();

        UpdateButtonState();
    }
    public void UpdateButtonState()
    {
       

        if (buffUpgradeButton.isUpgraded)
        {
            mainButton.interactable = true;
            other1Button.interactable = true;
            other2Button.interactable = true;
            other3Button.interactable = true;

        }
        else if(!buffUpgradeButton.isUpgraded)
        {
            mainButton.interactable = false;
            other1Button.interactable = false;
            other2Button.interactable = false;
            other3Button.interactable = false;
            
        }
    }
    public IEnumerator UpdateThisBar()
    {
        yield return new WaitForEndOfFrame();

        if(currentProgress < targetProgress)
        {
            currentProgress += deltaProgress; // Увеличиваем текущий прогресс

            updateBar.fillAmount = Mathf.Clamp01(currentProgress); // Обновляем прогресс-бар

        }
        else if (currentProgress >= targetProgress)
        {
            currentProgress += 0; // Увеличиваем текущий прогресс

            updateBar.fillAmount = Mathf.Clamp01(currentProgress);
        }
       /* while (currentProgress < targetProgress)
        {
            currentProgress += deltaProgress; // Увеличиваем текущий прогресс
            updateBar.fillAmount = Mathf.Clamp01(currentProgress); // Обновляем прогресс-бар
            yield return null; // Ждем до следующего кадра
        }

        currentProgress = targetProgress; // Убедимся, что текущий прогресс не превышает целевой
        updateBar.fillAmount = Mathf.Clamp01(currentProgress); // Обновляем прогресс-бар
        Debug.Log("targetProgress" + targetProgress);*/
        //isUpdating = false;
    }

    // Update is called once per frame
    /*void Update()
    {

        StartCoroutine(UpdateThisBar());
       
    } */
    /*public void OnUpgradeBarClicked()
    {
        UpgradeBarBuff(thisBuffType, thisAmount);
    }*/
    public void ApplyBuff()
    {
        
        SetTargetProgress(targetProgress + deltaProgress);
        //isUpdating = true;
        StartCoroutine(UpdateThisBar());
    }
    public void SetTargetProgress(float newTarget)
    {
        targetProgress = Mathf.Clamp01(newTarget); // Устанавливаем новый целевой прогресс и ограничиваем его от 0 до 1
        Debug.Log($"Target progress set to: {targetProgress}");
    }
    public void UpgradeBarBuff(BuffType thisBuffType, float thisAmount)
    {
       

        thisBuffType = buttonUpgrade.buffType;

        var buff = buffReciever.Buffs.Find(b => b.type == thisBuffType);//buff=null

        if (buff != null)
        {
            buff.amount = targetProgress;
            Debug.Log("amount" + buff.amount);
        }
        else if (buff == null)
        {
            Debug.Log("buff == null");
        }
       
    }
  

    
    public void ButtonClick()
    {
       // if (!isUpdating) // Проверяем, идет ли обновление
        {
            
            ApplyBuff();
            Debug.Log("Button clicked!");
            UpgradeBarBuff(thisBuffType, thisAmount);
            
        }
        
    }
    public IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();

    }

    #region Save and Load
    public void Save(ref UpgradeBarSaveData data)
    {
        
        data.amountBarList = new List<UpgrateBarList>();


        foreach (var buff in buffReciever.Buffs)
        {
            //buff.amount = targetProgress;
            data.amountBarList.Add(new UpgrateBarList(buff.type, buff.amount));
            
        }
    }
    public void Load(UpgradeBarSaveData data)
    {
        //targetProgress = data.amount;
        if (data.amountBarList == null || data.amountBarList.Count == 0)
        {
            Debug.LogError("No upgrade data to load.");
            return;
        }
        
        foreach (var bar in data.amountBarList)
        {
            var buff = buffReciever.Buffs.Find(b => b.type == bar.buffType);
            if (bar.buffType == buff.type)
            {
                //SetTargetProgress(targetProgress);
                // StartCoroutine(UpdateThisBar());
                //ButtonClick();
                ApplyBuff();

            }
           
            if (buff != null)
            {
                //StartCoroutine(WaitFrame());
                //targetProgress = buff.amount;
                buff.amount = bar.amount;
               
                Debug.Log("Load buff "+ bar.buffType+" targetProgress " + bar.amount);
            }
            else

            {
                
                if (firstInizialize)
                {
                    buffReciever.Buffs.Add(new Buff { type = BuffType.Damage, amount = bar.amount });
                    buffReciever.Buffs.Add(new Buff { type = BuffType.Force, amount = bar.amount });
                    buffReciever.Buffs.Add(new Buff { type = BuffType.Speed, amount = bar.amount });
                    buffReciever.Buffs.Add(new Buff { type = BuffType.Health, amount = bar.amount });
                    firstInizialize = false;
                }
                Debug.Log("Buff with type " + bar.buffType + " not found during load.");
            }
            
        }
    }
    #endregion
}

[System.Serializable]

public struct UpgrateBarList
{
    public BuffType buffType;
    public float amount;
    public UpgrateBarList(BuffType buffType, float amount)
    {
        this.buffType = buffType;
        this.amount = amount;
    }
}
[System.Serializable]
public struct UpgradeBarSaveData
{
    public List<UpgrateBarList> amountBarList; 
}