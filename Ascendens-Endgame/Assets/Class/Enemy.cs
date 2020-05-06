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
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(5,5,0);
        
    }
    // Update is called once per frame
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "esquina")
        {
            aux = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        
        if (other.gameObject.tag == "esquina")
        {
            aux = false;
            propulcion = float.Parse(other.gameObject.name);
            Sprite.GetComponent<Animator>().SetTrigger("jumping");
        }
        
    }
    public void salto()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
    }
    public void mover()
    {
        Sprite.GetComponent<Animator>().SetBool("moving", true);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y + propulcion, 0);
    }
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
    public void morir()
    {
        velocidad = 0;
        Sprite.GetComponent<Animator>().SetBool("live", false);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        gameObject.GetComponent<Enemy>().enabled = false;
        
    }
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


    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, AttackRange);
        Gizmos.DrawWireSphere(FollowPoint.transform.position, FollowRange);
    }

}
