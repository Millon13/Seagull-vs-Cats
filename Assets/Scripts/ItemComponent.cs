using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : Sound, IObjectDestroyer
{
    [SerializeField] public ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
 

    public Item item;
    public Item Item
    {
        get { return item; }
    }
    public void Destroy(GameObject gameObject)
    {
        MonoBehaviour.Destroy(gameObject);
    }
    void Start()
    {
        item = GameManager.Instance.itemDataBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.itemsContainer.Add(gameObject, this);
        PlaySound(0);
       

    }

   

    
}

public enum ItemType
{
    Energetic=1,Grasshopper=2,Donate=3,Fish=4,Health=5
}
