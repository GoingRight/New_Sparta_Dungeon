using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Enemy,
    Alley,
    Neutral
}

[CreateAssetMenu(fileName = "NPC", menuName = "New NPC")]
public class NPCData : ScriptableObject
{
    public NPCType Type;
    public string npcName;
    public string npcDescription;
    public string[] lines;
}
