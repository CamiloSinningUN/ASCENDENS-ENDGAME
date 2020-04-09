using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left : MonoBehaviour
{
    public bool ladoizquierdo, recibir_Daño;
    void Start()
    {
        ladoizquierdo = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "piso")
        {
            ladoizquierdo = false;
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
            ladoizquierdo = true;
        }

    }
}
