using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayerMenu : MonoBehaviour
{
    public void acabarIdle()
    {
        GetComponent<Animator>().SetInteger("cuentas", GetComponent<Animator>().GetInteger("cuentas")+1);
    }
    public void ataq()
    {
        GetComponent<Animator>().SetInteger("cuentas", 0);
    }
}
