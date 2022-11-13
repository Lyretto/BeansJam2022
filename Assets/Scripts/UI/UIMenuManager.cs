using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public void Startgame()
    {
        SceneManager.LoadScene("PlayRoom");
    }

    public void LeaveGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void SwitchToScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
