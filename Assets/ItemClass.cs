using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//Scriptable Object as root class to duplicate and create instances
public abstract class ItemClass : ScriptableObject {
    
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable;

    public abstract ItemClass GetItem(); 
    public abstract DummyClass GetDummy(); 
       
}
