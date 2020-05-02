
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
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
