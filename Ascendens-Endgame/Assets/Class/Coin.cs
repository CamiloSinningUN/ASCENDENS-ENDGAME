using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Avatar>().recibirDinero(1);
            Destroy(gameObject);
        }
        Debug.Log("Entre moneda");
    }
}
