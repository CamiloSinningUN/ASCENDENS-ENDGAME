using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Guarda la partida a la que se entró.
///</summary>

public class Button : MonoBehaviour
{

    ///<summary>
    ///Cumple la función asignada a esta clase.
    ///</summary>
    ///<param name="i">
    ///Número de la partida.
    ///</param>

    public void Boton(int i)
    {
        PlayerPrefs.SetInt("Boton", i);
    }
   
}
