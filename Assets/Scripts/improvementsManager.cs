using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class improvementsManager : MonoBehaviour
{
    public PlayerController pc;
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

    }
}
