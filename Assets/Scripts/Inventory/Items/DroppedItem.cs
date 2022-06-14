using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemClass item;
    public int quantity;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            Destroy(gameObject);
            InventoryManager.Instance.Add(Instantiate(item), quantity);
        }
    }
}
