using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] public int health;
    [SerializeField] private Animator animator;
    public Action<int, GameObject> OnTakeHit;
    public Player player;

    //public Action<int, GameObject> OnAddHealth;
    public int CurrentHealth
    {
        get { return health; }
    }
    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
        

    }



    public void TakeHit(int damage, GameObject attacker)
    {
        
        health -= damage;//health = health - damage
        if (OnTakeHit != null)
            OnTakeHit(damage, attacker);
        //Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);//Удаляет геймобщект на котором расположен данный скрипт
           
        } 
        if (damage > 0)
        {
         animator.SetTrigger("Damage");
        }

        
        
      

    }
   
    /*public void SetHealth(int health)
    {
        this.health += health;//this пишем тк health в методе приоритетнее и он бы себя увеличивал, а не то что в начале пишется
    }
    */
    public void AddHealth()
    {
        health += Convert.ToInt32(player.bonusHealth);
        if (health > 100)
            health = 100;
       // if (OnAddHealth != null)
       //     OnAddHealth(bonusHealth);
    }
   
    /*private void Start()
    {
        Debug.Log(Player.Instance.isCheatMode);
    } */
   
    
}
