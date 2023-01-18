using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateThings : MonoBehaviour
{
    public gameManager gm;
    public GameObject dinero;
    public GameObject maxPts;
    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.instance;
        dinero.GetComponent<TextMeshProUGUI>().text = gm.dinero + "$";
        maxPts.GetComponent<TextMeshProUGUI>().text = (Mathf.Round(gm.highScore*100)/100) + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void salir()
    {
        Application.Quit();
    }
}
