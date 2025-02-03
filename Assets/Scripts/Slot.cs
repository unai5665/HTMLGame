using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string expectedTag; // La etiqueta esperada en este slot

    public void OnDrop(PointerEventData eventData)
    {
        DraggableTag draggedTag = eventData.pointerDrag.GetComponent<DraggableTag>();

        if (draggedTag != null)
        {
            // Verificar si la etiqueta es correcta
            if (draggedTag.GetTag() == expectedTag)
            {
                Debug.Log("Etiqueta colocada correctamente: " + draggedTag.GetTag());

                // Fijar la posición de la etiqueta dentro del slot
                draggedTag.transform.SetParent(transform);
                draggedTag.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.Log("Etiqueta incorrecta, vuelve a la posición original.");
                draggedTag.ResetPosition(); // Si es incorrecto, regresa al origen
            }
        }
    }
}
