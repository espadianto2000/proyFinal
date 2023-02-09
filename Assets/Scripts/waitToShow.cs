using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitToShow : MonoBehaviour
{
    [SerializeField]
    public GameObject cerrar;
    void Start()
    {
        Invoke("cerrarItem", 4);
        Time.timeScale = 0;
    }

    private void cerrarItem()
    {
        cerrar.SetActive(true);
    }
    public void dest()
    {
        Time.timeScale = 1;

        Destroy(gameObject);

    }
}
