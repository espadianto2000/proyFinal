using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactus : MonoBehaviour
{
    public float vida;
    public float tiempoVida;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Heroe");
        if(player.GetComponent<PlayerController>().cactusLVL>=1)
        {
            switch(player.GetComponent<PlayerController>().cactusLVL)
            {
                case 1:
                    vida = 50;
                    tiempoVida = 30;
                    break;
                case 2:
                    vida = 60;
                    tiempoVida = 32.5f;
                    break;
                case 3:
                    vida = 70;
                    tiempoVida = 35;
                    break;
                case 4:
                    vida = 80;
                    tiempoVida = 37.5f;
                    break;
                case 5:
                    vida = 90;
                    tiempoVida = 40;
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
        if(tiempoVida>0)
        {
            tiempoVida -= Time.deltaTime;

        }
        if (tiempoVida<=0)
        {
            Destroy(gameObject);
        }
    }
}
