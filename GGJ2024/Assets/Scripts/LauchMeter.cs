using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LauchMeter : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxLauch(int laugh)
    {
        slider.maxValue = laugh;
        slider.value = laugh;
    }

    public void SetLaugh(int laugh)
    {
        slider.value = laugh;
    }
}
