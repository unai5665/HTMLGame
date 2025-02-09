using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TagContainer : MonoBehaviour
{
    public List<string> expectedOrder; // Lista con el orden correcto de etiquetas
    public List<Slot> tagSlots; // Slots en el UI donde las etiquetas se colocan
    public GameManager gameManager;
    public GameObject draggableTagPrefab;  // Prefab de la etiqueta arrastrable
    public Transform tagContainerPanel; 

    void Start()
    {
        if (GameManager.Instance != null)
        {
            expectedOrder = new List<string>(GameManager.Instance.currentTags); // Sincroniza con el GameManager
        }
        InitializeSlots();
    }

    public void UpdateExpectedOrder(List<string> newOrder)
{
    if (newOrder == null || newOrder.Count == 0)
    {
        return;
    }

    // Limpiar el expectedOrder y actualizar con el nuevo orden
    expectedOrder.Clear();
    expectedOrder.AddRange(newOrder);


    // Asegurar que los slots también se actualizan
    ClearSlots();
    InitializeSlots();
}
    

    public void CheckOrder()
    {
        List<string> playerOrder = new List<string>();

        // Recorre los slots y recoge las etiquetas que el jugador ha colocado en ellos
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>();
            if (draggableTag != null)
            {
                playerOrder.Add(draggableTag.GetTag()); // Obtiene las etiquetas en los espacios
            }
        }

        // Compara el orden de las etiquetas con el esperado
        if (playerOrder.Count == expectedOrder.Count && playerOrder.SequenceEqual(expectedOrder))
        {
            gameManager.NextLevel(); // Si es correcto, pasa de nivel
        }
        else
        {
        
            ResetTags();
        }
    }

    public bool AllSlotsFilled()
    {
        foreach (Slot tag in tagSlots)
        {
            if (tag == null) // Si hay un slot sin etiqueta, aún no está completo
            {
                return false;
            }
        }
        return true;
    }

    public void ClearSlots()
    {
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>();
            if (draggableTag != null)
            {
                Destroy(draggableTag.gameObject); // Eliminar la etiqueta dentro del slot
            }
        }
    }

    public void InitializeSlots()
    {
        for (int i = 0; i < tagSlots.Count; i++)
        {
            if (i < expectedOrder.Count)  // Asegura que no haya error si hay más slots que etiquetas
            {
                tagSlots[i].expectedTag = expectedOrder[i]; // Asigna la etiqueta esperada al slot
            
            }
            else
            {
                tagSlots[i].expectedTag = null;  // Si hay más slots que etiquetas, vacíalos
            }
        }
    }







    public void ResetTags()
    {
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>(); // Obtener la etiqueta dentro del slot

            if (draggableTag != null) 
            {
                draggableTag.ResetPosition(); // Devolver solo las etiquetas que estén en un slot
            }
        }
    }


        public void AddTagToUI(string tag)
    {
        // Importante: Usa 'false' para que el objeto adopte las propiedades del prefab
        GameObject newTag = Instantiate(draggableTagPrefab, tagContainerPanel, false);

        TMP_Text text = newTag.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            // Forzar que rich text esté desactivado
            text.richText = false;
            text.text = tag;
           
        }
    }

}
