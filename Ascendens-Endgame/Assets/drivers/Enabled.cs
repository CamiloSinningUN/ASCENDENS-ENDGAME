using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabled : MonoBehaviour
{
    public GameObject botonContinuar;
    public int boton;
    public GameObject Ascendens;
    void Start()
    {
        boton = PlayerPrefs.GetInt("Boton");
        //verificar que el personaje existe
        
    }

}
