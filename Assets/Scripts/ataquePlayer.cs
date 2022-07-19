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
            if(pc.roboVidaLVL>=1)
            {
                switch(pc.roboVidaLVL)
                {
                    case 1:
                        if (pc.roboVida && Random.value <= 0.05f)
                        {
                            pc.curar(pc.DanoBase * 0.3f);

                        }
                        break;
                    case 2:
                        if (pc.roboVida && Random.value <= 0.08f)
                        {
                            pc.curar(pc.DanoBase * 0.35f);

                        }
                        break;
                    case 3:
                        if (pc.roboVida && Random.value <= 0.11f)
                        {
                            pc.curar(pc.DanoBase * 0.4f);

                        }
                        break;
                    case 4:
                        if (pc.roboVida && Random.value <= 0.14f)
                        {
                            pc.curar(pc.DanoBase * 0.45f);

                        }
                        break;
                    case 5:
                        if (pc.roboVida && Random.value <= 0.17f)
                        {
                            pc.curar(pc.DanoBase * 0.5f);

                        }
                        break;

                }
                
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
