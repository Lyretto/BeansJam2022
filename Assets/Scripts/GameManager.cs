using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public TiredTimer timer;
    [NonSerialized] public RageMeter rageMeter;
    [SerializeField] private float startTime = 10;
    [SerializeField] private float rageCount = 3;

    private void OnEnable()
    {
       GameEvents.Instance.tiredTimerExpired.AddListener(SwitchToDemon);
       GameEvents.Instance.togglePause.AddListener(Paused);
       GameEvents.Instance.calm.AddListener(ResetTimer);
    }

    private void OnDisable()
    {
        if (!GameEvents.Instance) return;
        GameEvents.Instance.tiredTimerExpired.RemoveListener(SwitchToDemon);
        GameEvents.Instance.togglePause.RemoveListener(Paused);
        GameEvents.Instance.calm.RemoveListener(ResetTimer);
    }


    private void Paused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }

    private void ResetTimer()
    {
        timer.tiredTime = startTime;
        timer.timeMulti = 1;
        timer.StartTimer();
    }

    private void ResetRageMeter()
    {
        rageMeter.ragePerObstruction = 1;
        rageMeter.startRage = rageCount;
        rageMeter.ResetMeter();
    }
    
    private void Awake()
    {
        timer = gameObject.AddComponent<TiredTimer>();
        rageMeter = gameObject.AddComponent<RageMeter>();
        ResetTimer();
    }

    public void Restart()
    {
        SceneManager.LoadScene("PlayRoom");
    }

    private void SwitchToDemon()
    {
        ResetRageMeter();
    }
}
