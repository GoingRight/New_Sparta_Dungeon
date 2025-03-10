using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerInventory inventory;

    public ItemData curItem;
    public Transform dropPosition;


    public Action addItem;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        inventory = GetComponent<PlayerInventory>();
    }
}
