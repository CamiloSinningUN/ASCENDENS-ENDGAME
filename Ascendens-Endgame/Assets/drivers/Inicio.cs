using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Instancia al personaje en la escena.
///</summary>

public class Inicio : MonoBehaviour
{
    public GameObject Person;

    ///<summary>
    ///Se llama antes que todos los "void start".
    ///</summary>

    void Awake()
    {
        Instantiate(Person, gameObject.transform, true);
        Person.GetComponent<Avatar>().CargarJugador();


    }

    
}
