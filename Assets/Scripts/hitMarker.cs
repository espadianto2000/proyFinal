using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hitMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("desaparecer",0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z); 
    }
    public void desaparecer()
    {
        Destroy(gameObject);
    }
}
