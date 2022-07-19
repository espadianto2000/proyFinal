using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaScript : MonoBehaviour
{
    public GameObject player;
    public Collider2D col1;
    public Collider2D col2;
    public Collider2D col3;
    public Collider2D col4;
    public Collider2D col5;

    public float tiempoExplosion = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Heroe");
        
        switch(player.GetComponent<PlayerController>().bombaLVL)
        {
            case 1:
                Invoke("explo1", 2f);
                break;
            case 2:
                Invoke("explo2", 2f);
                break;
            case 3:
                Invoke("explo3", 2f);
                break;
            case 4:
                Invoke("explo4", 2f);
                break;
            case 5:
                Invoke("explo5", 2f);
                break;

        }


    }

    // Update is called once per frame
    void Update()
    {
    }
    void explo1()
    {
        transform.GetChild(0).GetComponent<Animator>().SetInteger("explotar", 1);
        col1.enabled = true;
        Invoke("desaparecerCollider", 0.3f);
    }
    void explo2()
    {
        transform.GetChild(0).GetComponent<Animator>().SetInteger("explotar", 1);
        col2.enabled = true;
        Invoke("desaparecerCollider", 0.3f);

    }
    void explo3()
    {
        transform.GetChild(0).GetComponent<Animator>().SetInteger("explotar", 1);
        col3.enabled = true;
        Invoke("desaparecerCollider", 0.3f);


    }
    void explo4()
    {
        transform.GetChild(0).GetComponent<Animator>().SetInteger("explotar", 1);
        col4.enabled = true;
        Invoke("desaparecerCollider", 0.3f);


    }
    void explo5()
    {
        transform.GetChild(0).GetComponent<Animator>().SetInteger("explotar", 1);
        col5.enabled = true;
        Invoke("desaparecerCollider", 0.3f);


    }
    void desaparecerCollider()
    {
        col1.enabled = false;
        col2.enabled = false;
        col3.enabled = false;
        col4.enabled = false;
        col5.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            switch(player.GetComponent<PlayerController>().bombaLVL)
            {
                case 1:
                    collision.gameObject.GetComponent<enemyController>().vida -= 10;
                    Debug.Log("explo hizo 10 de daño");
                    break;
                case 2:
                    collision.gameObject.GetComponent<enemyController>().vida -= 20;
                    Debug.Log("explo hizo 20 de daño");

                    break;
                case 3:
                    collision.gameObject.GetComponent<enemyController>().vida -= 30;
                    Debug.Log("explo hizo 30 de daño");

                    break;
                case 4:
                    collision.gameObject.GetComponent<enemyController>().vida -= 40;
                    Debug.Log("explo hizo 40 de daño");

                    break;
                case 5:
                    collision.gameObject.GetComponent<enemyController>().vida -= 50;
                    Debug.Log("explo hizo 50 de daño");

                    break;

            }
            collision.gameObject.GetComponent<enemyController>().efectoFlash();
        }
    }

}
