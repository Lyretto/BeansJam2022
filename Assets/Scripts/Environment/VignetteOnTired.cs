using System;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;

public class VignetteOnTired : MonoBehaviour
{
    private Volume volume;
    private TiredTimer tiredTimer;
    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Start()
    {
        tiredTimer = GameEvents.Instance.GetComponent<TiredTimer>();
    }

    private void Update()
    {

        if (tiredTimer.tiredTime > 10 || tiredTimer.tiredTime <= 0)
        {
            volume.weight = 0;
            return;
        }
        volume.weight = 1 - tiredTimer.tiredTime / 10;
    }
}
