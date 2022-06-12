using UnityEngine;

public class FeedableAnimal : Animal {

    public Sprite fed;
    private SpriteRenderer spriteRenderer;

    public override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        if (spriteRenderer.sprite != fed) {
            spriteRenderer.sprite = fed;
        }
    }
}
