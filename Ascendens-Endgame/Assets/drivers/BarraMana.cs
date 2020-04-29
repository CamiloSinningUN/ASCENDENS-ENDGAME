using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraMana : MonoBehaviour
{
    public Slider slider;
    public void setmaxmana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void setmana(int mana)
    {
        slider.value = mana;
    }
}
