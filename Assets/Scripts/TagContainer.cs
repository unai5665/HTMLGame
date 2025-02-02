using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class TagContainer : MonoBehaviour
{
    public List<string> expectedOrder; // Lista con el orden correcto de etiquetas
    public List<DraggableTag> tagSlots; // Slots en el UI donde las etiquetas se colocan
    public GameManager gameManager;

    public void CheckOrder()
    {
        List<string> playerOrder = new List<string>();

        foreach (DraggableTag tag in tagSlots)
        {
            playerOrder.Add(tag.GetTag()); // Obtiene las etiquetas en los espacios
        }

        if (playerOrder.Count == expectedOrder.Count && playerOrder.SequenceEqual(expectedOrder))
        {
            gameManager.NextLevel(); // Si es correcto, pasa de nivel
        }
        else
        {
            Debug.Log("Orden incorrecto, intenta de nuevo.");
        }
    }
}
