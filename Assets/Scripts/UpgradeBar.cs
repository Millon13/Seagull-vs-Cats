using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
 {    
    
    [SerializeField] private Image updateBar;// Ссылка на Image компонента прогресс-бара
    [SerializeField] private Button mainButton;
    [SerializeField] private Button other1Button;
    [SerializeField] private Button other2Button;
    [SerializeField] private Button other3Button;
    private float currentProgress = 0f; // Текущий прогресс
    private float targetProgress = 0f; // Целевой прогресс
    [SerializeField] private float deltaProgress = 0.1428f;
    [SerializeField] private BuffUpgradeButton buffUpgradeButton;
    public bool isUpdating;
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
        isUpdating = false;
    }

    // Update is called once per frame
    /*void Update()
    {

        StartCoroutine(UpdateThisBar());
       
    } */
    
    public void ApplyBuff()
    {
        
        SetTargetProgress(targetProgress + deltaProgress);
        isUpdating = true;
        StartCoroutine(UpdateThisBar());
    }

    public void SetTargetProgress(float newTarget)
    {
        targetProgress = Mathf.Clamp01(newTarget); // Устанавливаем новый целевой прогресс и ограничиваем его от 0 до 1
        Debug.Log($"Target progress set to: {targetProgress}");
    }
    public void ButtonClick()
    {
        if (!isUpdating) // Проверяем, идет ли обновление
        {
            ApplyBuff();
            Debug.Log("Button clicked!");
        }
    }

    #region Save and Load
    public void Save(ref UpgradeBarSaveData data)
    {
        data.amount = targetProgress;
    }
    public void Load(UpgradeBarSaveData data)
    {
        targetProgress = data.amount;
    }
    #endregion*/
}

[System.Serializable]
public struct UpgradeBarSaveData
{
    public float amount;
}