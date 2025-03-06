using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    public Image bar;
    public void UpdateUI(float ratio)
    {
        bar.fillAmount = ratio;
    }
}
