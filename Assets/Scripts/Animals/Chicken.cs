using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chicken : MonoBehaviour {

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject droppedItemsContainer;
    [SerializeField] private Egg egg;

    void Update() {

        int dropEgg = Random.Range(0, 1001);

        if(dropEgg == 7) {
            GameObject droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity, droppedItemsContainer.transform);
            DroppedItem item = droppedItem.GetComponent<DroppedItem>();
            item.item = egg;
            item.quantity = 1;
            droppedItem.GetComponent<SpriteRenderer>().sprite = egg.GetItem().itemIcon;
        }
    }
}