using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null && despierto)
        {

        }
        else
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

    void ataque1()
    {

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
