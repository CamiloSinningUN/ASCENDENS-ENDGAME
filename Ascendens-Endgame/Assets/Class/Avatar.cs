using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Avatar : MonoBehaviour
{
    
    public float fuerza;
    public int Money;
    public float velocidad;
    public float AttackRange = 0.5f;
    public float AttackRangeDistance = 2f;
    public int daño;
    public int vida;   

    public LayerMask enemyMask;
    public bool aux=true;
    public GameObject bala;
    public GameObject AttackPointL;
    public GameObject AttackPointR;
    public GameObject Gun;
    public GameObject Sprite;
    private void awake()
    {
        CargarJugador();
    }
    private void OnCollisionStay(Collision collision)
    {

        if (Input.GetKey(KeyCode.W) && collision.transform.tag == "piso")
        {

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, fuerza, 0);
            Sprite.GetComponent<Animator>().SetBool("Jumping", true);
            
        }
        else if (collision.transform.tag == "piso")
        {
            Sprite.GetComponent<Animator>().SetBool("Jumping", false);
            aux = true;
        }
        if (collision.transform.tag != "piso")
        {
            aux = false;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPointL.GetComponent<Transform>().position, AttackRange);
        Gizmos.DrawWireSphere(AttackPointR.GetComponent<Transform>().position, AttackRange);
        Gizmos.DrawWireSphere(Gun.GetComponent<Transform>().position, AttackRangeDistance);
    }
    private void Update()
    {
        if (aux)
        {
            movimiento();
        }
        if (!Sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attacking"))
        {
            ataque_cuerpo();
        }

        morir();
        Dispara();
        
    }
    //
    public Avatar(Avatar player)
    {
        fuerza = player.fuerza;
        Money = player.Money;
        velocidad = player.velocidad;
        daño = player.daño;
        vida = player.vida;
    }
    //
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
            Sprite.GetComponent<Animator>().SetTrigger("atacking");           
            Collider[] hitEnemiesL = Physics.OverlapSphere(AttackPointL.transform.position,AttackRange,enemyMask);
            Collider[] hitEnemiesR = Physics.OverlapSphere(AttackPointR.transform.position, AttackRange, enemyMask);
            foreach (Collider enemy in hitEnemiesL)
            {
                if (Sprite.GetComponent<SpriteRenderer>().flipX == false)
                {
                    enemy.GetComponent<Enemy>().recibirdaño(daño);
                }                                             
            }
            foreach (Collider enemy in hitEnemiesR)
            {
                if (Sprite.GetComponent<SpriteRenderer>().flipX == true)
                {
                    enemy.GetComponent<Enemy>().recibirdaño(daño);
                }
            }
        }
    }
    public void recibir_daño(int daño,Transform posicion_daño)
    {
        vida = vida - daño;
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
                    break;
                }

                
            }
        }
    }
    public void morir()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    public void recibirDinero(int money)
    {
      
        Money = Money + money;
    }  
    //
    public void guardarJugador()
    {
        SaveSystem.SavePlayer(this);
    }
    public void CargarJugador()
    {
        AvatarData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            fuerza = data.fuerza;
            Money = data.Money;
            velocidad = data.velocidad;
            daño = data.daño;
            vida = data.vida;
        }
        

    }
   //
}
