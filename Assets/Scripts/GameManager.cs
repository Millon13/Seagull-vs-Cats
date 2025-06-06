using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject goodEndPanel;
    [SerializeField] private GameObject secondStarPanel;
    [SerializeField] private GameObject thirdStarPanel;
    [SerializeField] private GameObject badEndPanel;
    [SerializeField] private GameObject otherPanel;
    [SerializeField] private Animator animatorX;
    
    [SerializeField] private Task task;
    public Player Player { get; set; }
    public Inventory Inventory { get; set; }

    public int levelNumber;
    public int extraLevelCoins;
    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    public Dictionary<int, int> levelStars;
    //public Dictionary<GameObject, Fish> fishContainer;
    [HideInInspector] public Inventory inventory;
    public ItemBase itemDataBase;
    public GameObject player;
    private bool isPaused;
    public GameObject cross;
    
    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
        levelStars = new Dictionary<int, int>();
        //fishContainer = new Dictionary<GameObject, Fish>();
        LoadProgress();//тут нуль референс
    }
    private void Update()
    {
        BadEnding();
    
        /*if (Input.GetKey(KeyCode.Z))
        {
            SaveProgress();
        }
        if (Input.GetKey(KeyCode.X))
        {
            LoadProgress();
        }*/

        
        
       // GoodEnding();
    }
    public void EndLevel()
    {

        SaveProgress();
        CalculateStars(levelNumber);
        //UpdateCoinsBasedOnStars(levelNumber);
    }
    public void SaveProgress()
    {
        SaveSystem.Save();
    }
    public void LoadProgress()
    {
        SaveSystem.Load();//тут нуль референс
    }
    public void CalculateStars(int levelNumber)
    {
       
        if (!GameManager.Instance.levelStars.ContainsKey(levelNumber))
        {
            GameManager.Instance.levelStars[levelNumber] = Convert.ToInt32(Player.starCount); // Сохраняем звезды для уровня
        }
    }
    /*public void UpdateCoinsBasedOnStars(int levelNumber)
    {
        int coinsEarned = 0;


        if (GameManager.Instance.levelStars.TryGetValue(levelNumber, out int stars))
        {

            switch (stars)
            {
                case 3:
                    coinsEarned = 15;
                    break;
                case 2:
                    coinsEarned = 10;
                    break;
                case 1:
                    coinsEarned = 5;
                    break;
                default:
                    coinsEarned = 0;
                    break;
            }
        }

        extraLevelCoins += coinsEarned ;

    }*/
    private void Start()
    {
        badEndPanel.gameObject.SetActive(false);
        goodEndPanel.gameObject.SetActive(false);
        //otherPanel.gameObject.SetActive(true);
       
        
    }
    public void OnClickPause()
    {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
    public void OnClickInventary()
    {
        if (Time.timeScale > 0)
        {
            inventoryPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
            
        else
        {
            inventoryPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        

    }


    public void GoodEnding()
    {
       
       // isPaused = !isPaused;
        if (task != null && task.isGoodEnd )
        {
            goodEndPanel.gameObject.SetActive(true);
            otherPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            animatorX.SetBool("IsPaused", true);
            //EndSave();
            //Time.timeScale = 1;

        }
    }
    public void AfterEnding()
    {
        goodEndPanel.gameObject.SetActive(false);
        
    }
    public void OneStarGoodEnding()
    {

        // isPaused = !isPaused;
            
        if (task != null && task.isGoodEnd )
        {
            goodEndPanel.gameObject.SetActive(true);
            secondStarPanel.gameObject.SetActive(false);
            thirdStarPanel.gameObject.SetActive(false);
            otherPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            animatorX.SetBool("IsPaused", true);
            //EndSave();
            //Time.timeScale = 1;

        }
    }
    public void TwoStarGoodEnding()
    {

        // isPaused = !isPaused;
        if (task != null && task.isGoodEnd )
        {
            goodEndPanel.gameObject.SetActive(true);
            thirdStarPanel.gameObject.SetActive(false);
            otherPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            animatorX.SetBool("IsPaused", true);
            //EndSave();
            //Time.timeScale = 1;

        }
    }
    public void BadEnding()
    {
        isPaused = !isPaused;
        if (player == null)
        {
            if (isPaused)
            {
                badEndPanel.gameObject.SetActive(true);
                otherPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                animatorX.SetBool("IsPaused", true);
                //EndSave();
                //Time.timeScale = 1;
            }
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /*public void EndSave()
    {
        SaveSystem.Save();
    }*/
    public void Upgrade()
    {
        // coinContainer
    }

    //StartCoroutine(Animation(2f)); 
    /*else
    {
        badEndPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        animator.SetBool("IsPaused", false);
    }\*

}



}
/* private IEnumerator Animation(float delay)
{
yield return new WaitForSeconds(delay);

}
/*private void Start()
{
var healthObjects = FindObjectsOfType<Health>;//можно но игра будет долго грузиться
foreach(var health in healthObjects)
{
    healthContainer.Add(health.gameObject, health);
}*/
}
