using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    public Slider slider;

    public void SetNumber(int number)
    {
        slider.value = number;
    }

    public void SetMaxNumber(int number)
    {
        slider.maxValue = number;
        slider.value = number;
    }
}
