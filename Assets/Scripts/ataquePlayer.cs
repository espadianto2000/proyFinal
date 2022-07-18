using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ataquePlayer : MonoBehaviour
{
    public Collider2D areaAtaque;
    public PlayerController pc;
    public GameObject hmHeal;

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
        if(collision.tag == "RecibeDano")
        {
            Debug.Log("ataque");
            collision.GetComponentInParent<enemyController>().vida -= pc.DanoBase;
            if(pc.roboVida && Random.value<=0.05f)
            {
                pc.curar(pc.DanoBase*0.3f);
                
            }
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
