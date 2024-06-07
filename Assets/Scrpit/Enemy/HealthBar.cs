using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider.value = 1;
    }

    public void HPbarValue(int currentHP, int maxHP)
    {
        if (slider != null)
        {
            float fillAmount = (float)currentHP / maxHP;
            slider.value = fillAmount;
        }
        else
        {
            Debug.LogWarning("Slider가 연결되지 않았습니다.");
        }
    }
}
