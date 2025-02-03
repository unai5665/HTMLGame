using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableTag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Canvas canvas;  // Necesario para la conversión de coordenadas

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Al empezar a arrastrar, hacer el objeto un poco transparente y permitir arrastrarlo
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convertir la posición de la pantalla a coordenadas locales dentro del canvas
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition);
        
        // Asignar la posición local del cursor al RectTransform del objeto arrastrado
        rectTransform.anchoredPosition = localPointerPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Al terminar el arrastre, restaurar la opacidad original y permitir los raycast
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    // Resetea la posición original si la etiqueta no se suelta en el lugar correcto
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    public string GetTag()
    {
        return GetComponent<TMP_Text>().text;
    }
}
