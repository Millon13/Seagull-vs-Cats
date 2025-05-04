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
        
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))//����� ContainsKey ��������� ���������� �� ����� ���� � healthContainer(��������� true ��� false) ���� ������ ����� ��� ������� � ������ ������� ��� ������� health
        {
            var health = GameManager.Instance.healthContainer[col.gameObject];// ��������� �� ����� ��������� ���� ������� �������� �������
            health.TakeHit(damage, gameObject);
        }
        /*var health = col.gameObject.GetComponent<Health>();
        if (health != null)//����� ContainsKey ��������� ���������� �� ����� ���� � healthContainer(��������� true ��� false) ���� ������ ����� ��� ������� � ������ ������� ��� ������� health
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
