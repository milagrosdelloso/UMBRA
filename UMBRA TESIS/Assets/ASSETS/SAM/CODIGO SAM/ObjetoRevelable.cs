using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRevelable : MonoBehaviour
{
    [Header("Objeto que se revela")]
    public GameObject objetoARevelar;

    [Header("Inventario")]
    public Sprite inventorySprite;

    [Header("Jugador")]
    public Transform jugador;

    [Header("Interacción")]
    public GameObject recogerTexto;
    public float distanciaParaRecoger = 2.5f;

    private Inventory inventory;

    void Start()
    {
        // El objeto empieza oculto
        if (objetoARevelar != null)
        {
            objetoARevelar.SetActive(false);
        }
        else
        {
            Debug.LogError("No asignaste 'Objeto A Revelar' en " + gameObject.name);
        }

        // Busca el inventario
        inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("No se encontró un Inventory en la escena.");
        }

        // El texto empieza oculto
        if (recogerTexto != null)
        {
            recogerTexto.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No asignaste 'Recoger Texto' en " + gameObject.name);
        }

        // Si no asignaste el jugador manualmente, intenta encontrarlo por Tag
        if (jugador == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                jugador = player.transform;
                Debug.Log("Jugador encontrado automáticamente.");
            }
            else
            {
                Debug.LogError(
                    "No se encontró al jugador. Arrastrá tu First Person Controller al campo 'Jugador' de "
                    + gameObject.name
                );
            }
        }
    }

    public void Revelar()
    {
        if (objetoARevelar != null)
        {
            objetoARevelar.SetActive(true);
        }
    }

    public void Ocultar()
    {
        if (objetoARevelar != null)
        {
            objetoARevelar.SetActive(false);
        }

        if (recogerTexto != null)
        {
            recogerTexto.SetActive(false);
        }
    }

    void Update()
    {
        // Si no está revelado, no hacemos nada
        if (objetoARevelar == null || !objetoARevelar.activeSelf)
        {
            if (recogerTexto != null)
            {
                recogerTexto.SetActive(false);
            }

            return;
        }

        // Si no tenemos jugador, no podemos calcular distancia
        if (jugador == null)
        {
            return;
        }

        // Distancia entre jugador y objeto
        float distancia = Vector3.Distance(
            jugador.position,
            objetoARevelar.transform.position
        );

        // Si está cerca
        if (distancia <= distanciaParaRecoger)
        {
            if (recogerTexto != null)
            {
                recogerTexto.SetActive(true);
            }

            // Recoger con E
            if (Input.GetKeyDown(KeyCode.E))
            {
                Recoger();
            }
        }
        else
        {
            if (recogerTexto != null)
            {
                recogerTexto.SetActive(false);
            }
        }
    }

    void Recoger()
    {
        if (inventory == null)
        {
            Debug.LogError("No se encontró el Inventory.");
            return;
        }

        if (inventorySprite == null)
        {
            Debug.LogError(
                "No asignaste el Inventory Sprite en " + gameObject.name
            );
            return;
        }

        bool recogido = inventory.AddItem(inventorySprite);

        if (recogido)
        {
            Debug.Log(gameObject.name + " fue recogido.");

            // Ocultar objeto
            objetoARevelar.SetActive(false);

            // Ocultar mensaje
            if (recogerTexto != null)
            {
                recogerTexto.SetActive(false);
            }

            // Desactivar este revelador
            gameObject.SetActive(false);
        }
    }
}