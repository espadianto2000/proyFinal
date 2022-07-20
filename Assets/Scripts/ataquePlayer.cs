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
        pc = GameObject.Find("Heroe").GetComponent<PlayerController>();
        if(transform.name == "flecha(Clone)")
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "RecibeDano" && collision.name == "AreaRecibeDaņo")
        {
            //Debug.Log("ataque");
            collision.GetComponentInParent<enemyController>().vida -= pc.DanoBase * pc.multiplicadorDanoUlti;
            //collision.GetComponentInParent<enemyController>().efectoFlash();
            //GameObject obj = Instantiate(collision.GetComponentInParent<enemyController>().hm, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            //obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pc.DanoBase * pc.multiplicadorDanoUlti + "";
            if (pc.roboVidaLVL>=1)
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
