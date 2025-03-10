using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;

    public UIInventory inventory;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
