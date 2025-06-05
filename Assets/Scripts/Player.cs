using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Player : Sound
{
    [SerializeField] private float speed = 2.5f;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.5)
                speed = value;
        }
    }
    [SerializeField] private float fish = 0.0f;
    public float Fish
    {
        get { return fish; }
        set
        {
            if (value > 0.1)
                fish = value;
        }
    }
    [SerializeField] private float force;
    [SerializeField] private float damageForce;
    [SerializeField] private Task task;
    [SerializeField] private Rigidbody2D rigidbd;
    public int totalLevelCoins;
    private bool hasMethodExecuted = false;
    
    [SerializeField] private float minimalHeight;
    
    [SerializeField] private bool isCheatMode;
    [SerializeField] private ItemComponent itemComponent;


    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Arrow arrow;// вместо SerializeField] private GameObject arrow;
    private Arrow currentArrow;// вместо SerializeField] private GameObject currentArrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private BuffReciever buffReciever;
    [SerializeField] private Health health;
    public Health Health { get { return health; } }

    //[SerializeField] private Arrow arrowscript;
    //[SerializeField] private int numberOfArrows=0;
    //[SerializeField] private float playerReadiness;
    private List<Arrow> arrowsPool;
    [SerializeField] private int arrowsCount = 3;

    [SerializeField] private float shootForce=5;
    [SerializeField] private float rechargeTime;
    [SerializeField] private Camera playerCamera;
    private bool isJumping;
    private bool isPlayerReadiness;
    private bool isBlockMovement;

    [SerializeField] private Joystick joystick;
    private float movejoystick;
    [SerializeField] LevelsStars levelsStars;

    [SerializeField] private float fishCount;
    private float bonusForce;
    private float bonusDamage;
    private float bonusSpeed;
    [HideInInspector]public float bonusHealth;
    private float bonusFish;
    public float starCount;
    private float currentLevel;
    private float inventoryCoins;
    
    [SerializeField] private float threeStarFish;
    [SerializeField] private float twoStarFish;
    [SerializeField] private float oneStarFish;

    [SerializeField] private bool isThreeStarFish;
    [SerializeField] private bool isTwoStarFish;
    [SerializeField] private bool isOneStarFish;
    [SerializeField] Inventory inventory;
    public int levelNumber;

    private UICharectorController controller;
   

    #region Instance
    public static Player Instance { get; set; }
    #endregion

    public void EndLevel()
    {
        
        SaveProgress();
        CalculateStars( levelNumber, fishCount);
        UpdateCoinsBasedOnStars(levelNumber);
    }
  
    public void SaveProgress()
    {
        SaveSystem.Save();
    }
    public void LoadProgress()
    {
        SaveSystem.Load();
    }
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //LoadProgress();
        arrowsPool = new List<Arrow>();
        for(int i=0; i < arrowsCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowsPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
        health.OnTakeHit += TalkeHit;
        totalLevelCoins=0;
        inventoryCoins = inventory.coinsCount;
        buffReciever.OnBuffChanged += ApplyBuffs;

        //if (fish = fish_end)
        levelsStars.level = currentLevel;

        
        
        PlaySound(2);
       
    }
    // Update is called once per frame
   

    private void ApplyBuffs()
    {
        //bonusHealth = 0;
        
         
        var forceBuff= buffReciever.Buffs.Find(t => t.type == BuffType.Force);
        var damageBuff= buffReciever.Buffs.Find(t => t.type == BuffType.Damage);
        var speedBuff= buffReciever.Buffs.Find(t => t.type == BuffType.Speed);
        var fishBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Fish);
 
        
        bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;
        bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
        bonusSpeed = speedBuff == null ? 0 : speedBuff.additiveBonus;
        bonusFish = fishBuff == null ? 0 : fishBuff.additiveBonus;
        fishCount += bonusFish;
        
        ApplyHealths();
            
        

       
       
    }
    private void ApplyHealths()
    { 
        var healthBuff = buffReciever.Buffs.Find(t => t.type == BuffType.Health);
        bonusHealth = healthBuff == null ? 0 : healthBuff.additiveBonus;

      
        if (bonusHealth > 0 &&  itemComponent.item.Type == BuffType.Health)
        {
            if (!hasMethodExecuted)
            {
                  health.AddHealth();
                  bonusHealth = 0; 
            }
            hasMethodExecuted = true;
        }
       
      
        // Применение бонусного здоровья

        //Debug.Log("Вы вызвали делегат");
    }
    public void InitUIController(UICharectorController uiController)
    {
        controller = uiController;
        controller.Jump.onClick.AddListener(Jump);
        controller.Fire.onClick.AddListener(CheckShoot);
    }
    void FixedUpdate()//вместо Update чтоб вне зависимости от компа физика работала корректно
    {
        
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
          Jump();
        }
       
