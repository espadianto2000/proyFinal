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

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        aparecer = true;
        desvanecer=false;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        aparecer=false;
        desvanecer=true;
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        if(transform.tag == "boton1")
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().empezarPartida(1);
        }else if (transform.tag == "boton2")
        {
            GameObject.Find("gameManager").GetComponent<gameManager>().empezarPartida(2);
        }
    }
}
