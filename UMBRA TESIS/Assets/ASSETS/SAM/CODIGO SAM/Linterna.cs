using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light flashlight;

    void Start()
    {
        flashlight.enabled = false;
        OcultarTodos();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        BuscarObjetos();
    }

    void BuscarObjetos()
    {
        ObjetoRevelable[] objetos = FindObjectsOfType<ObjetoRevelable>();

        foreach (ObjetoRevelable objeto in objetos)
        {
            Vector3 direccion = objeto.transform.position - flashlight.transform.position;

            float distancia = direccion.magnitude;

            float angulo = Vector3.Angle(
                flashlight.transform.forward,
                direccion
            );

            if (flashlight.enabled &&
                distancia <= flashlight.range &&
                angulo <= flashlight.spotAngle / 2f)
            {
                objeto.Revelar();
            }
            else
            {
                objeto.Ocultar();
            }
        }
    }

    void OcultarTodos()
    {
        ObjetoRevelable[] objetos = FindObjectsOfType<ObjetoRevelable>();

        foreach (ObjetoRevelable objeto in objetos)
        {
            objeto.Ocultar();
        }
    }
}