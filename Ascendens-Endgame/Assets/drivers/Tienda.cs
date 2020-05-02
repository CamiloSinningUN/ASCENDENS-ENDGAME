using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public Avatar avatar;
    
    void Start()
    {
        avatar = GameObject.FindWithTag("Player").GetComponent<Avatar>();
        Debug.Log(avatar);
        

    }
    public void ComprarVidamax()
    {
        avatar.ComprarVidamax(1,3);
    }
    public void ComprarManamax()
    {
        avatar.ComprarManamax(1,3);
    }
    public void ComprarDaño()
    {
        avatar.ComprarDaño(1,1);
    }
    public void ComprarRegeneraciónVida()
    {
        avatar.ComprarRegeneraciónVida(2);
    }
    public void ComprarRegeneraciónMana()
    {
        avatar.ComprarRegeneraciónMana(2);
    }
}
