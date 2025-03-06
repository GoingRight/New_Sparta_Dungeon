using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public Condition Health;
    public Condition Stamina;
    [SerializeField] private ConditionUI healthUI;
    [SerializeField] private ConditionUI staminaUI;
    [SerializeField] private float maxhelath;
    [SerializeField] private float maxstamina;


    public CharacterInfo characterInfo;

    private void Start()
    {
        //if(Haskey)


        Health = new Condition(maxhelath, healthUI.UpdateUI);
        Stamina = new Condition(maxstamina, staminaUI.UpdateUI);
    }

    bool CheckStamina(float cost, float curStamina)
    {
        if (curStamina < cost)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
