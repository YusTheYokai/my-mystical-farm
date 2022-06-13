using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Muhsroom Class", menuName = "Item/Mushroom")]
public class Mushroom : ItemClass {

    public override ItemClass GetItem() {
        return this;
    }
    public override DummyClass GetDummy() {
        return null;
    }
}