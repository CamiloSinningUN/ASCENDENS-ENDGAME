using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Rendering;

///<summary>
///Clase principal del personaje.
///</summary>
///<remarks>
///Controla todas las acciones y atributos actuales del personaje.
///</remarks>

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
    public int mana=3;
    public int manaActual;
    
    public string nivel="Nivel1.0";

    public bool GroundCheck;
    public LayerMask enemyMask;
    public bool aux=true;
    public bool Backpackisopen = false;
    public GameObject bala;
    public GameObject AttackPointL;
    public GameObject AttackPointR;
    public GameObject Gun;
    public GameObject Sprite;
    public BarraVida barravida;
    public BarraMana barramana;
    public GameObject mochila;
    public Text ContadorDinero;

    ///<summary>
    ///Se llama al iniciar.
    ///</summary>

    private void Start()
    {
         nivel = SceneManager.GetActiveScene().name;
   
          if(nivel == "Nivel1.0")
          {
              Sprite = GameObject.Find("Person");
              GameObject.Find("Eva").SetActive(false);
              GameObject.Find("Heavy").SetActive(false);
          }else if(nivel == "Nivel2.0")
          {
              Sprite = GameObject.Find("Heavy");
              GameObject.Find("Eva").SetActive(false);
              GameObject.Find("Person").SetActive(false);
          }else if(nivel == "Nivel3.0")
          {
              Sprite = GameObject.Find("Eva");
              GameObject.Find("Heavy").SetActive(false);
              GameObject.Find("Person").SetActive(false);
            Debug.Log("Me voltie");
            Sprite.GetComponent<SpriteRenderer>().flipX = false;

        }

        CargarJugador();
        barravida = GameObject.Find("BarraVida").GetComponent<BarraVida>();
        barramana = GameObject.Find("BarraMana").GetComponent<BarraMana>();
        mochila = GameObject.Find("Tienda");
        mochila.SetActive(false);
        ContadorDinero = GameObject.Find("Contador").GetComponent<Text>();
        ContadorDinero.text = Money + "";
        
        
        vidaActual = vida;
        barravida.setmax(vida);
        manaActual = mana;
        barramana.setmaxmana(mana);
    }

    ///<summary>
    ///En cada actualizacion de fisicas este es llamado para cualquier collider que este tocando el trigger.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "piso")
        {
            GroundCheck = true;
        }
        if (other.transform.tag == "Enemy")
        {
            GroundCheck = true;
        }
    }

    ///<summary>
    ///Este es llamado cuando el collider deja de tocar el trigger del objeto.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "piso")
        {
            GroundCheck = false;
        }
        if (other.transform.tag == "Enemy")
        {
            GroundCheck = false;
        }
    }

    ///<summary>
    ///Es llamado cuando un collider comienza a tocar el trigger del objeto.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="piso")
        Sprite.GetComponent<Animator>().SetBool("Jumping", false);
    }

    ///<summary>
    ///Este es llamado en cada frame para cualquier collider/rigidbody que este tocando el collider/rigidbody del objeto.
    ///</summary>
    ///<param name="collision">
    ///Es el collider que detecta el collider del objeto.
    ///</param>

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "piso" && GroundCheck == false)
        {
           
            aux = false;
        }else if (collision.transform.tag!="piso" && GroundCheck ==false)
        {
            aux = false;
        }
        
        if (collision.transform.tag == "piso" && GroundCheck == true)
        {
            aux = true;
        }
        if (collision.transform.tag == "Caer")
        {
            vidaActual = 0;
        }
        
        if (GroundCheck == false && collision.transform.tag == "Enemy")
        {
            aux = false;
        }
        if(GroundCheck && collision.transform.tag == "Enemy")
        {
            aux = true;
        }

    }

    ///<summary>
    ///Este es llamado cuadno el collider/rigidbody deja de tocar otro collider/rigidbody.
    ///</summary>
    ///<param name="collision">
    ///Es el collider que detecta el collider del objeto.
    ///</param>

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "piso" && GroundCheck == false)
        {
            aux = true;
        }
    }

    ///<summary>
    ///Dibuja una esfera útil para el desarrollo.
    ///</summary>

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPointL.GetComponent<Transform>().position, AttackRange);
        Gizmos.DrawWireSphere(AttackPointR.GetComponent<Transform>().position, AttackRange);
        Gizmos.DrawWireSphere(Gun.GetComponent<Transform>().position, AttackRangeDistance);
    }

    ///<summary>
    ///Se llama cada frame.
    ///</summary>

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
        
        saltar();        
        morir();
        Dispara();
        Mochila();
        
    }

    ///<summary>
    ///Hace posible el movimiento del personaje.
    ///</summary>

    public void movimiento()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            Sprite.GetComponent<Animator>().SetBool("Moving", true);
            Sprite.GetComponent<SpriteRenderer>().flipX = true;
            if ( Sprite.name=="Eva")
            {
                Sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
            Sprite.GetComponent<Animator>().SetBool("Moving", true);
            Sprite.GetComponent<Animator>().GetComponent<SpriteRenderer>().flipX = false;
            if (Sprite.name=="Eva")
            {
                Sprite.GetComponent<SpriteRenderer>().flipX = true;
            }

        }
        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
           Sprite.GetComponent<Animator>().SetBool("Moving", false);
        }
    }

    ///<summary>
    ///Permite el salto del personaje.
    ///</summary>

    public void saltar()
    {
        if(Input.GetKeyDown(KeyCode.W) && GroundCheck)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, fuerza, 0);
            Sprite.GetComponent<Animator>().SetBool("Jumping", true);
        }
       
    }

    ///<summary>
    ///Permite realizar un ataque cuerpo a cuerpo.
    ///</summary>

    public void ataque_cuerpo()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Sprite.GetComponent<Animator>().SetTrigger("atacking");
            Collider[] hitEnemiesL = Physics.OverlapSphere(AttackPointL.transform.position, AttackRange, enemyMask);
            Collider[] hitEnemiesR = Physics.OverlapSphere(AttackPointR.transform.position, AttackRange, enemyMask);
            foreach (Collider enemy in hitEnemiesL)
            {
                if (Sprite.name != "Eva")
                {
                    if (Sprite.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        enemy.GetComponent<Enemy>().recibirdaño(daño);
                    }
                }
                else
                {
                    if (Sprite.GetComponent<SpriteRenderer>().flipX == true)
                    {
                        enemy.GetComponent<Enemy>().recibirdaño(daño);
                    }
                }

            }
            foreach (Collider enemy in hitEnemiesR)
            {
                if (Sprite.name != "Eva")
                {
                    if (Sprite.GetComponent<SpriteRenderer>().flipX == true)
                    {
                        enemy.GetComponent<Enemy>().recibirdaño(daño);
                    }
                }
                else
                {
                    if (Sprite.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        enemy.GetComponent<Enemy>().recibirdaño(daño);
                    }
                }
            }
        }
    }

    ///<summary>
    ///Permite que el personaje pierda vida luego del ataque.
    ///</summary>
    ///<param name="daño">
    ///Daño que recibirá el personaje.
    ///</param>
    ///<param name="posicion_daño">
    ///Posición del enemigo al ejecutar el golpe.
    ///</param>

    public void recibir_daño(int daño,Transform posicion_daño)
    {
        vidaActual = vidaActual - daño;
        barravida.setHealth(vidaActual);
        if (posicion_daño.position.x > gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-6, 4, 0);
            Sprite.GetComponent<Animator>().SetTrigger("hit");
            aux = false;

        }
        if(posicion_daño.position.x < gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(6, 4, 0);
            Sprite.GetComponent<Animator>().SetTrigger("hit");
            aux = false;
        }
    }

    ///<summary>
    ///Realiza un ataque a distancia contra los enemigos.
    ///</summary>

    public void Dispara()
    {
        if (Input.GetKeyDown(KeyCode.Q) && manaActual > 0)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(GameObject.Find("Gun").transform.position, AttackRangeDistance, enemyMask);
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy != null)
                {                    
                    Instantiate(bala, GameObject.Find("Gun").transform.position, Quaternion.Euler(0, 0, 90));
                    manaActual = manaActual - 1;
                    barramana.setmana(manaActual);
                    break;
                }

                
            }
        }
    }

    ///<summary>
    ///Cuando la vida del personaje es 0, permite que este muera.
    ///</summary>

    public void morir()
    {
        if (vidaActual <= 0)
        {
            
            SceneManager.LoadScene(nivel);
        }
        
    }

    ///<summary>
    ///Aumenta el dinero del personaje al entrar en contacto con una moneda.
    ///</summary>
    ///<param name="money">
    ///dinero que recibe al obtener una moneda.
    ///</param>

    public void recibirDinero(int money)
    {
        Debug.Log("recibi dinero");
        Money = Money + money;
        ContadorDinero.text = Money + "";
    }

    ///<summary>
    ///Permite la perpetuación de tu información.
    ///</summary>

    public void guardarJugador()
    {
        SaveSystem.SavePlayer(this);
    }

    ///<summary>
    ///Dibuja una esfera útil para el desarrollo.
    ///</summary>

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

    ///<summary>
    ///Abre la tienda de pociones.
    ///</summary>

    public void Mochila()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (Backpackisopen)
            {
                
                mochila.SetActive(false);
                Backpackisopen = false;
                
            }
            else
            {
                mochila.SetActive(true);
                Backpackisopen = true;
                
            }
        }
    }

    ///<summary>
    ///Eleva la vida del jugador a su máximo valor.
    ///</summary>
    ///<param name="newVidamax">
    ///Vida que será agregada.
    ///</param>
    ///<param name="costo">
    ///Costo de la poción.
    ///</param>

    public void ComprarVidamax(int newVidamax,int costo)
    {
        vida = vida + newVidamax;
        barravida.setmax(vida);
        barravida.setHealth(vidaActual);
        Money = Money - costo;
        ContadorDinero.text = Money + "";
    }

    ///<summary>
    ///Eleva el maná del jugador a su máximo valor.
    ///</summary>
    ///<param name="newManamax">
    ///Maná que será agregado.
    ///</param>
    ///<param name="costo">
    ///Costo de la poción.
    ///</param>

    public void ComprarManamax(int newManamax,int costo)
    {
        mana = mana + newManamax;
        barramana.setmaxmana(mana);
        barramana.setmana(manaActual);
        Money = Money - costo;
        ContadorDinero.text = Money + "";
    }

    ///<summary>
    ///Eleva el daño que causa el jugador.
    ///</summary>
    ///<param name="newDaño">
    ///Valor del daño que será agregado al daño actual.
    ///</param>
    ///<param name="costo">
    ///Costo de la poción.
    ///</param>

    public void ComprarDaño(int newDaño, int costo)
    {
        daño = daño + newDaño;
        Money = Money - costo;
        ContadorDinero.text = Money + "";
    }

    ///<summary>
    ///Aumenta la vida del personaje a la capacidad máxima actual.
    ///</summary>
    ///<param name="costo">
    ///Costo de la poción.
    ///</param>

    public void ComprarRegeneraciónVida( int costo)
    {
        vidaActual = vida;
        barravida.setHealth(vidaActual);
        Money = Money - costo;
        ContadorDinero.text = Money + "";
    }

    ///<summary>
    ///Aumenta el maná del personaje a la capacidad máxima actual.
    ///</summary>
    ///<param name="costo">
    ///Costo de la poción.
    ///</param>

    public void ComprarRegeneraciónMana( int costo)
    {
        manaActual = mana;
        barramana.setmana(manaActual);
        Money = Money - costo;
        ContadorDinero.text = Money + "";
    }
}
