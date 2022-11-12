using System.Collections;
using UnityEngine;

public class TiredTimer : MonoBehaviour
{
    public float tiredTime;
    public float timeMulti;

    public void StartTimer()
    {
        StartCoroutine(TickTimer());
    }
    
    public void AddTime(float time)
    {
        tiredTime += time;
    }

    private IEnumerator TickTimer()
    {
        while (tiredTime > 0)
        {
            tiredTime -= Time.deltaTime * timeMulti;
            yield return null;
        }

        GameEvents.Instance.tiredTimerExpired.Invoke();
        yield return null;
    }
}

