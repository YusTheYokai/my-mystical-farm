using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private GameObject itemCursor;
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;
    [SerializeField] private SlotClass[] startItems;

    private SlotClass[] items;
    private GameObject[] slots;
    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass orginalSlot;
    bool isMovingItem;
    public bool drop;

    private void Start() {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];
        //initializing slots i
        for (int i = 0; i < items.Length; i++) {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startItems.Length; i++) {
            items[i] = startItems[i];
        }

        //set all slots
        for (int i = 0; i < slotHolder.transform.childCount; i++) {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        
        RefreshUI();
        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }

    private void Update() {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;

        if (isMovingItem) {
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }

        if (Input.GetMouseButtonDown(0)) {
            //find closest slot that is clicked on
            if (isMovingItem) {
                //endItemMove();
                Debug.Log(GetClosestSlot());
            } else {
               // StartItemMove();
                Debug.Log(GetClosestSlot().GetItem());
            }
        }
        
        if (Input.GetKeyDown("e")) {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeInHierarchy);
        }
    }

    #region Inventory Utils 
    public void RefreshUI() {
        for (int i = 0; i < slots.Length; i++) {
            try {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if(items[i].GetItem().isStackable) {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity() + "";
                } else {
                    slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }
            } catch {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public bool Add(ItemClass item, int quantity) {
        //check if inventory contains item
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable) { 
            slot.AddQuantity(1);
        } else {
            for (int i = 0; i < items.Length; i++) {
                if (items[i].GetItem() == null) { //empty slot
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
        }

        RefreshUI();
        //successfully added an item
        return true;
    } 

    public bool Remove(ItemClass item) {
        //items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null) {
            if (temp.GetQuantity() > 1) {
                temp.SubQuantity(1);
            } else {
                int slotToRemoveIndex = 0;
                for (int i = 0; i < items.Length; i++) {
                    if (items[i].GetItem() == item) {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
            items[slotToRemoveIndex].Clear();
            }
        } else {
            return false;
        }

        RefreshUI();
        return true;
    }

    public SlotClass Contains(ItemClass item) {
        for (int i = 0; i < items.Length; i++) {
            if (items[i].GetItem() == item) {
                return items[i];
            }
        }
        return null;
    }
    #endregion Inventory Utils

    #region Moving Items
    // private bool StartItemMove() {
    //     orginalSlot = GetClosestSlot();
    //     if (orginalSlot == null || orginalSlot.GetItem() == null) {
    //         return false; //item to move doesn't exist
    //     }

    //     movingSlot = new SlotClass(orginalSlot);
    //     orginalSlot.Clear();
    //     isMovingItem = true;
    //     RefreshUI();
    //     return true;
    // }

    // private bool endItemMove() {
    //     // Debug.Log(GetClosestSlot());
    //     if (drop){
    //         Debug.Log("Item dropped");
    //         //TODO....
    //         Debug.Log(drop);
    //     } else {
    //         Debug.Log(drop);
    //         orginalSlot = GetClosestSlot();
    //         if (orginalSlot == null) {
    //             Add(movingSlot.GetItem(), movingSlot.GetQuantity());
    //             movingSlot.Clear();
    //         } else {
    //             if (orginalSlot.GetItem() != null) {
    //                 if (orginalSlot.GetItem() == movingSlot.GetItem()) {
    //                     if (orginalSlot.GetItem().isStackable) {
    //                         orginalSlot.AddQuantity(movingSlot.GetQuantity());
    //                         movingSlot.Clear();
    //                     } else {
    //                         return false;
    //                     }
    //                 } else {
    //                     //swap items
    //                     tempSlot = new SlotClass(orginalSlot); 
    //                     orginalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
    //                     movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());
    //                     RefreshUI();
    //                     return true;
    //                 }
    //             } else {
    //                 orginalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
    //                 movingSlot.Clear();
    //             }
    //         }
    //     }

    //     isMovingItem = false;
    //     //after placing item -> refresh UI
    //     RefreshUI();
    //     return true;
    // }

    private SlotClass GetClosestSlot() {

        for (int i = 0; i < slots.Length; i++) {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 27) {
                return items[i];
            }
        }
        return null;
    }
    #endregion Moving Items
}

