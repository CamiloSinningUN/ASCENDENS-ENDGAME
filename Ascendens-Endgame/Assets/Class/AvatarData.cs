using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

///<summary>
///Hecha para funciones de guardado.
///</summary>

public class AvatarData 
{
    public float fuerza;
    public int Money;
    public float velocidad;    
    public int daño;
    public int vida;
    public string nivel;

    ///<summary>
    ///Es el constructor.
    ///</summary>
    ///<param name="avatar">
    ///Es un personaje.
    ///</param>

    public AvatarData(Avatar avatar)
    {
        fuerza = avatar.fuerza;
        Money = avatar.Money;
        velocidad = avatar.velocidad;
        daño = avatar.daño;
        vida = avatar.vida;
        nivel = avatar.nivel;
    }
}
