using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Object
{
    public bool aux = true;
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
