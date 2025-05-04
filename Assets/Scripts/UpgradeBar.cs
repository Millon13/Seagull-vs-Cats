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
    void Start()
    {
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
    public void UpdateButtonState()
    {
        if (buffUpgradeButton.isUpgraded)
        {
            mainButton.interactable = true;
            other1Button.interactable = true;
            other2Button.interactable = true;
            other3Button.interactable = true;

        }
        else
        {
            mainButton.interactable = false;
            other1Button.interactable = false;
            other2Button.interactable = false;
            other3Button.interactable = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (currentProgress < targetProgress ) 
        {
            currentProgress += deltaProgress; // Увеличиваем текущий прогресс
            
            updateBar.fillAmount = Mathf.Clamp01(currentProgress); // Обновляем прогресс-бар

        }
        else if(currentProgress >= targetProgress )
        {
            currentProgress += 0; // Увеличиваем текущий прогресс

            updateBar.fillAmount = Mathf.Clamp01(currentProgress);
        }
          
       
    } 
    
    public void ApplyBuff()
    {
        
        SetTargetProgress(targetProgress + deltaProgress);
    }

    public void SetTargetProgress(float newTarget)
    {
        targetProgress = Mathf.Clamp01(newTarget); // Устанавливаем новый целевой прогресс и ограничиваем его от 0 до 1
        
    }
    public void ButtonClick()
    {
        ApplyBuff();
    }


}

