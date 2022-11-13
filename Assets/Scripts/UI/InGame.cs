using UnityEngine;

namespace UI
{
    public class InGame : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private void Start()
        {
            pauseMenu.SetActive(false);
        }

        private void OnEnable()
        {
            GameEvents.Instance.togglePause.AddListener(TogglePause);
        }
        private void OnDisable()
        {
            GameEvents.Instance.togglePause.RemoveListener(TogglePause);
        }

        private void TogglePause(bool paused)
        {
            pauseMenu.SetActive(paused);
        }
    }
}
