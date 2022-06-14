using UnityEngine;

public class FeedableAnimal : Animal {

    [SerializeField] private ItemClass food;
    [SerializeField] private Sprite fed;
    private SpriteRenderer spriteRenderer;

    public override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown() {
        if (food == null && spriteRenderer.sprite != fed) {
            spriteRenderer.sprite = fed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        DragDrop dragDrop = collider.gameObject.GetComponent<DragDrop>();
        if (dragDrop != null && spriteRenderer.sprite != fed && dragDrop.slot.GetItem() == food.GetItem()) {
            dragDrop.slot.SubQuantity(1);
            spriteRenderer.sprite = fed;
            InventoryManager.Instance.RefreshUI();
        }
    }
}
