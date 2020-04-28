using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ascendens : MonoBehaviour
{
    public AvatarData data;
    public GameObject BotonContinuar;
    private void Start()
    {
        data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            BotonContinuar.SetActive(false);
        }
    }
    public void RemoverPartida()
    {
        SaveSystem.RemovePlayer();
    }
    public void CargarPartida()
    {
        data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            
            SceneManager.LoadScene(data.nivel);
        }
        
       
        
    }
}
