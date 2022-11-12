using System;
using Cinemachine;
using UnityEngine;

public class CameraRotationPause : MonoBehaviour
{
    private CinemachineInputProvider cineMachineInputProvider;


    private void OnEnable()
    {
        GameEvents.Instance.togglePause.AddListener(Paused);
    }

    private void OnDisable()
    {
        GameEvents.Instance.togglePause.RemoveListener(Paused);
    }

    private void Paused(bool paused)
    {
        cineMachineInputProvider.enabled = !paused;
    }

    private void Awake()
    {
        cineMachineInputProvider =  GetComponent<CinemachineInputProvider>();
    }
}
