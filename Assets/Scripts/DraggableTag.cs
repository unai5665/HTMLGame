using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableTag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Verifica si está sobre un espacio válido
        if (!ValidDrop())
        {
            transform.position = originalPosition; // Devuelve a la posición original
        }
    }
    public void ResetPosition()
{
    // Devolver la etiqueta a su posición original
    transform.position = originalPosition;
}

    private bool ValidDrop()
    {
        // Aquí puedes implementar la lógica para saber si se colocó en un lugar válido
        return true;
    }

    public string GetTag()
    {
        return GetComponent<TMP_Text>().text;
    }
}
