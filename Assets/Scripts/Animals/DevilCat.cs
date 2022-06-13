using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilCat : MonoBehaviour {
    public Sprite devilCat;
    private SpriteRenderer spriteRenderer;
   
    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver() {
        if (spriteRenderer.sprite != devilCat) {
            if (Input.GetMouseButtonDown(1)) {
                spriteRenderer.sprite = devilCat;
            } 
        }
    }
}
