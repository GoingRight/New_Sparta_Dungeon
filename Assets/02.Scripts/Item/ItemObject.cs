using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractInfo();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;

    public string GetInteractInfo()
    {
        string str = $"{itemData.itemName}\n{itemData.itemDescription}";
        return str;
    }

    public void OnInteract()
    {
        
    }
}
