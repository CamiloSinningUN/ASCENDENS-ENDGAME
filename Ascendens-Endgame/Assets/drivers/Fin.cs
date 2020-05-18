using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///Administra el paso entre niveles.
///</summary>

public class Fin : MonoBehaviour
{
    public string siguiente;
    //guardar progreso   
    //pasar a siguiente escena

    ///<summary>
    ///En cada actualización de físicas este es llamado para cualqier collider que esté tocando el trigger.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

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
