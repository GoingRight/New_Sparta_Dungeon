using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCData NPCData;

    public string GetInteractInfo()
    {
        string str = $"{NPCData.npcName}\n{NPCData.npcDescription}";
        return str;
    }

    public void OnInteract()
    {
        
    }


}
