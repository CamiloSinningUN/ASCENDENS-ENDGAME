using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject Person;
    
    void Awake()
    {
        Instantiate(Person, gameObject.transform, true);
        Person.GetComponent<Avatar>().CargarJugador();


    }

    
}
