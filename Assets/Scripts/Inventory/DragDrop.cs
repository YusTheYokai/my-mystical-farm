using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas inventoryCanvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startRectTransformPosition;
    private Transform startParent;

    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        startRectTransformPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
        transform.SetParent(inventoryCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
    }  

    public void OnEndDrag(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject != null) {
            transform.SetParent(startParent);
            rectTransform.anchoredPosition = startRectTransformPosition;
            print("if");
        } else {
            print("else");
        }
        canvasGroup.blocksRaycasts = true;
    } 

    public void OnPointerDown(PointerEventData eventData) {
    }


}
