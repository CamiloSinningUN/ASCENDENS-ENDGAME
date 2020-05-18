using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///Clase principal de la aplicación.
///</summary>
///<remarks>
///Controla las partidas que se encuentran disponibles.
///</remarks>

public class Ascendens : MonoBehaviour
{
    public AvatarData data;
    public GameObject BotonContinuar;

    ///<summary>
    ///Es llamado al iniciar el objeto y extrae en data el jugador requerido.
    ///</summary>

    private void Start()
    {
        data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            BotonContinuar.SetActive(false);
        }
    }

    ///<summary>
    ///Borra al jugador seleccionado.
    ///</summary>
    
    public void RemoverPartida()
    {
        SaveSystem.RemovePlayer();
    }

    ///<summary>
    ///Extrae información del jugador seleccionado.
    ///Devuelve la info en el atributo data.
    ///Carga el nivel donde se encuentra el jugador.
    ///</summary>

    public void CargarPartida()
    {
        data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            SceneManager.LoadScene(data.nivel);
        }
        
       
        
    }
}
