using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Le da estilo a la historia del juego.
///</summary>

public class StarWars : MonoBehaviour
{
    public float velocidad = 80;

    ///<summary>
    ///Es llamada al iniciar.
    ///</summary>

    void Start()
    {
        
    }

   
    ///<summary>
    ///Es llamado en cada frame.
    ///</summary>

    void Update()
    {
        Vector3 posicion = transform.position;
        Vector3 vectorArriba = transform.TransformDirection(0,1,0);
        posicion += vectorArriba * velocidad * Time.deltaTime;
        transform.position = posicion;
    }
}
