using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    public string siguiente;
    //guardar progreso   
    //pasar a siguiente escena
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Avatar>().nivel = siguiente;
            other.GetComponent<Avatar>().guardarJugador();
            
            SceneManager.LoadScene(siguiente);

        }
    }
}
