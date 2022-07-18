using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class botonOutTimeScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Color cl = GetComponent<Image>().color;
        cl.a = 0.5f;
        GetComponent<Image>().color = cl;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Color cl = GetComponent<Image>().color;
        cl.a = 0;
        GetComponent<Image>().color = cl;
    }
    public void OnPointerClick(PointerEventData pointerEvent)
    {
        Color cl = GetComponent<Image>().color;
        cl.a = 0;
        GetComponent<Image>().color = cl;
    }
}
