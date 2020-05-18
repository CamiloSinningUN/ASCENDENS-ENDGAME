using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///Es el menú de pausa.
///</summary>

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject Intrucciones;

    ///<summary>
    ///Se llama al inicio.
    ///</summary>

    private void Start()
    {
        GameIsPaused = false;
    }

    ///<summary>
    ///Se llama cada frame.
    ///</summary>

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        } 
    }

    ///<summary>
    ///Reanuda.
    ///</summary>

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Intrucciones.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    ///<summary>
    ///Pausa el juego.
    ///</summary>

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    ///<summary>
    ///Despliega las instrucciones.
    ///</summary>

    public void Instrucciones()
    {
        Intrucciones.SetActive(true);
    }

    ///<summary>
    ///Cierra las instrucciones.
    ///</summary>

    public void cerrarInstrucciones()
    {
        Intrucciones.SetActive(false);
    }

    ///<summary>
    ///Sale de la partida y redirige a la pantalla inicial.
    ///</summary>

    public void Salir()
    {
        SceneManager.LoadScene("Jugar");
    }

}
