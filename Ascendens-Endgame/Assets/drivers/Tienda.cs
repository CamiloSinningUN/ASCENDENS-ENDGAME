using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///Clase que permite la compra de atributos y pociones.
///</summary>

public class Tienda : MonoBehaviour
{
    public Avatar avatar;

    public GameObject BotonCompraVidaMax;
    public GameObject BotonCompraManaMax;
    public GameObject BotonCompraRegeneraciónVida;
    public GameObject BotonCompraRegeneraciónMana;
    public GameObject BotonCompraDaño;

    ///<summary>
    ///Es llamada al iniciar.
    ///</summary>

    void Start()
    {
        avatar = GameObject.FindWithTag("Player").GetComponent<Avatar>();
    }

    ///<summary>
    ///Es llamada en cada frame.
    ///</summary>

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

    ///<summary>
    ///Sirve para comprar la vida máxima.
    ///</summary>

    public void ComprarVidamax()
    {
        avatar.ComprarVidamax(1,3);
    }

    ///<summary>
    ///Sirve para comprar el Maná máximo.
    ///</summary>

    public void ComprarManamax()
    {
        avatar.ComprarManamax(1,3);
    }

    ///<summary>
    ///Sirve para Comprar daño.
    ///</summary>

    public void ComprarDaño()
    {
        avatar.ComprarDaño(1,1);
    }

    ///<summary>
    ///Sirve para comprar regeneración de la vida.
    ///</summary>

    public void ComprarRegeneraciónVida()
    {
        avatar.ComprarRegeneraciónVida(2);
    }

    ///<summary>
    ///Sirve para Comprar regeneración del maná.
    ///</summary>

    public void ComprarRegeneraciónMana()
    {
        avatar.ComprarRegeneraciónMana(2);
    }
}
