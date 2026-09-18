using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorLinterna : MonoBehaviour
{
    public Light flashlight;

    private void OnTriggerStay(Collider other)
    {
        if (!flashlight.enabled)
            return;

        ObjetoRevelable objeto = other.GetComponent<ObjetoRevelable>();

        if (objeto == null)
            return;

        Vector3 direccion = other.transform.position - flashlight.transform.position;

        float distancia = direccion.magnitude;

        // Está dentro del alcance de la linterna
        if (distancia > flashlight.range)
            return;

        // Está dentro del ángulo del Spotlight
        float angulo = Vector3.Angle(
            flashlight.transform.forward,
            direccion
        );

        if (angulo <= flashlight.spotAngle / 2f)
        {
            objeto.Revelar();
        }
    }
}
