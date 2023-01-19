using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemBuys : MonoBehaviour
{
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
}
