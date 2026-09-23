using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light flashlight;

    private Inventory inventory;

    void Start()
    {
        flashlight.enabled = false;
        OcultarTodos();

        inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("No se encontró el Inventory.");
        }
    }

    void Update()
    {
        if (inventory == null)
        {
            return;
        }

        // La linterna solamente puede prenderse
        // cuando tenemos seleccionado el Slot 1
        if (inventory.GetSelectedSlot() == 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlight.enabled = !flashlight.enabled;
            }
        }
        else
        {
            // Si cambiamos a otro objeto, la linterna se apaga
            if (flashlight.enabled)
            {
                flashlight.enabled = false;
            }
        }

        BuscarObjetos();
    }

    void BuscarObjetos()
    {
        ObjetoRevelable[] objetos = FindObjectsOfType<ObjetoRevelable>();

        foreach (ObjetoRevelable objeto in objetos)
        {
            Vector3 direccion =
                objeto.transform.position - flashlight.transform.position;

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