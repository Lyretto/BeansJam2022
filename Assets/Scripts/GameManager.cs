using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float tiredTimer = 0;
    private TiredTimer timer;
    [SerializeField] private float startTime = 10;
    
    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
       GameEvents.Instance.tiredTimerExpired.AddListener(SwitchToDemon);
    }

    private void OnDisable()
    {
        if (!GameEvents.Instance) return;
        GameEvents.Instance.tiredTimerExpired.RemoveListener(SwitchToDemon);
    }


    private IEnumerator Start()
    {
        // Generate Level
        //yield return LevelGenerator.Generate();
        
        timer = gameObject.AddComponent<TiredTimer>();
        timer.tiredTime = startTime;
        timer.timeMulti = 1;
        yield return 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene("PlayRoom");
    }

    public void SwitchToDemon()
    {
    }
}
