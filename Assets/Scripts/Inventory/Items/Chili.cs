using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Chili Class", menuName = "Item/Chili")]
public class Chili : ItemClass {

    public override ItemClass GetItem() {
        return this;
    }
    public override DummyClass GetDummy() {
        return null;
    }
}