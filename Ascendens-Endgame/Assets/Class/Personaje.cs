using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

}

public class salto : MonoBehaviour
{
    public GameObject sprite;
    public Rigidbody body;
    public bool plataforma;
    public float fuerza;
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.W) && other.tag == "piso")
        {
            body.velocity = new Vector3(body.velocity.x, fuerza, 0);
            sprite.GetComponent<Animator>().SetBool("Jumping", true);
        }
        else if (other.tag == "piso")
        {
            sprite.GetComponent<Animator>().SetBool("Jumping", false);
        }
        if (other.tag == "piso")
        {
            plataforma = true;
        }
        if (!(other.tag == "piso"))
        {
            plataforma = false;
        }
    }
}

public class Player : MonoBehaviour
{
    public int Dinero;
    public GameObject bala, disparador, sprite, golpeI, golpeD;
    public estilo pos;
    public colisionI LI;
    public colisionZ LD;
    public bool cortar, empuje;
    public colisionI.datos inve;
    public salto placa;
    public float velocidad = 2f, corte = 0, empujeL, empujeA, i;
    Rigidbody body;
    public int direccion;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(0, 0, 0);
        golpeI.SetActive(false);
        golpeD.SetActive(false);
        empuje = false;
    }

    // Update is called once per frame
    void Update()
    {
        cuerpo();
        movimiento();
        disparo();
    }

    void disparo()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            estilo pos = bala.GetComponent<estilo>();
            pos.direccion = direccion;
            Instantiate(bala, disparador.transform.position, Quaternion.Euler(0, 0, 90));
        }
        if (LD.recibirD || LI.recibirD)
        {
            i += 0.1f * Time.deltaTime;
            if (placa.plataforma == true & i > 0.05)
            {
                i = 0;
                LD.recibirD = false;
                LI.recibirD = false;
                empuje = false;
                sprite.GetComponent<Animator>().SetBool("daño", false);

            }
        }
    }
    void cuerpo()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sprite.GetComponent<Animator>().SetBool("atacking", true);

            cortar = true;

        }

        if (cortar & direccion == -1)
        {
            corte += 0.1f * Time.deltaTime;
            if (corte < 0.055 & corte > 0.02)
            {
                golpeI.SetActive(true);
            }
            if (corte > 0.055)
            {
                cortar = false;
                golpeI.SetActive(false);
                sprite.GetComponent<Animator>().SetBool("atacking", false);

                corte = 0;
            }
        }
        if (cortar & direccion == 1)
        {
            corte += 0.1f * Time.deltaTime;
            if (corte < 0.055 & corte > 0.02)
            {
                golpeD.SetActive(true);
            }
            if (corte > 0.055)
            {
                cortar = false;
                golpeD.SetActive(false);
                sprite.GetComponent<Animator>().SetBool("atacking", false);

                corte = 0;
            }
        }
    }
    void movimiento()
    {
        if (!LD.recibirD & !LI.recibirD)
        {
            if (Input.GetKey(KeyCode.D) & LD.De)
            {
                body.velocity = new Vector3(velocidad, body.velocity.y, 0);
                sprite.GetComponent<Animator>().SetBool("Moving", true);
                sprite.GetComponent<SpriteRenderer>().flipX = false;
                direccion = 1;
            }
            if (Input.GetKey(KeyCode.A) & LI.iZ)
            {
                body.velocity = new Vector3(-velocidad, body.velocity.y, 0);
                sprite.GetComponent<Animator>().SetBool("Moving", true);
                sprite.GetComponent<SpriteRenderer>().flipX = true;
                direccion = -1;
            }
            if (!Input.GetKey("a") && !Input.GetKey("d"))
            {
                sprite.GetComponent<Animator>().SetBool("Moving", false);
            }
        }
        else
        {


            if (!empuje)
            {
                if (LD.recibirD)
                {
                    body.velocity = new Vector3(-empujeL, empujeA, 0);
                    inve.vida--;
                }
                if (LI.recibirD)
                {
                    body.velocity = new Vector3(empujeL, empujeA, 0);
                    inve.vida--;
                }
                sprite.GetComponent<Animator>().SetBool("daño", true);
                empuje = true;
            }
            else
            {
                body.velocity = new Vector3(body.velocity.x, body.velocity.y, 0);
            }

        }

    }


}

public class estilo : MonoBehaviour
{
    private Rigidbody body;
    public float t;
    public int direccion;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direccion == 0)
        {
            direccion = 1;
        }
        body.velocity = new Vector3(direccion * 10, 0, 0);
        t += Time.deltaTime;
        if (t > 10)
        {
            Destroy(gameObject);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" & !other.isTrigger)
        {
            Destroy(gameObject);
            Debug.Log(other.tag);
        }


    }
}

public class colisionZ : MonoBehaviour
{
    // Start is called before the first frame update
    public bool De, recibirD;
    // Update is called once per frame

    void Start()
    {
        De = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "piso")
        {
            De = false;
        }

        if (other.tag == "enemigo")
        {
            recibirD = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "piso")
        {
            De = true;
        }

    }
}

public class colisionI : MonoBehaviour
{

    public bool iZ, recibirD;
    // Update is called once per frame
    void Start()
    {
        iZ = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "piso")
        {
            iZ = false;
        }
        if (other.tag == "enemigo")
        {
            recibirD = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "piso")
        {
            iZ = true;
        }

    }

    public class datos : MonoBehaviour
    {
        public int dinero, cantidadFlechas, vida, vidaMax = 15;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (vidaMax < vida)
            {
                vida = vidaMax;
            }
            if (vida <= 0)
            {
                //muerte 
            }
        }
    }
}
