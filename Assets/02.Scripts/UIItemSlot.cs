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
    public int uiSlotIndex;

    public UIInventory inventory;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        inventory = GetComponentInParent<UIInventory>();
    }

    public void OnClick()
    {
        if (icon.sprite != null)
        {
            inventory.ChangeBtnEffect(uiSlotIndex);
            inventory.useButton.gameObject.SetActive(true);
            inventory.dropButton.gameObject.SetActive(true);
        }
        else
        {
            inventory.useButton.gameObject.SetActive(false);
            inventory.dropButton.gameObject.SetActive(false);
        }


    }
}
