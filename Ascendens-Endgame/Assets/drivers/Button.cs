using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    public void Boton(int i)
    {
        PlayerPrefs.SetInt("Boton", i);
    }
   
}
