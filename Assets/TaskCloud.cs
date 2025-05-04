using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCloud : MonoBehaviour
{
    public Animator[] taskCloud;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Animator anim in taskCloud)
            {
                anim.SetTrigger("Trigger");
            }
        }

    }
    public void OnTriggerExitr2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Animator anim in taskCloud)
            {
                anim.SetTrigger("Trigger");
            }
        }

    }
}