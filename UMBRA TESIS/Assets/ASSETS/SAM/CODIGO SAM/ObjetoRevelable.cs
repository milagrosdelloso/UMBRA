using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRevelable : MonoBehaviour
{
    public GameObject objetoARevelar;

    void Start()
    {
        objetoARevelar.SetActive(false);
    }

    public void Revelar()
    {
        objetoARevelar.SetActive(true);
    }

    public void Ocultar()
    {
        objetoARevelar.SetActive(false);
    }
}