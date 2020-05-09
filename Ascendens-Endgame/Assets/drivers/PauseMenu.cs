using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject Intrucciones;

    // Update is called once per frame
    private void Start()
    {
        GameIsPaused = false;
    }
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
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Intrucciones.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Instrucciones()
    {
        Intrucciones.SetActive(true);
    }
    public void cerrarInstrucciones()
    {
        Intrucciones.SetActive(false);
    }
    public void Salir()
    {
        SceneManager.LoadScene("Jugar");
    }

}
