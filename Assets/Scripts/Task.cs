using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour

{
    public Animator[] tasks;
    public bool isGoodEnd;
    [SerializeField] Player player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator anim in tasks)
            {
                anim.SetTrigger("triggerON");
            }
            isGoodEnd = true;
            
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
       {
            foreach (Animator anim in tasks)
            {
                anim.SetTrigger("triggerON");
            }
            isGoodEnd = false;
        }

    }

}
