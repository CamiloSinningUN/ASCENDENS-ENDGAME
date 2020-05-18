using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///Controla la barra de la vida mientras se encuentra jugando una partida.
///</summary>

public class BarraVida : MonoBehaviour
{
    public Slider slider;

    ///<summary>
    ///Asigna el valor máximo de la vida del personaje como máximo del slider.
    ///</summary>
    ///<param name="health">
    ///Es el valor máximo de la vida.
    ///</param>

    public void setmax(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    ///<summary>
    ///Asigna el valor actual de la vida del personaje al slider.
    ///</summary>
    ///<param name="health">
    ///Es el valor actual de la vida.
    ///</param>

    public void setHealth(int health)
    {
        slider.value = health;
    }
}
