using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus eventBus;

    void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Musik");
        eventBus = FMODUnity.RuntimeManager.GetBus("bus:/Sound");

        musicSlider.onValueChanged.AddListener((volume) =>
        {
            Settings.Instance.musicVolume = volume;
            musicBus.setVolume(volume / 3);
        });
        soundSlider.onValueChanged.AddListener(volume => eventBus.setVolume(Settings.Instance.soundVolume = volume));

        musicSlider.value = Settings.Instance.musicVolume;
        soundSlider.value = Settings.Instance.soundVolume;
    }
}