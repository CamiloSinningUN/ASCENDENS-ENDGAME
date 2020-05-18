using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Clase diseñada para las monedas.
///Hija de la clase object
///</summary>


public class Coin : Object
{
    public bool aux = true;

    ///<summary>
    ///En cada actualización de física este es llamado para cualquier collider que esté tocando el trigger.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && aux)
        {
            other.GetComponent<Avatar>().recibirDinero(1);
            aux = false;
            Destroy(gameObject);
            
        }
        
    }
}
