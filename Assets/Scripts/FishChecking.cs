using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class FishChecking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isGoodEnd;
   
    private void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("Player"))
        {
            isGoodEnd = true;
        }

    }

    private void OnTriggerStay(Collider col)
    {


        if (col.CompareTag("Player"))
        { 
            isGoodEnd = true;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        { 
            isGoodEnd = false;
        }

    }
}
