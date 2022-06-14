using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager Instance;

    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private SlotClass[] startItems;
    [SerializeField] private Text moneyText;

    private SlotClass[] items;
    private GameObject[] slots;

    private float _money;
    public float Money {
        get {
            return _money;
        }
        set {
            _money = value;
            moneyText.text = _money.ToString();
        }
    }

    // //////////////////////////////////////////////////////////////////////////
    // Methoden
    // //////////////////////////////////////////////////////////////////////////

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    private void Start() {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        // initializing slots
        for (int i = 0; i < items.Length; i++) {
            items[i] = new SlotClass();
        }

        // settings start items
        for (int i = 0; i < startItems.Length; i++) {
            items[i] = startItems[i];
        }

        // set all slots
        for (int i = 0; i < slotHolder.transform.childCount; i++) {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        Money = 5f;
        RefreshUI();
    }

    private void Update() {
        if (Input.GetKeyDown("e")) {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeInHierarchy);
        }
    }

    #region Inventory Utils 
    public void RefreshUI() {
        for (int i = 0; i < slots.Length; i++) {
            Transform stack = slots[i].transform.GetChild(0);
            DragDrop dragDrop = stack.GetComponent<DragDrop>();
            Image image = stack.GetChild(0).GetComponent<Image>();
            Text quantity = stack.GetChild(1).GetComponent<Text>();

            try {
                dragDrop.slot = items[i];

                image.enabled = true;
                image.sprite = items[i].GetItem().itemIcon;

                if (items[i].GetItem().isStackable) {
                    quantity.text = items[i].GetQuantity().ToString();
                } else {
                    quantity.text = "";
                }
            } catch {
                image.sprite = null;
                image.enabled = false;
                quantity.text = "";
            }
        }
    }

    public bool Add(ItemClass item, int quantity) {
        //check if inventory contains item
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable) { 
            slot.AddQuantity(quantity);
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

    /// <summary>
    /// Swaps a stack with the stack closest to the cursor.
    /// </summary>
    /// <param name="stack">the stack to swap</param>
    public void SwapStacks(Transform stack) {
        int i = SlotIndexFromStack(stack);
        int j = ClosestSlotIndex();
        (slots[i], slots[j]) = (slots[j], slots[i]);
        RefreshUI();
    }

    private int ClosestSlotIndex() {
        var pairs = slots.Select(slot => new KeyValuePair<GameObject, float>(slot, Vector2.Distance(Camera.main.WorldToScreenPoint(slot.transform.position), Input.mousePosition)));

        KeyValuePair<GameObject, float> closest = new KeyValuePair<GameObject, float>(null, float.MaxValue);
        foreach (var pair in pairs) {
            if (pair.Value < closest.Value) {
                closest = pair;
            }
        }

        return Array.IndexOf(slots, closest.Key);
    }

    public void ClearStack(Transform stack) {
        int index = SlotIndexFromStack(stack);
        items[index] = new SlotClass();
        RefreshUI();
    }

    private int SlotIndexFromStack(Transform stack) {
        GameObject slot = Array.Find(slots, slot => slot.transform.GetChild(0) == stack);
        return Array.IndexOf(slots, slot);
    }

    #endregion Inventory Utils
}
