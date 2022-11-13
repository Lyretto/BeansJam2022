using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI
{
    public class UIInGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject looseScreen;
        [SerializeField] private GameObject awakeScreen;
        [SerializeField] private GameObject sleepScreen;
        [SerializeField] private TMP_Text scoreText;
        bool isChild = true;
        [SerializeField] private float timer;
        private float score;

        private void Start()
        {
            timer = 0;
            pauseMenu.SetActive(false);
            looseScreen.SetActive(false);
        }
        private void Update()
        {
            if (isChild)
            {
                timer += Time.deltaTime;
            }           
        }

        private void OnEnable()
        {
            GameEvents.Instance.togglePause.AddListener(TogglePause);
            GameEvents.Instance.transforming.AddListener(ToggleTransform);
        }
        private void OnDisable()
        {
            GameEvents.Instance.togglePause.RemoveListener(TogglePause);
            GameEvents.Instance.transforming.RemoveListener(ToggleTransform);
        }

        private void TogglePause(bool paused)
        {
            pauseMenu.SetActive(paused);
        }

        public void Resume()
        {
            GameEvents.Instance.togglePause.Invoke(false);          
        }

        public void Restart()
        {
            GameEvents.Instance.togglePause.Invoke(false);
            SceneManager.LoadScene("PlayRoom");
        }

        public void BackToMenu()
        {
            GameEvents.Instance.togglePause.Invoke(false);
            SceneManager.LoadScene("Menu");
        }

        private void ToggleTransform(PlayerState state)
        {
            isChild = state == PlayerState.Child;
        }

        private void OpenLooseScreen()
        {
            SetScore();
            looseScreen.SetActive(true);
        }

        private void SetScore()
        {
            score = timer;
            scoreText.text = "Your Score: " + score.ToString();
        }
    }
}
