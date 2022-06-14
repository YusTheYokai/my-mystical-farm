using UnityEngine;

public class Chicken : MonoBehaviour {

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject droppedItemsContainer;
    [SerializeField] private Egg egg;

    void Update() {
        int dropEgg = Random.Range(0, 10001);

        if (dropEgg == 7) {
            GameObject go = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity, droppedItemsContainer.transform);
            DroppedItem item = go.GetComponent<DroppedItem>();
            item.item = egg;
            item.quantity = 1;
            go.GetComponent<SpriteRenderer>().sprite = egg.GetItem().itemIcon;
        }
    }
}