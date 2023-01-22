using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controllerMenuMuerte : MonoBehaviour
{
    public GameObject botonAnuncio;
    public GameObject botonRevivirGemas;
    public GameObject botonReiniciar;
    public GameObject botonSalir;
    public TextMeshProUGUI tiempo;
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI dinero;
    public TextMeshProUGUI gemas;

    public void mostrardata(string tiempo, string puntos, string dinero, string gemas, int vez)
    {
        if(vez > 1)
        {
            botonAnuncio.SetActive(false);
        }
        botonRevivirGemas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ((int)(50 * Mathf.Pow(2, vez))) + "";
        this.tiempo.text = tiempo;
        this.puntos.text = puntos;
        this.dinero.text = dinero;
        this.gemas.text = gemas;
    }
    public void reiniciarOAcabar()
    {
        gameManager.instance.dinero += int.Parse(dinero.text.Split('$')[0]);
        gameManager.instance.gems += int.Parse(gemas.text);
        gameManager.instance.guardar();
    }
}
