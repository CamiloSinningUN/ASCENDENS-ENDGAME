using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidad;
    public float propulcion;
    public int vida;

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
        if (vida <= 0)
        {
            GameObject.Find("Enemigo").GetComponent<Animator>().SetBool("live", false);
            velocidad = 0;
            gameObject.tag = "piso";
            gameObject.GetComponent<BoxCollider>().center = new Vector3(1, -0.5f, 0);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 0, 0);
            GameObject.Find("izquierdo").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("derecho").GetComponent<BoxCollider>().enabled = false;

        }
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
        if (other.gameObject.tag == "espada")
        {
            GameObject.Find("Enemigo").GetComponent<Animator>().SetTrigger("hit");
            vida -= 2;
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

}
