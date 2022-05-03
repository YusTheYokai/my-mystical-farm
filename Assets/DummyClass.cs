using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Dummy Class", menuName = "Item/Dummy")]
public class DummyClass : ItemClass {
    public override ItemClass GetItem() {
        return this;
    }
    public override DummyClass GetDummy() {
        return this;
    }
}
