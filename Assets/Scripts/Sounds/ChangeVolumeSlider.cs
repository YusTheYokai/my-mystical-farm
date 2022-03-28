using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour {

    [SerializeField] private Slider _slider;

    [SerializeField] private AudioSource _audioSource;

    void Start() {
        _slider.onValueChanged.AddListener(volume => _audioSource.volume = volume);
    }
}
