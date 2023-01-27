using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateThings : MonoBehaviour
{
    public gameManager gm;
    public GameObject dinero;
    public GameObject gemas;
    public GameObject maxPts;
    public GameObject premium;
    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.instance;
        updateThingsFunc();
    }
    public void updateThingsFunc()
    {
        dinero.GetComponent<TextMeshProUGUI>().text = gm.dinero + "";
        gemas.GetComponent<TextMeshProUGUI>().text = gm.gems + "";
        maxPts.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(gm.highScore * 100) / 100) + "";
    }
    private void Update()
    {
        if (gm.premium)
        {
            premium.SetActive(true);
        }
        else
        {
            premium.SetActive(false);
        }
    }
    public void salir()
    {
        Application.Quit();
    }
}
