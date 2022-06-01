using UnityEngine;
using UnityEngine.EventSystems;

public class Inventorypanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private InventoryManager inventoryManager;

    public void OnPointerEnter(PointerEventData eventData) {
        inventoryManager.drop = false;
        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        inventoryManager.drop = true;
        Debug.Log("exit");
    }
}
