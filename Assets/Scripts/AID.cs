using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AID : Sound
{
    //public Health health;
   // public int bonusHealth;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Enemy"))
        {
            // aidHealth += 10;
            //health.health += aidHealth;
            /*Health health = col.gameObject.GetComponent<Health>();
            health.SetHealth();*/
            Destroy(gameObject);
            PlaySound(0);
        }
    }
   
}
