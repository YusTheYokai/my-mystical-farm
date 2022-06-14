using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Egg Class", menuName = "Item/Egg")]
public class Egg : ItemClass {

    public override ItemClass GetItem() {
        return this;
    }
    public override DummyClass GetDummy() {
        return null;
    }
}