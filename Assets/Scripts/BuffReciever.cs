using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;
    public Action OnBuffChanged;
    public List<Buff> Buffs
    {
        get { return buffs; }
    }
    void Start()
    {
        GameManager.Instance.buffRecieverContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }
    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            buffs.Add(buff);
        if (OnBuffChanged != null)
            OnBuffChanged();
    }
    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);
        if (OnBuffChanged != null)
            OnBuffChanged();
    }
    
}
