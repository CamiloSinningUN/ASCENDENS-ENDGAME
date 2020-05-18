using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Da funciones y atributos a la bala disparada por el personaje.
///Próximamente disparada también por enemigos
///</summary>

public class Shoot : MonoBehaviour
{
    public float AttackRangeDistance = 2f;
    public LayerMask enemyMask;
    public int velocidad;

    ///<summary>
    ///Se llama al iniciar.
    ///</summary>

    private void Start()
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(GameObject.Find("Gun").transform.position, AttackRangeDistance, enemyMask);
        foreach (Collider enemy in hitEnemies)
        {
            
            if (enemy != null)
            {
                float x = enemy.GetComponent<Transform>().position.x - GameObject.Find("Gun").GetComponent<Transform>().position.x;
                float y = enemy.GetComponent<Transform>().position.y - GameObject.Find("Gun").GetComponent<Transform>().position.y;
                float norma = Mathf.Sqrt(x * x + y * y);
                trayectoria(velocidad * (x / norma), velocidad * (y / norma));
                
            }

           
        }
    }

    ///<summary>
    ///Le da un dirección a la bala.
    ///</summary>
    ///<param name="x">
    /// Es la velocidad que adquiere la bala en la dirección x.
    ///</param>
    ///<param name="y">
    ///Es la velocidad que adquiere la bala en la dirección y.
    ///</param>

    public void trayectoria(float x,float y)
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(x, y, 0);        
    }

    ///<summary>
    ///Es llamado cuando un collider comienza a tocar el trigger del objeto.
    ///</summary>

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().recibirdaño(1);
            }
           
        }


    }
}
