using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBackgroundMusic : MonoBehaviour {

    [SerializeField] private AudioClip _music;
    void Start() {
        SoundManager.Instance.PlayMusic(_music);
    }
}
