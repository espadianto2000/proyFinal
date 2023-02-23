using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class blueMageController : MonoBehaviour
{

    public float vida;
    public float velocidad;
    public GameObject player;
    public bool vulnerable = false;
    public bool despierto = false;
    [SerializeField]
    private Collider2D trigg;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private NavMeshAgent navAg;
    [SerializeField]
    private float tiempoEntreAtaques,tiempoMaxEntreAtaques;
    [SerializeField]
    private bool atacando = false;
    public int estado = 0;
    private float tiempoInterAtaque = 1f;
    public GameObject proy1;
    Vector3 posEstatico;


    private void Start()
    {
        navAg.updateRotation = false;
        navAg.updateUpAxis = false;
        navAg.speed = velocidad;
        navAg.angularSpeed = velocidad * 5;
        navAg.acceleration = velocidad * 3;
    }
    void FixedUpdate()
    {
        if(player != null && despierto)
        {
            navAg.speed = velocidad;
            navAg.angularSpeed = velocidad * 5;
            navAg.acceleration = velocidad * 3;
            navAg.SetDestination(player.transform.position);
            if (tiempoEntreAtaques > 0 && !atacando)
            {
                navAg.isStopped = false;
                tiempoEntreAtaques -= Time.fixedDeltaTime;
                estado = 0;
            }
            else if(!atacando)
            {
                posEstatico = transform.position;
                navAg.isStopped = true;
                navAg.velocity = Vector3.zero;
                atacando = true;
                tiempoEntreAtaques = tiempoMaxEntreAtaques;
                switch (Random.Range(0, 6))
                {
                    case 0:
                    case 3:
                        estado = 1;
                        ataque1();
                        Invoke("acabarAtaque", 7f);
                        break;
                    case 1:
                    case 4:
                        estado = 2;
                        ataque1();
                        Invoke("acabarAtaque", 7f);
                        break;
                    case 2:
                    case 5:
                        estado = 3;
                        ataque1();
                        Invoke("acabarAtaque", 7f);
                        break;
                }
            }

        }
        else if(player == null)
        {
            try
            {
                player = GameObject.Find("Heroe");
            }catch
            {
                Debug.Log("Cant find hero");
            }
            
        }
    }
    private void Update()
    {
        switch (estado)
        {
            case 0:
                break;
            case 1:
                if(tiempoInterAtaque>0)
                {
                    transform.position = posEstatico;
                    tiempoInterAtaque -= Time.deltaTime;
                }
                else
                {
                    tiempoInterAtaque = 1f;
                    Instantiate(proy1, player.transform.position, Quaternion.identity);
                }
                break;
        }
    }
    void acabarAtaque()
    {
        estado = 0;
        atacando = false;
    }

    void ataque1()
    {
        estado = 1;
    }

    void ataque2()
    {

    }

    void ataque3()
    {

    }
    void despertar()
    {
        animator.SetTrigger("despertar");
    }
    public void finDespertar()
    {
        vulnerable = true;
        Invoke("waitDespertar", 2f);
    }
    void waitDespertar()
    {
        despierto = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Heroe")
        {
            trigg.enabled = false;
            despertar();
        }
    }
}
