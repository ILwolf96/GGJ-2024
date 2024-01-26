using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LaughMeter : MonoBehaviour
{
    public Slider slider;
    
    //public void SetMaxLaugh(int laugh)
    //{
    //    slider.maxValue = laugh;
    //    slider.value = laugh;
    //}

    public void SetLaugh(float laugh)
    {
        slider.value += laugh;
    }
}
