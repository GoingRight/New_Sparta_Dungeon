using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour
{
    public PlayerInventory playerinventory;
    public UIItemSlot[] uIItemSlots;
    public GameObject slotPrefab;
    public Transform Slots;

    private void Start()
    {
        playerinventory = CharacterManager.Instance.Player.inventory;
        invenUIInit();
    }

    public void invenUIInit()
    {
        uIItemSlots = new UIItemSlot[playerinventory.slotCount];
        for(int i = 0; i < playerinventory.slotCount; i++)
        {
            Instantiate(slotPrefab, Slots);
        }
    }

    public void slotUIUpdate()
    {

    }
}
