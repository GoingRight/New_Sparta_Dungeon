using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }
    }

    public Canvas gameUI;
    public Canvas inventoryUI;
    public DialogueUI dialogueUI;

    private bool isInventory;
    public bool IsInventory
    {
        get
        {
            return isInventory;
        }
        set
        {
            isInventory = value;
            gameUI.gameObject.SetActive(!isInventory);
            inventoryUI.gameObject.SetActive(isInventory);
        }

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        isInventory = false;
    }
}
