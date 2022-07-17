using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateThings : MonoBehaviour
{
    public gameManager gm;
    public GameObject dinero;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        dinero.GetComponent<TextMeshProUGUI>().text = gm.dinero + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
