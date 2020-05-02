using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    public Avatar avatar;

    public GameObject BotonCompraVidaMax;
    public GameObject BotonCompraManaMax;
    public GameObject BotonCompraRegeneraciónVida;
    public GameObject BotonCompraRegeneraciónMana;
    public GameObject BotonCompraDaño;


    void Start()
    {
        avatar = GameObject.FindWithTag("Player").GetComponent<Avatar>();
    }
    private void Update()
    {
        if (avatar.Money < 3)
        {
            BotonCompraVidaMax.SetActive(false);
            BotonCompraManaMax.SetActive(false);
        }
        if (avatar.Money < 2)
        {
            BotonCompraRegeneraciónVida.SetActive(false);
            BotonCompraRegeneraciónMana.SetActive(false);
        }
        if (avatar.Money < 1)
        {
            BotonCompraDaño.SetActive(false);
        }
        if (avatar.Money >= 3)
        {
            BotonCompraVidaMax.SetActive(true);
            BotonCompraManaMax.SetActive(true);
        }
        if (avatar.Money >= 2)
        {
            BotonCompraRegeneraciónVida.SetActive(true);
            BotonCompraRegeneraciónMana.SetActive(true);
        }
        if (avatar.Money >= 1)
        {
            BotonCompraDaño.SetActive(true);
        }
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
