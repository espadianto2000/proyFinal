using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class improvementsManager : MonoBehaviour
{
    public PlayerController pc;
    public GameObject[] posiblesMejoras;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void mejorar(int orden)
    {
        switch (orden) { 
            case 0:
                pc.nivelVidaMejora++;
                pc.vidaMax += 10;
                pc.vidaActual += 10;
                break;
            case 1:
                pc.nivelDanoMejora++;
                pc.DanoBase=pc.DanoBase * 1.08f;
                break;
            case 2:
                pc.nivelVelocidadMejora++;
                pc.VelocidadBase = pc.VelocidadBase * 1.05f;
                break;
            case 3:
                pc.nivelCriticoMejora++;
                pc.probCritico += 0.03f;
                pc.probCritico = pc.probCritico > 1 ? 1 : pc.probCritico;
                break;
            case 4:
                pc.nivelVelocidadAtaqueMejora++;
                pc.velocidadAtaque= pc.velocidadAtaque * 0.95f;
                pc.velocidadAtaque = pc.velocidadAtaque < 0.3f ? 0.3f : pc.velocidadAtaque;
                break;
            case 5:
                pc.nivelAlcanceMejora++;
                pc.tamanoAtaque = pc.tamanoAtaque * 1.1f;
                if(SceneManager.GetActiveScene().name == "jugador1" && pc.slash.transform.localScale.x < pc.tamanoAtaque)
                {
                    pc.slash.transform.localScale = new Vector3(pc.tamanoAtaque, pc.tamanoAtaque, 1);
                }
                break;
            case 6:
                pc.nivelXPMejora++;
                pc.porcentajeExp += 0.05f;
                break;
            case 7:
                pc.nivelPtsMejora++;
                pc.porcentajePuntos += 0.05f;
                break;
            case 8:
                pc.nivelDineroMejora++;
                pc.porcentajeDinero += 0.05f;
                break;
        }
    }
    public void mejorarItem(int orden)
    {
        switch (orden)
        {
            case 0:
                pc.armEspLVL++;
                pc.armaduraEspinas = true;
                break;
            case 1:
                pc.roboVidaLVL++;
                pc.roboVida = true;
                break;
            case 2:
                pc.escudoProtLVL++;
                pc.escudoProtector = true;
                break;
            case 3:
                pc.escudoInmLVL++;
                pc.escudoInmortal = true;
                break;
            case 4:
                pc.invLVL++;
                pc.invisibilidad = true;
                break;
            case 5:
                pc.dagaLVL++;
                pc.dagaPoder = true;
                break;
            case 6:
                pc.evasionLVL++;
                pc.evadirAtaque = true;
                break;
            case 7:
                pc.cactusLVL++;
                pc.cactusPoder = true;
                break;
            case 8:
                pc.bombaLVL++;
                pc.bombaPoder = true;
                break;
        }
    }
    public void generarMejoras()
    {
        if (transform.GetChild(3)!=null)
        {
            Destroy(transform.GetChild(3).gameObject);
        }
        if (transform.GetChild(2) != null)
        {
            Destroy(transform.GetChild(2).gameObject);
        }
        if (transform.GetChild(1) != null)
        {
            Destroy(transform.GetChild(1).gameObject);
        }
        if (transform.GetChild(0) != null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        for (int i = 0; i<4; i++)
        {
            int index = Random.Range(0, posiblesMejoras.Length);
            Instantiate(posiblesMejoras[index], transform);
        }
    }
}
