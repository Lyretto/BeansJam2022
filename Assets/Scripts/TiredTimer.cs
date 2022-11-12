using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredTimer : MonoBehaviour
{
    public float tiredTime;
    public float timeMulti;

    private void Start()
    {
        StartCoroutine(TickTimer());
    }

    private IEnumerator TickTimer()
    {
        tiredTime -= Time.deltaTime;
        yield return 0;
    }
}

