using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidad;
    public float propulcion;
    public int vida=100;
    public float AttackRange = 0.5f;
    public LayerMask PersonLayer;
    public int daño;

    public bool aux;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(5,5,0);        
        aux = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (aux)
        {
            salto();
        }
        else
        {
            mover();
        }
        Ataque_Cuerpo();
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
        if (other.gameObject.tag == "bala")
        {
            GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("hit");
            vida--;
        }
        
        if (other.gameObject.tag == "esquina")
        {
            aux = false;
            propulcion = float.Parse(other.gameObject.name);
            GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("jumping");
        }
        //que paresca que le pega cuando se acerca
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("atack");
        }
        if (!other.isTrigger)
        {
            velocidad = -velocidad;
            GameObject.Find("Enemigo").GetComponent<SpriteRenderer>().flipX=true;
        }
    }
    public void salto()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
    }
    public void mover()
    {
        GameObject.Find("Enemigo").GetComponent<Animator>().SetBool("moving", true);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocidad, gameObject.GetComponent<Rigidbody>().velocity.y + propulcion, 0);
    }
    public void recibirdaño(int daño)
    {
        vida = vida-daño;
        GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("hit");
        if (vida <= 0)
        {
            morir();
           
        }
    }
    public void morir()
    {
        GameObject.Find("Enemigo").GetComponent<Animator>().SetBool("live", false);
        velocidad = 0;
        gameObject.tag = "piso";
        gameObject.GetComponent<BoxCollider>().center = new Vector3(1, -0.5f, 0);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0, 0);
    }
    public void Ataque_Cuerpo()
    {                 
            Collider[] hitpersons = Physics.OverlapSphere(GameObject.Find("AttackPointE").transform.position, AttackRange, PersonLayer);
            foreach (Collider person in hitpersons)
            {
            Debug.Log(person.name);
            if (person != null)
            {
                GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("atack");
                person.GetComponent<Avatar>().recibir_daño(daño,gameObject.transform);
            }
            
            }      
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GameObject.Find("AttackPointE").GetComponent<Transform>().position, AttackRange);
    }

}
