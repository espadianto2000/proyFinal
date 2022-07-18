using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class interfazInGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float puntos = 0;
    public gameManager gm;
    public TextMeshProUGUI displayPuntos;
    void Start()
    {
        if (GameObject.Find("gameManager"))
        {
            gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        displayPuntos.text =  (Mathf.Round(puntos*100))/100 + "";
    }
    public void reiniciar()
    {
        gm.reiniciarPartida();
    }
    public void salir()
    {
        gm.irMenuPrincipal();
    }
    public void salirPausa()
    {
        gm.outPausa();
    }
}
