using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject droppedItemsContainer;

    public SlotClass slot;

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
            // GameObject go = Instantiate(droppedItemPrefab, PlayerMovement.Instance.getPosition(), Quaternion.identity, droppedItemsContainer.transform);
            // DroppedItem item = go.GetComponent<DroppedItem>();

            // item.item = slot.GetItem();
            // item.quantity = slot.GetQuantity();
            // item.GetComponent<SpriteRenderer>().sprite = slot.GetItem().itemIcon;

            Vector3 itemPosition = PlayerMovement.Instance.getPosition();
            itemPosition.y -= 5f;
            GameObject droppedItem = Instantiate(droppedItemPrefab, itemPosition, Quaternion.identity, droppedItemsContainer.transform);

            var item = droppedItem.GetComponent<DroppedItem>();
            item.item = slot.GetItem();
            item.quantity = slot.GetQuantity();
            item.GetComponent<SpriteRenderer>().sprite = slot.GetItem().itemIcon;

            // reset the stack's position
            rectTransform.anchoredPosition = startRectTransformPosition;
            // clear the item(s) from the stack
            InventoryManager.Instance.ClearStack(transform);
        }

        canvasGroup.blocksRaycasts = true;
    } 
}
