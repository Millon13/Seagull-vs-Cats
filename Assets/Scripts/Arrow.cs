using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour, IObjectDestroyer
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private Rigidbody2D rigidbd;
    [SerializeField] private float lifeTime;
    private Player player;
    public float LiveTime
    {
        get { return lifeTime; }
        set { lifeTime = value; }
    }
    [SerializeField] private float force;
    public float Force
    {
        get { return Force; }
        set { Force = value; }
    }
    
    
    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowInPool(this);
    }
 


    public void SetImpulse(Vector2 direction, float force,int bonusDamage, Player player)
    {
        

        this.player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject;
        triggerDamage.Damage += bonusDamage;

        rigidbd.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLive());
        Debug.Log("SetImpulse/StartLive");

        
        
    }
    private IEnumerator StartLive()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        Debug.Log("StartLive работает");
        yield break;
        
    }
  


}
