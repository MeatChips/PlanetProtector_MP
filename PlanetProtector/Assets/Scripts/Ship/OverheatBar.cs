using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color minColor;
    [SerializeField] private Color maxColor;

    void Start()
    {
        slider.onValueChanged.AddListener(ChangeFillColor);
        // Set initial color
        ChangeFillColor(slider.value); // Set first color
    }

    public void SetNumber(int number)
    {
        slider.value = number;
    }

    public void SetMaxNumber(int number)
    {
        slider.maxValue = number;
        slider.value = number;
    }

    void ChangeFillColor(float value)
    {
        fillImage.color = Color.Lerp(minColor, maxColor, slider.normalizedValue);
    }

}
