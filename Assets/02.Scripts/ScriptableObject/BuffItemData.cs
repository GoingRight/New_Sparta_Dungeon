using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buff
{
    InfiniteStamina
}

[CreateAssetMenu(fileName = "BuffItem", menuName = "New BuffItem")]
public class BuffItemData : ItemData
{

    [Header("Buff")]
    public Buff buffType;
    public float buffDuration;

    public override void UseItem()
    {
        base.UseItem();
        Debug.Log(CharacterManager.Instance.Player.buffControll.name);
        CharacterManager.Instance.Player.buffControll.StartBuffEffect(this);
    }
}
