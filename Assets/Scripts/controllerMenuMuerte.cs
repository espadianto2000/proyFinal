using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

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
    private int vez;
    [SerializeField]
    private GameObject compra200;
    [SerializeField]
    private GameObject compra500;
    [SerializeField]
    private GameObject compra1000;
    [SerializeField]
    private GameObject compra5000;

    public void mostrardata(string tiempo, string puntos, string dinero, string gemas, int vez)
    {
        compra200.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems+"";
        compra500.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        compra1000.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        compra5000.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        if (vez > 1)
        {
            botonAnuncio.SetActive(false);
            if(vez > 4)
            {
                botonRevivirGemas.SetActive(false);
            }
        }
        this.vez = vez;
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
    public void intentarRevivirGemas()
    {
        if (gameManager.instance.gems >= (int)(50 * Mathf.Pow(2, vez))) 
        {
            gameManager.instance.gems -= (int)(50 * Mathf.Pow(2, vez));
            GameObject.Find("Heroe").GetComponent<PlayerController>().revivir();
        }
        else
        {
            if((int)(50 * Mathf.Pow(2, vez)) - gameManager.instance.gems <= 200)
            {
                compra200.SetActive(true);
            }else if ((int)(50 * Mathf.Pow(2, vez)) - gameManager.instance.gems <= 500)
            {
                compra500.SetActive(true);
            }
            else if ((int)(50 * Mathf.Pow(2, vez)) - gameManager.instance.gems <= 1000)
            {
                compra1000.SetActive(true);
            }
            else if ((int)(50 * Mathf.Pow(2, vez)) - gameManager.instance.gems <= 5000)
            {
                compra5000.SetActive(true);
            }
        }
    }
    public void comprarGemas(int valor)
    {
        gameManager.instance.gems += valor;
        gameManager.instance.guardar();
        compra200.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        compra500.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        compra1000.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        compra5000.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.instance.gems + "";
        switch (valor)
        {
            case 200:
                StartCoroutine(cerrarVentana(compra200));
                break;
            case 500:
                StartCoroutine(cerrarVentana(compra500));
                break;
            case 1000:
                StartCoroutine(cerrarVentana(compra1000));
                break;
            case 5000:
                StartCoroutine(cerrarVentana(compra5000));
                break;
        }
    }
    IEnumerator cerrarVentana(GameObject ventana)
    {
        Thread.Sleep(2000);
        ventana.SetActive(false);
        yield return null;
    }

}
