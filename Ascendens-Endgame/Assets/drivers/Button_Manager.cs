using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///Administra funciones de los botones de las escenas iniciales.
///</summary>

public class Button_Manager : MonoBehaviour
{

    ///<summary>
    ///Lleva a la escena deseada.
    ///</summary>
    ///<param name="x">
    ///Es el nombre de la escena a la que se desea ir.
    ///</param>

    public void Jugar(string x)
    {
        SceneManager.LoadScene(x);
    }

    ///<summary>
    ///Saca del juego.
    ///</summary>

    public void salir()
    {
        Application.Quit();
    }

    ///<summary>
    ///Lleva a la página de desarrollo del juego.
    ///</summary>
    ///<param name="link">
    ///Es el url de la página.
    ///</param>
  
}
