using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string expectedTag; // La etiqueta que debería estar en este slot
    private RectTransform slotRectTransform;

    void Start()
    {
        slotRectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableTag draggedTag = eventData.pointerDrag.GetComponent<DraggableTag>();

        if (draggedTag != null)
        {
            // Verifica si la etiqueta arrastrada es la correcta
            if (draggedTag.GetTag() == expectedTag)
            {
                // Coloca la etiqueta en el slot
                draggedTag.transform.SetParent(transform);
                draggedTag.transform.localPosition = Vector3.zero; // Asegura que se coloque en el centro del slot

                // Verifica el orden después de colocar la etiqueta
                TagContainer tagContainer = FindObjectOfType<TagContainer>();
                if (tagContainer != null)
                {
                    tagContainer.CheckOrder(); // Verifica si el orden es correcto
                }
            }
            else
            {
                // Si la etiqueta es incorrecta, vuelve a su posición original
                draggedTag.ResetPosition();
            }
        }
    }
}
