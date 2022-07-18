using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactus : MonoBehaviour
{
    public float vida = 50;
    public float tiempoVida = 30;
    // Start is called before the first frame update
    void Start()
    {
        
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
