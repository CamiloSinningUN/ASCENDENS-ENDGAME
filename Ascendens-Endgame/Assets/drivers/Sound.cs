using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource fuente;
    public AudioClip clip;
   
    

    public void reproducir()
    {
        fuente.clip = clip;
        fuente.Play();
    }
}
