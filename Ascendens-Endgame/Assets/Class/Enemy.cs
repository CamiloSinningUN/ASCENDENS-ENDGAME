using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
///Clase enemigos.
///</summary>
///<remarks>
///Da atributos y funciones a los enemigos.
///</remarks>


public class Enemy : MonoBehaviour
{
    public float velocidad;
    public float propulcion;
    public int daño;
    public int vida=3;
    public float AttackRange = 0.5f;
    public float FollowRange = 2f;
    public LayerMask PersonLayer;
    public GameObject AttackPoint;
    public GameObject FollowPoint;
    public GameObject Sprite;

    public bool aux=false;

    ///<summary>
    ///Se llama al iniciar.
    ///</summary>
    
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(5,5,0);
        
    }

    ///<summary>
    ///Se llama cada frame.
    ///</summary>

    void Update()
    {
        if (aux)
        {
            salto();
        }
        
        if (!Sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("attack") && !Sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("hurt"))
        {
            
            Ataque_Cuerpo();
        }
        seguir();
        
        
        
    }

    ///<summary>
    ///Este es llamado cuando el collider deja de tocar el trigger del objeto.
    ///</summary>
    ///<param name="other">
    ///Es el collider que detecta el trigger.
    ///</param>

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "esquina")
        {
            aux = true;
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
        
        if (other.gameObject.tag == "esquina")
        {
            aux = false;
            propulcion = float.Parse(other.gameObject.name);
            Sprite.GetComponent<Animator>().SetTrigger("jumping");
        }
        
    }

    ///<summary>
    ///Permite el salto del personaje.
    ///</summary>

    public void salto()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
    }

    ///<summary>
    ///Hace posible el movimiento del personaje.
    ///</summary>

    public void mover()
    {
        Sprite.GetComponent<Animator>().SetBool("moving", true);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y + propulcion, 0);
    }

    ///<summary>
    ///Permite que el enemigo pierda vida luego del ataque.
    ///</summary>
    ///<param name="daño">
    ///Daño que recibirá el enemigo.
    ///</param>

    public void recibirdaño(int daño)
    {
        Debug.Log("recibi daño: " + daño);
           
        vida = vida-daño;
        Sprite.GetComponent<Animator>().SetTrigger("hit");
        if (vida <= 0)
        {
            morir();
           
        }
    }

    ///<summary>
    ///Cuando la vida del enemigo es 0, permite que este muera.
    ///</summary>

    public void morir()
    {
        velocidad = 0;
        Sprite.GetComponent<Animator>().SetBool("live", false);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        gameObject.GetComponent<Enemy>().enabled = false;
        
    }

    ///<summary>
    ///Permite realizar un ataque cuerpo a cuerpo.
    ///</summary>

    public void Ataque_Cuerpo()
    {
        Collider[] hitpersons = Physics.OverlapSphere(AttackPoint.transform.position, AttackRange, PersonLayer);
        foreach (Collider person in hitpersons)
        {
            if (person != null)
            {
                if (person.tag == "Player")
                {
                    
                    Sprite.GetComponent<Animator>().SetTrigger("atack");
                    person.GetComponent<Avatar>().recibir_daño(daño, gameObject.transform);
                    break;
                }


            }
            

        }
    }

    ///<summary>
    ///Permite que el enemigo siga al personaje cuando se encuentra en su campo de visión.
    ///</summary>

    public void seguir()
    {
        Collider[] hitpersons = Physics.OverlapSphere(FollowPoint.transform.position, FollowRange, PersonLayer);
       
        foreach (Collider person in hitpersons)
        {
            
            if (person != null)
            {
                if (person.tag == "Player")
                {
                    Sprite.GetComponent<Animator>().SetBool("moving", true);
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y + propulcion, 0);
                    if (person.transform.position.x > gameObject.transform.position.x)
                    {
                        if (Sprite.GetComponent<SpriteRenderer>().flipX == true)
                        {
                            Sprite.GetComponent<SpriteRenderer>().flipX = false;
                            if (Sprite.name != "Boss")
                            {
                                Sprite.transform.Translate(new Vector3(2, 0, 0));
                            }
                            
                            velocidad = -velocidad;
                        }

                    }
                    else if(person.transform.position.x< gameObject.transform.position.x)
                    {
                        if(Sprite.GetComponent<SpriteRenderer>().flipX == false)
                        {
                            Sprite.GetComponent<SpriteRenderer>().flipX = true;
                            if (Sprite.name != "Boss")
                            {
                                Sprite.transform.Translate(new Vector3(-2, 0, 0));
                            }
                                
                            velocidad = -velocidad;

                        }
                        
                    }
                }


            }
           

        }
        
    }

    ///<summary>
    ///Dibuja una esfera útil para el desarrollo.
    ///</summary>

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, AttackRange);
        Gizmos.DrawWireSphere(FollowPoint.transform.position, FollowRange);
    }

}
