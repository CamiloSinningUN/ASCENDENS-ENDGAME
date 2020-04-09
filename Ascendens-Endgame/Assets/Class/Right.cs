using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    public bool Ladoderecho, recibir_Daño;
    void Start()
    {
        Ladoderecho = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "piso")
        {
            Ladoderecho = false;
        }

        if (other.tag == "enemigo")
        {
            recibir_Daño = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "piso")
        {
            Ladoderecho = true;
        }

    }
}
