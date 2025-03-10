using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Usable
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public GameObject dropPrefab;
    public Sprite icon;

    [Header("Stack")]
    public bool canStack;
    public int maxStackAmount;

    public virtual void UseItem()
    {

    }
}
