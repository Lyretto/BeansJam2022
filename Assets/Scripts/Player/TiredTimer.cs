using System.Collections;
using System.Linq;
using UnityEngine;

public class TiredTimer : MonoBehaviour
{
    public float tiredTime;

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
            var modifier = PlayerController.Instance.WakeAoes;
            var timeMulti = modifier.Count <= 0 ? 1 : modifier.Select(aoe => aoe.modifier).Aggregate((total, m) => total * m);
            
            tiredTime -= Time.deltaTime * timeMulti;
            yield return null;
        }

        GameEvents.Instance.tiredTimerExpired.Invoke();
        yield return null;
    }
}