#endif
        Movement();
        
        animator.SetFloat("Speed", Mathf.Abs(rigidbd.velocity.x));

        CheckFall();

    }
    private void Jump()
    {
        if ( groundDetection.isGround)
        {
            rigidbd.AddForce(Vector2.up * (force+bonusForce), ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
            PlaySound(0, random: true);
           

        }

    }
    private void Update()
    {
        if (fishCount == 0)
        {
            GameManager.Instance.AfterEnding();
        }
        if (fishCount == threeStarFish)
        {

            starCount = 3;
            isOneStarFish = false;
            isTwoStarFish = false;
            isThreeStarFish = true;
        }
        if (fishCount == twoStarFish)
        {

            starCount = 2;
            isOneStarFish = false;
            isTwoStarFish = true;
            isThreeStarFish = false;

        }
        if (fishCount == oneStarFish)
        {
            starCount = 1;
            isOneStarFish = true;
            isTwoStarFish = false;
            isThreeStarFish = false;
        }

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.OnClickPause();
        }
        if (gameObject == null)
        {
            starCount = 0;
            GameManager.Instance.BadEnding();
        }
        if (task.isGoodEnd && isOneStarFish)
        {
            isBlockMovement = true;
            GameManager.Instance.OneStarGoodEnding();
            
        }
        if (task.isGoodEnd && isTwoStarFish)
        {
            isBlockMovement = true;
            GameManager.Instance.TwoStarGoodEnding();
        }
        if (task.isGoodEnd && isThreeStarFish)
        {
            isBlockMovement = true;
            GameManager.Instance.GoodEnding();
            
        }
       
        //string message = $"Тип:{bonusFish.GetType().Name}";
        //Debug.Log(message);

    }
    public void CalculateStars(int levelNumber, float fishCount)
    {
        if (fishCount >= threeStarFish)
        {
            starCount = 3;
        }
        else if (fishCount >= twoStarFish)
        {
            starCount = 2;
        }
        else if (fishCount >= oneStarFish)
        {
            starCount = 1;
        }
        else
        {
            starCount = 0;
        }
        if (!GameManager.Instance.levelStars.ContainsKey(levelNumber))
        {
            GameManager.Instance.levelStars[levelNumber] = Convert.ToInt32(starCount); // Сохраняем звезды для уровня
        }
    }
    public void UpdateCoinsBasedOnStars(int levelNumber)
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

        totalLevelCoins += coinsEarned + Convert.ToInt32(inventoryCoins);

    }
    private void Movement()
    {
        animator.SetBool("isGround", groundDetection.isGround);
       
        if (!isJumping && !groundDetection.isGround)
            animator.SetTrigger("StartFall");
        isJumping = isJumping && !groundDetection.isGround;//мы проверяем если мы сейчас в прыжке и при этом мы не касаемся земли, то мы сохраняем значение перем.isJumping, если мы падаем при этом не касаемся земли тогда мы попадем в if выше и начнется анимация падения минуя startjump
        direction = Vector2.zero;//каждый кадр направление приводим к нулю

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
        direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
        direction = Vector2.right;
        }
