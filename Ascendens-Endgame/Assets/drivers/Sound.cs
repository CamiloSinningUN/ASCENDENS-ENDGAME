using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Administra el sonido del videojuego.
///</summary>

public class Sound : MonoBehaviour
{
    public AudioSource fuente;
    public AudioClip clip;

    ///<summary>
    ///Reproduce.
    ///</summary>

    public void reproducir()
    {
        fuente.clip = clip;
        fuente.Play();
    }
}
