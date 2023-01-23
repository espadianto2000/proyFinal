using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class gemBuys : MonoBehaviour
{
    public GameObject premium;
    public void compraGemas(string costoPremio)
    {
        string[] temp = costoPremio.Split(',');
        int costo = int.Parse(temp[0]);
        int premio = int.Parse(temp[1]);
        if (gameManager.instance.gems >= costo)
        {
            gameManager.instance.gems -= costo;
            gameManager.instance.dinero += premio;
        }
    }
    public void recompensa(int gemas)
    {
        gameManager.instance.gems += gemas;
    }
    public void comprarPremium()
    {
        gameManager.instance.premium = true;
        premium.GetComponent<Button>().interactable = false;
        premium.GetComponent<IAPButton>().enabled = false;
    }
    private void Start()
    {
        if (gameManager.instance.premium)
        {
            premium.GetComponent<Button>().interactable = false;
            premium.GetComponent<IAPButton>().enabled = false;
        }
    }
}