#endif 
        /*if (controller.Left.IsPressed)
        
        {
            direction = Vector3.left;//(-1,0)
            //transform.Translate(Vector2.left*Time.deltaTime*speed); слишком примитивно
        }
        if (controller.Right.IsPressed)
       
        {
            direction = Vector3.right;//(1,0)
            //transform.Translate(Vector2.right * Time.deltaTime*speed);
        }*/
        movejoystick = joystick.Horizontal;
        direction = new Vector2(movejoystick*speed,rigidbd.velocity.y);
        //direction *= speed;
        //direction.y = rigidbd.velocity.y;//чтобы состовляющая по оси у не стала равна 0
        if(!isBlockMovement)
            rigidbd.velocity = direction;
        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true; 
        animator.SetFloat("Speed", Mathf.Abs(rigidbd.velocity.x));
        
       
        CheckFall();
    }
    private void TalkeHit(int damage, GameObject attacker)
    {
        animator.SetBool("GetDamage", true);
        animator.SetTrigger("TakeHit");
        isBlockMovement = true;
        rigidbd.AddForce(transform.position.x < attacker.transform.position.x ? new Vector2(-damageForce, 0) : new Vector2(damageForce, 0), ForceMode2D.Impulse);
       
    }
    public void UnBlockedMovement()
    {
        isBlockMovement = false;
        animator.SetBool("GetDamage", false);
    }
    private void CheckShoot()
    {
        if ( !isPlayerReadiness)
        {
            animator.SetTrigger("Shooting"); 
        }
    }
  
    public void InitArrow()
    {

        currentArrow = GetArrowFromPool();
        Debug.Log("Init/ GetArrowFromPool");
        currentArrow.SetImpulse(Vector2.right, 0,0, this); // убрали .GetComponent<Arrow>()(перед SetImpulse)
         Debug.Log("Init SetImpulse(Vector2.right, 0,0, this)");
        Debug.Log("Init/ GetArrowFromPool");
        // currentArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);//(объект, точка спавна, поворот)/trouble
        Debug.Log("InitArrow");

    }
    public void Shoot()
    {
        
        currentArrow.SetImpulse
            (Vector2.right, spriteRenderer.flipX ?
            -force * shootForce : force * shootForce, (int)bonusDamage, this);//trouble
      //  Debug.Log("Shoot/ SetImpulse(Vector2.right, spriteRenderer.flipX ? -force * shootForce : force * shootForce, (int)bonusDamage, this)");
        StartCoroutine(ReTime());
        //Debug.Log("Shoot/StartCoroutine(ReTime())");
       // Debug.Log("Shoot");
        PlaySound(1);
    }
 
    private IEnumerator ReTime()
    {
        isPlayerReadiness = true;
        yield return new WaitForSeconds(rechargeTime);
        isPlayerReadiness = false;
        Debug.Log("IEnumerator ReTime работает");
        // yield break;
    }

    private void CheckFall()
    {  
        if (transform.position.y < minimalHeight && isCheatMode)
        {
            rigidbd.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, 0);

        }
        else if (transform.position.y < minimalHeight && !isCheatMode) 
        { 
            Destroy(gameObject);
            OnDestroy();
        }

    }
    private Arrow GetArrowFromPool()
    {
        
        if (arrowsPool.Count > 0)
        {
            var arrowTemp = arrowsPool[0];
            arrowsPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }
    public void ReturnArrowInPool(Arrow arrowTemp)
    {
        if (!arrowsPool.Contains(arrowTemp))
            arrowsPool.Add(arrowTemp);
        
        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        playerCamera.transform.parent = null;
        playerCamera.enabled = true;
    }

    #region Save and Load
    public void Save(ref PlayerSaveData data)
    {
        data.starOfEndLevel= starCount;
        data.coinsCountOfEndScene = inventory.coinsCount;
        data.thisLevel = currentLevel;
    }
    public void Load(PlayerSaveData data)
    {
        starCount = (data.starOfEndLevel);//надо вызывать, когда начисляются звёзды
        inventory.coinsCount= Convert.ToInt32(data.coinsCountOfEndScene);
        currentLevel = data.thisLevel;
    }
    #endregion

}
[System.Serializable]
public struct PlayerSaveData
{
    public float thisLevel;
    public float starOfEndLevel;
    public float coinsCountOfEndScene;

}


