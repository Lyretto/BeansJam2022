using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    public float soundVolume = 0.2f;
    public float musicVolume = 0.1f;
    public float mouseSens = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        Application.targetFrameRate = 60;
    }
}
