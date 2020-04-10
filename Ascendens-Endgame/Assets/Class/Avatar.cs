using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    
    public float fuerza;
    public int dinero;
    public float velocidad = 2f;
    public float AttackRange = 0.5f;
    public float AttackRangeDistance = 2f;
    public int daño=1;
    public LayerMask enemyMask;
    public bool aux=true;
    public GameObject bala;
    

    
    private void OnCollisionStay(Collision collision)
    {

        if (Input.GetKey(KeyCode.W) && collision.transform.tag == "piso")
        {

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, fuerza, 0);
            GameObject.Find("Person").GetComponent<Animator>().SetBool("Jumping", true);
        }
        else if (collision.transform.tag == "piso")
        {
            GameObject.Find("Person").GetComponent<Animator>().SetBool("Jumping", false);
            aux = true;
        }
    }
    private void Update()
    {
        if (aux)
        {
            movimiento();
        }
        if (!GameObject.Find("Person").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attacking"))
        {
            ataque_cuerpo();
        }
            

        Dispara();
        
    }
    public void movimiento()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            GameObject.Find("Person").GetComponent<Animator>().SetBool("Moving", true);
            GameObject.Find("Person").GetComponent<SpriteRenderer>().flipX = true;
            
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            GameObject.Find("Person").GetComponent<Animator>().SetBool("Moving", true);
            GameObject.Find("Person").GetComponent<Animator>().GetComponent<SpriteRenderer>().flipX = false;
            
        }
        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            GameObject.Find("Person").GetComponent<Animator>().SetBool("Moving", false);
        }
    }
    public void ataque_cuerpo()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            GameObject.Find("Person").GetComponent<Animator>().SetTrigger("atacking");
            Collider[] hitEnemies =  Physics.OverlapSphere(GameObject.Find("AttackPoint").transform.position,AttackRange,enemyMask);
            foreach (Collider enemy in hitEnemies)
            {
                
                    enemy.GetComponent<Enemy>().recibirdaño(daño);
                           
            }
        }
    }
    public void recibir_daño(int daño,Transform posicion_daño)
    {
        if (posicion_daño.position.x > gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-6, 4, 0);
            GameObject.Find("Person").GetComponent<Animator>().SetTrigger("hit");
            aux = false;

        }
        if(posicion_daño.position.x < gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(6, 4, 0);
            GameObject.Find("Person").GetComponent<Animator>().SetTrigger("hit");
            aux = false;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GameObject.Find("AttackPoint").GetComponent<Transform>().position,AttackRange);
        Gizmos.DrawWireSphere(GameObject.Find("Gun").GetComponent<Transform>().position, AttackRangeDistance);
    }
    public void Dispara()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Collider[] hitEnemies = Physics.OverlapSphere(GameObject.Find("Gun").transform.position, AttackRangeDistance, enemyMask);
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy != null)
                {                    
                    Instantiate(bala, GameObject.Find("Gun").transform.position, Quaternion.Euler(0, 0, 90));                    
                }
                

            }
        }
    }
}
