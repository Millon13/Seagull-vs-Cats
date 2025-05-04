using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private bool isDestroingAfterCollision;
    private IObjectDestroyer destroyer;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent;}
        set { parent = value; }
    }
    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;
        
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))//метод ContainsKey проверяет существует ли такой ключ в healthContainer(возращает true или false) если такого ключа нет очевино у такого объекта нет скрипта health
        {
            var health = GameManager.Instance.healthContainer[col.gameObject];// возращает по ключу компонент хелс данного игрового объекта
            health.TakeHit(damage, gameObject);
        }
        /*var health = col.gameObject.GetComponent<Health>();
        if (health != null)//метод ContainsKey проверяет существует ли такой ключ в healthContainer(возращает true или false) если такого ключа нет очевино у такого объекта нет скрипта health
        {

            health.TakeHit(damage,gameObject);
        }*/
        if (isDestroingAfterCollision)
        {
            if (destroyer == null)
                Destroy(gameObject);
            else destroyer.Destroy(gameObject);
        }
    }
 
}
public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
