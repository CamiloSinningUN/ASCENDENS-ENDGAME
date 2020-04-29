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
    public int vida=3;
    public int vidaActual;
    public string nivel="Nivel1.0";

    public LayerMask enemyMask;
    public bool aux=true;
    public GameObject bala;
    public GameObject AttackPointL;
    public GameObject AttackPointR;
    public GameObject Gun;
    public GameObject Sprite;
    public BarraVida barravida;
    //las plataformas hacen que escales, arreglar eso
    private void Start()
    {
        barravida = GameObject.Find("BarraVida").GetComponent<BarraVida>();
       
        CargarJugador();
        
        vidaActual = vida;
        barravida.setmax(vida);
    }
    
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.transform.tag != "piso" && gameObject.GetComponent<Rigidbody>().velocity.y !=0)
        {
            aux = false;
        }
       
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
        vidaActual = vidaActual - daño;
        barravida.setHealth(vidaActual);
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
        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    public void recibirDinero(int money)
    {
      
        Money = Money + money;
    }  
   
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
            nivel = data.nivel;
        }
        else
        {
            Debug.Log("Soy nulo");
        }
        

    }
   
}
