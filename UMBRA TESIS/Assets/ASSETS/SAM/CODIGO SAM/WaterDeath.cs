using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            startPosition = player.transform.position;
            startRotation = player.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            other.transform.position = startPosition;
            other.transform.rotation = startRotation;

            if (controller != null)
                controller.enabled = true;
        }
    }
}
