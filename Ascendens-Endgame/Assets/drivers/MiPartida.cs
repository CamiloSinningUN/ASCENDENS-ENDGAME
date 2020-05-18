using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///....
///</summary>

public class MiPartida : MonoBehaviour
{
    public void Lapartida(int numero)
    {
        PlayerPrefs.GetInt("Mipartida", numero);
    }
}
