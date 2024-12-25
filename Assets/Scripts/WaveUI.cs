using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Start()
    {
        slider.maxValue = 70;
        slider.minValue = 0;
    }
    public void setslider(int amount)
    {
        slider.value = amount;
    }
}
