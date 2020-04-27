using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button_Manager : MonoBehaviour
{
      
    
    public void Jugar(string x)
    {
        SceneManager.LoadScene(x);
    }
    
    public void salir()
    {
        Application.Quit();
    }
}
