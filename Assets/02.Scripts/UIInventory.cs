using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public PlayerInventory playerinventory;
    public UIItemSlot[] uIItemSlots;
    public GameObject slotPrefab;
    public Transform Slots;
    public ItemData selectedItem;
    public int selectedItemIndex;

    public Button useButton;
    public Button dropButton;
    public Action onClickUseBtn;
    public Action<ItemData> onClickDropBtn;

    private void Start()
    {
        playerinventory = CharacterManager.Instance.Player.inventory;
        invenUIInit();
        playerinventory.slotUIUpdate += slotUIUpdate;
        onClickDropBtn = playerinventory.DropItem;
    }

    public void invenUIInit()
    {
        uIItemSlots = new UIItemSlot[playerinventory.slotCount];
        for(int i = 0; i < playerinventory.slotCount; i++)
        {
            uIItemSlots[i] = Instantiate(slotPrefab, Slots).GetComponent<UIItemSlot>();
            uIItemSlots[i].uiSlotIndex = i;
        }
        slotUIUpdate();
    }

    public void slotUIUpdate()
    {
        for(int i = 0; i < uIItemSlots.Length; i++)
        {
            if (playerinventory.slots[i].itemData != null)
            {
                uIItemSlots[i].icon.sprite = playerinventory.slots[i].itemData.icon;
                uIItemSlots[i].quantityText.text = playerinventory.slots[i].quantity.ToString();
            }
            if (playerinventory.slots[i].quantity == 0)
            {
                uIItemSlots[i].quantityText.text = string.Empty;
            }
        }
    }

    public void ChangeBtnEffect(int index)
    {
        selectedItemIndex = index;
        //onClickUseBtn = playerinventory.slots[index].Use;

    }

    public void OnUse()
    {
        onClickUseBtn?.Invoke();
    }

    public void OnDrop()
    {
        onClickDropBtn?.Invoke(playerinventory.slots[selectedItemIndex].itemData);
    }
}
