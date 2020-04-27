using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject Person;
    
    void Start()
    {
        Instantiate(Person, gameObject.transform, true);
        


    }

    
}
