using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    [SerializeField] private Slider mouseSlider;

    void Start()
    {

        mouseSlider.onValueChanged.AddListener((sensitivity) =>
        {
            Settings.Instance.mouseSens = sensitivity;
        });

        mouseSlider.value = Settings.Instance.mouseSens;
    }
}