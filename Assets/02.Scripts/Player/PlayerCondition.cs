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

    public float lastStaminaUse;
    public float staminaRecoverRate;
    public float recoverStaminaValue;

    private void Start()
    {
        lastStaminaUse = 0;
        Health = new Condition(maxhelath, healthUI.UpdateUI);
        Stamina = new Condition(maxstamina, staminaUI.UpdateUI);
    }

    private void Update()
    {
        if(Stamina.curValue < Stamina.maxValue)
        {
            RecoverStamina();
        }
    }

    public bool CheckStamina(float cost)
    {
        if (Stamina.curValue < cost)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void RecoverStamina()
    {
        if(Time.time - lastStaminaUse> staminaRecoverRate)
        {
            Stamina.Add(recoverStaminaValue * Time.deltaTime);
            Stamina.curValue = Mathf.Clamp(Stamina.curValue, 0, maxstamina);
        }
    }
}
