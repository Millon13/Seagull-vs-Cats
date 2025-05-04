using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : Sound
{
    public int damage = 10;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //public string collisionTag;
    [SerializeField] private Animator animator;
    private float direction;
    private Health health;
    public float Direction
    {
        get { return direction;  }
       
    }
   

    private void Start()
    {
        //if (Mathf.Abs(direction) > 0.1)
          PlaySound(0);
    }
    private void OnCollisionStay2D(Collision2D col)//если скрипт висит на игроке в колизион попадет колайдер противника и наоборот
    {
        health = col.gameObject.GetComponent<Health>();
        //if (collision.gameObject.tag == collisionTag) не особо верно могут возникать ошибки
       // if (col.gameObject.CompareTag(collisionTag))
       //{ 
       
           
            
        if (health != null)
        {
         //animator.SetTrigger("Startimpact");
         direction = (col.transform.position - transform.position).x;
         animator.SetFloat("Direction", Mathf.Abs(direction));

        }
         
        //SetDamage();
            
           
       // }
            
        // Debug.Log("Взаимодействие с правильным объетом");
        //else

            //Debug.Log("Взаимодействие с правильным объетом");
        //Debug.Log(collision.gameObject.name);
    }
    public void SetDamage()
    {
        if (health != null)
            health.TakeHit(damage, gameObject);
        health = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }
   /* private void OnCollisionExit2D(Collision2D col)
    {
        if (animator != null)
            animator.SetBool("PlayerNear", false);
    }*/

}
