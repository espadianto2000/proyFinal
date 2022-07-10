using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataquePlayer : MonoBehaviour
{
    public Collider2D areaAtaque;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RecibeDaņo")
        {
            Debug.Log("ataque");
            collision.GetComponentInParent<enemyController>().vida -= 1;
        }
        
    }
    private void activarSlash()
    {
        areaAtaque.enabled = true;
    }
    private void desactivarSlash()
    {
        areaAtaque.enabled = false;
    }
}
