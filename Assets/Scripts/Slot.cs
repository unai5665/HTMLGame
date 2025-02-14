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
            

            if (draggedTag.GetTag() == expectedTag) // Si la etiqueta coincide
            {
                draggedTag.transform.SetParent(transform);

                draggedTag.transform.localPosition = Vector3.zero;
                
                

                // Verifica el orden después de colocar la etiqueta
               TagContainer tagContainer = FindObjectOfType<TagContainer>();
                if (tagContainer != null && tagContainer.AllSlotsFilled())  
                {
                    tagContainer.CheckOrder();
                }
            }
            else
            {
            
                draggedTag.ResetPosition();
            }
        }
    }
}

