using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mejoraBoton : MonoBehaviour
{
    public int valorOrden;
    public bool item;
    public GameObject valor;
    private void Start()
    {
        switch (valorOrden)
        {
            case 0:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.armEspLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelVidaMejora + "";
                }
                break;
            case 1:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.roboVidaLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelDanoMejora + "";
                }
                break;
            case 2:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.escudoProtLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelVelocidadMejora + "";
                }
                break;
            case 3:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.escudoInmLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelCriticoMejora + "";
                }
                break;
            case 4:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.invLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelVelocidadAtaqueMejora + "";
                }
                break;
            case 5:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.dagaLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelAlcanceMejora + "";
                }
                break;
            case 6:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.evasionLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelXPMejora + "";
                }
                break;
            case 7:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.cactusLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelPtsMejora + "";
                }
                break;
            case 8:
                if (item)
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.bombaLVL + "";
                }
                else
                {
                    valor.GetComponent<TextMeshProUGUI>().text = transform.parent.GetComponent<improvementsManager>().pc.nivelDineroMejora + "";
                }
                break;
        }
    }
    public void mejorarStat()
    {
        transform.parent.GetComponent<improvementsManager>().mejorar(valorOrden);
        transform.parent.parent.gameObject.SetActive(false);
        transform.parent.parent.parent.GetComponent<interfazInGameManager>().salirPausa();
        if (WaveManager.instance.mostrarAnuncio && !gameManager.instance.premium)
        {
            //WaveManager.instance.mostrarAnuncio = false;
            //mostrar anuncio
            LogicaAds.instance.MostrarInters();
            Debug.Log("se muestra anuncio");
            //AQUIIIIIIII//
            ///////
            ///////
            ///////
        }
    }
    public void mejorarItem()
    {
        transform.parent.GetComponent<improvementsManager>().mejorarItem(valorOrden);
        transform.parent.parent.parent.GetComponent<interfazInGameManager>().salirPausa();
        transform.parent.parent.gameObject.SetActive(false);
        if (WaveManager.instance.mostrarAnuncio && !gameManager.instance.premium)
        {
            //WaveManager.instance.mostrarAnuncio = false;
            //mostrar anuncio
            LogicaAds.instance.MostrarInters();
            Debug.Log("se muestra anuncio");
            //AQUIIIIIIII//
            ///////
            ///////
            ///////
        }
    }
}
