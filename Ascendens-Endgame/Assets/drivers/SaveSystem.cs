
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

///<summary>
///Sistema de guardado.
///Guarda.
///Carga.
///Borra los personajes.
///</summary>

public static class SaveSystem 
{

    ///<summary>
    ///Guarda el personaje.
    ///</summary>
    ///<param name="player">
    ///Es un personaje.
    ///</param>

    public static void SavePlayer(Avatar player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path="";
        switch (PlayerPrefs.GetInt("Boton"))
        {
            case 0:
                path = Application.persistentDataPath + "/player0.fun";
                break;
            case 1:
                path = Application.persistentDataPath + "/player1.fun";
                break;
            case 2:
                path = Application.persistentDataPath + "/player2.fun";
                break;
            default:
                Debug.Log("No se encontro boton");
                break;
        }    
        FileStream stream = new FileStream(path, FileMode.Create);
        AvatarData data = new AvatarData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    ///<summary>
    ///Carga el jugador seleccionada.
    ///</summary>
    ///<return>
    ///Devuelve un objeto de la clase avatardata.
    ///</return>

    public static AvatarData LoadPlayer()
    {
        string path ="";
        
        switch (PlayerPrefs.GetInt("Boton"))
        {
            case 0:
                path = Application.persistentDataPath + "/player0.fun";
                break;
            case 1:
                path = Application.persistentDataPath + "/player1.fun";
                break;
            case 2:
                path = Application.persistentDataPath + "/player2.fun";
                break;
            default:
                Debug.Log("No se encontro boton");
                break;
        }
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AvatarData data = formatter.Deserialize(stream) as AvatarData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("archivo no encontrado"+path);
            return null;
        }
    }

    ///<summary>
    ///Borra el jugador.
    ///</summary>

    public static void RemovePlayer()
    {
        string path = "";
        
        switch (PlayerPrefs.GetInt("Boton"))
        {
            case 0:
                path = Application.persistentDataPath + "/player0.fun";
                break;
            case 1:
                path = Application.persistentDataPath + "/player1.fun";
                break;
            case 2:
                path = Application.persistentDataPath + "/player2.fun";
                break;
            default:
                Debug.Log("No se encontro boton");
                break;
        }
        if (File.Exists(path))
        {
            File.Delete(path);            
        }
       
    } 
}
