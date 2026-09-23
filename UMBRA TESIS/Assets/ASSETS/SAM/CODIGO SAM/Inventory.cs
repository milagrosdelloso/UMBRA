using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Slots")]
    public Image[] slots;

    [Header("Fixed Items")]
    public Sprite flashlightSprite;
    public Sprite cardSprite;

    [Header("Objetos 3D en la mano")]
    public GameObject[] objetosEnMano;

    [Header("Tarjeta UI")]
    public GameObject tarjetaUI;

    private Sprite[] inventoryItems = new Sprite[5];

    private int selectedSlot = 0;

    void Start()
    {
        inventoryItems[0] = flashlightSprite;
        inventoryItems[1] = cardSprite;
        inventoryItems[2] = null;
        inventoryItems[3] = null;
        inventoryItems[4] = null;

        // La tarjeta empieza oculta
        if (tarjetaUI != null)
        {
            tarjetaUI.SetActive(false);
        }

        UpdateUI();
        UpdateSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlot(4);
        }
    }

    void SelectSlot(int slotIndex)
    {
        // Si el slot está vacío, no hacemos nada
        if (inventoryItems[slotIndex] == null)
        {
            return;
        }

        selectedSlot = slotIndex;

        UpdateSelection();

        Debug.Log("Seleccionaste el Slot " + (slotIndex + 1));
    }

    void UpdateSelection()
    {
        for (int i = 0; i < 5; i++)
        {
            // Selección visual del inventario
            if (i == selectedSlot)
            {
                slots[i].transform.localScale = Vector3.one * 1.15f;
            }
            else
            {
                slots[i].transform.localScale = Vector3.one;
            }

            // Mostrar / ocultar objetos 3D
            if (objetosEnMano != null && i < objetosEnMano.Length)
            {
                if (objetosEnMano[i] != null)
                {
                    objetosEnMano[i].SetActive(i == selectedSlot);
                }
            }
        }

        // Mostrar la tarjeta solamente cuando seleccionamos el Slot 2
        if (tarjetaUI != null)
        {
            tarjetaUI.SetActive(selectedSlot == 1);
        }
    }

    public bool AddItem(Sprite itemSprite)
    {
        for (int i = 2; i < 5; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = itemSprite;

                UpdateUI();

                return true;
            }
        }

        Debug.Log("Inventario lleno.");

        return false;
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex < 2 || slotIndex > 4)
        {
            Debug.Log("No podés eliminar este objeto.");
            return;
        }

        inventoryItems[slotIndex] = null;

        UpdateUI();
    }

    public Sprite GetItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > 4)
        {
            return null;
        }

        return inventoryItems[slotIndex];
    }

    // Permite que otros scripts sepan qué slot está seleccionado
    public int GetSelectedSlot()
    {
        return selectedSlot;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            if (inventoryItems[i] != null)
            {
                slots[i].sprite = inventoryItems[i];
                slots[i].enabled = true;
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false;
            }
        }
    }
}
