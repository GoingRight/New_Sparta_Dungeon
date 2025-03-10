using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int slotCount;
    public ItemSlot[] slots;
    public Transform dropPosition;

    public Action slotUIUpdate;

    private void Awake()
    {
        InventoryInit();
    }
    private void Start()
    {
        CharacterManager.Instance.Player.addItem += Additem;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
    }

    public void InventoryInit()
    {
        slots = new ItemSlot[slotCount]; //배열만 선언하고 내용물은 비어있음

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();//내용물 채워주기
            slots[i].index = i;
        }
    }
    public void Additem()
    {
        ItemData data = CharacterManager.Instance.Player.curItem;
        //아이템이 중복 가능한지
        //가능하다
        //아이템이 있는가
        //맥스 스택개수를 넘겼는가
        //불가능하다
        //남는 칸이 있는가
        //있다면 거기에 넣고 curItem은 null
        //없다면 dropPosition에서 다시 아이템을 생성
        if (data.canStack)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(slots[i].itemData = data)
                {
                    if (slots[i].quantity < data.maxStackAmount)
                    {
                        slots[i].quantity++;
                        CharacterManager.Instance.Player.curItem = null;
                        slotUIUpdate?.Invoke();
                        return;
                    }
                }
            }
        }

        ItemSlot emptySlot = null;

        for(int i =0;  i < slots.Length; i++)
        {
            if (slots[i].itemData == null)
            {
                emptySlot = slots[i];
                break;
            }
        }
        if(emptySlot != null)
        {
            emptySlot.itemData = data;
            emptySlot.quantity = 1;
            CharacterManager.Instance.Player.curItem = null;
            slotUIUpdate?.Invoke();
            return;
        }
        else
        {
            DropItem(data);
            CharacterManager.Instance.Player.curItem = null;
        }

    }

    public void DropItem(ItemData dropItem)
    {
        Instantiate(dropItem.dropPrefab, dropPosition.position, Quaternion.identity);
        slotUIUpdate?.Invoke();
    }
}
