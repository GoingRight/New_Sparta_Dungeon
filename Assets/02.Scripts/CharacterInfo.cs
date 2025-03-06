using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Awake()
    {
        CharacterManager.Instance.Player.condition.characterInfo = this;
    }
}
