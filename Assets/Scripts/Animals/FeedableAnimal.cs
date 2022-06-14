using UnityEngine;

public class FeedableAnimal : MonoBehaviour {

    [SerializeField] private ItemClass food;
    [SerializeField] private Sprite fed;
    private SpriteRenderer spriteRenderer;

    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown() {
        if (food == null && spriteRenderer.sprite != fed) {
            spriteRenderer.sprite = fed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (food == null) {
            return;
        }

        DragDrop dragDrop = collider.gameObject.GetComponent<DragDrop>();
        if (dragDrop != null && spriteRenderer.sprite != fed && dragDrop.slot.GetItem() == food.GetItem()) {
            dragDrop.slot.SubQuantity(1);
            spriteRenderer.sprite = fed;
            InventoryManager.Instance.RefreshUI();
        }
    }

    public Sprite getFed() {
        return fed;
    }
}
