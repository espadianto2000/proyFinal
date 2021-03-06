using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class boton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    public bool desvanecer = true;
    public bool aparecer = false;

    private void FixedUpdate()
    {
        if (desvanecer)
        {
            if (GetComponent<Image>().color.a > 0)
            {
                Color cl = GetComponent<Image>().color;
                cl.a = cl.a - Time.fixedDeltaTime*2;
                GetComponent<Image>().color = cl;
            }
        }
        else if (aparecer)
        {
            if (GetComponent<Image>().color.a < 0.3f)
            {
                Color cl = GetComponent<Image>().color;
                cl.a = cl.a + Time.fixedDeltaTime*2;
                GetComponent<Image>().color = cl;
            }
        }
    }
    private void Update()
    {
        if(transform.tag == "boton2")
        {
            if (GameObject.Find("gameManager").GetComponent<gameManager>().desbloquearPersonaje2)
            {
                GetComponent<Button>().interactable = true;
            }
            else
            {
                GetComponent<Button>().interactable = false;
                Color cl = GetComponent<Image>().color;
                cl.a = 0.5f;
                GetComponent<Image>().color = cl;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (GetComponent<Button>().interactable)
        {
            aparecer = true;
            desvanecer = false;
        }
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (GetComponent<Button>().interactable)
        {
            aparecer = false;
            desvanecer = true;
        }
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        if(transform.tag == "boton1")
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().empezarPartida(1);
        }else if (transform.tag == "boton2" && GetComponent<Button>().interactable)
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().empezarPartida(2);
        }
    }
}
