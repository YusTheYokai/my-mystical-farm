using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour {

    [SerializeField] private Button _button;

    [SerializeField] private AudioClip _audioClip;

    void Start() {
        _button.onClick.AddListener(() => SoundManager.Instance.PlayEffect(_audioClip));
    }
}
