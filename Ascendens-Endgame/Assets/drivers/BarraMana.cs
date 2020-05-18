using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///Controla la barra de maná mientras se juga la partida.
///</summary>

public class BarraMana : MonoBehaviour
{
    public Slider slider;

    ///<summary>
    ///Asigna el valor máximo del maná del personaje como máximo del slider.
    ///</summary>
    ///<param name="mana">
    ///Es el maná máximo del personaje.
    ///</param>

    public void setmaxmana(int mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    ///<summary>
    ///Asigna el valor actual del maná del personaje al slider.
    ///</summary>
    ///<param name="mana">
    ///Es el maná actual del personaje.
    ///</param>

    public void setmana(int mana)
    {
        slider.value = mana;
    }
}
