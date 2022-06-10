using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour {

    public GameObject Player;
    public Vector3 offset = new Vector3(0,0,-1);

    void FixedUpdate() {
        if (Player) {
            transform.position = new Vector3 (
                Player.transform.position.x + offset.x,
                Player.transform.position.y + offset.y,
                Player.transform.position.z + offset.z
            );
        }
    }
}
