using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AvatarData 
{
    public float fuerza;
    public int Money;
    public float velocidad;    
    public int daño;
    public int vida;
    public string nivel;

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
