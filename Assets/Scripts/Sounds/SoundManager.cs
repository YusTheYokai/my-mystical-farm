using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioSource _musicSource;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(AudioClip effect) {
        _effectSource.PlayOneShot(effect);
    }

    public void PlayMusic(AudioClip music) {
        _musicSource.Stop();
        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void ChangeMasterVolume(float volume) {
        AudioListener.volume = volume;
    }
}
