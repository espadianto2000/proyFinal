using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CargaTexto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        transform.GetComponent<TextMeshProUGUI>().text = "Cargando anuncio.";
        yield return new WaitForSeconds(0.7f);
        transform.GetComponent<TextMeshProUGUI>().text = "Cargando anuncio..";
        yield return new WaitForSeconds(0.7f);
        transform.GetComponent<TextMeshProUGUI>().text = "Cargando anuncio...";
        StartCoroutine(Delay());
    }
}
