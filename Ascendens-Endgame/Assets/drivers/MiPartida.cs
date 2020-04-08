using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiPartida : MonoBehaviour
{
    public void Lapartida(int numero)
    {
        PlayerPrefs.GetInt("Mipartida", numero);
    }
}
