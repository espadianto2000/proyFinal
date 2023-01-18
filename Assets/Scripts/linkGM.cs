using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkGM : MonoBehaviour
{
    public GameObject menuPausa;

    private void Start()
    {
        gameManager.instance.menuPausa = menuPausa;
    }
    public void reanudar()
    {
        gameManager.instance.audioClick.Play();
        gameManager.instance.outPausa();
    }
    public void reiniciar()
    {
        gameManager.instance.audioClick.Play();
        gameManager.instance.reiniciarPartida();
        gameManager.instance.outPausa();
    }
    public void salir()
    {
        gameManager.instance.acabarPartida();
        gameManager.instance.forceOutPausa();
    }
}
