using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ItemBase))]

public class ItemBaseEditor : Editor//Расширяем возможности эдитора юнити
{
    private ItemBase itemBase;

    private void Awake()
    {
        itemBase = (ItemBase)target;//поле target хранит ссылку на ItemBase
    }
    public override void OnInspectorGUI()

    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New Item"))
        {
            itemBase.CreateItem();
        }
        if (GUILayout.Button("Remove"))
        {
            itemBase.RemoveItem();
        }
        if (GUILayout.Button("<="))
        {
            itemBase.PrevItem();
        }
        if (GUILayout.Button("=>"))
        {
            itemBase.NextItem();
        }
      
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }

}
