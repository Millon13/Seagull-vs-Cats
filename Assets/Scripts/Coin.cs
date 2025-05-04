using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Sound
{
    [SerializeField] private Animator animator;
    public bool IsCollected { get; set; }
    private void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this);
        IsCollected = false;
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }
    public void EndDestroy()
    {
        
        Destroy(gameObject);
    }

        
}
