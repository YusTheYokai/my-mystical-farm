using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject droppedItemsContainer;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startRectTransformPosition;

    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        //raycast goes through item and lands on item slot
        canvasGroup.blocksRaycasts = false;
        startRectTransformPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        //item is moving alongside the mouse
        rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
    }  

    public void OnEndDrag(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject != null) {
            rectTransform.anchoredPosition = startRectTransformPosition;
            InventoryManager.Instance.SwapStacks(transform);
        } else {
            // create the dropped item
            GameObject droppedItem = Instantiate(droppedItemPrefab, PlayerMovement.Instance.getPosition(), Quaternion.identity, droppedItemsContainer.transform);
            // set its scale
            droppedItem.transform.localScale = new Vector3(2, 2, 2);
            // set its sprite
            droppedItem.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
            // TODO: set its quantity for pickup
            
            // reset the stack's position
            rectTransform.anchoredPosition = startRectTransformPosition;
            // clear the item(s) from the stack
            InventoryManager.Instance.ClearStack(transform);
        }

        canvasGroup.blocksRaycasts = true;
    } 
}
