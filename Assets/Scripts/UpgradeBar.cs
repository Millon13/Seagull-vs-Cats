using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
 {    
    
    [SerializeField] private Image updateBar;// ������ �� Image ���������� ��������-����
    [SerializeField] private Button mainButton;
    [SerializeField] private Button other1Button;
    [SerializeField] private Button other2Button;
    [SerializeField] private Button other3Button;
    private float currentProgress = 0f; // ������� ��������
    private float targetProgress = 0f; // ������� ��������
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
               // ������������� �� ������� ������� ������
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
            currentProgress += deltaProgress; // ����������� ������� ��������
            
            updateBar.fillAmount = Mathf.Clamp01(currentProgress); // ��������� ��������-���

        }
        else if(currentProgress >= targetProgress )
        {
            currentProgress += 0; // ����������� ������� ��������

            updateBar.fillAmount = Mathf.Clamp01(currentProgress);
        }
          
       
    } 
    
    public void ApplyBuff()
    {
        
        SetTargetProgress(targetProgress + deltaProgress);
    }

    public void SetTargetProgress(float newTarget)
    {
        targetProgress = Mathf.Clamp01(newTarget); // ������������� ����� ������� �������� � ������������ ��� �� 0 �� 1
        
    }
    public void ButtonClick()
    {
        ApplyBuff();
    }


}

