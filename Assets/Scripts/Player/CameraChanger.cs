using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook childCam;
    [SerializeField] CinemachineVirtualCamera demonCam;
    bool childCamOn = true;

    private void Start()
    {
        childCam.m_XAxis.m_MaxSpeed = 400 * Settings.Instance.mouseSens;
    }

    private void Update()
    {
        Debug.Log(childCam.m_XAxis.m_MaxSpeed);
    }

    private void OnEnable()
    {
        GameEvents.Instance.transforming.AddListener(_ => SwitchPriority());
    }

    private void OnDisable()
    {
        if(GameEvents.Instance) GameEvents.Instance.transforming.RemoveListener(_ => SwitchPriority());
    }

    void SwitchPriority()
    {
        if (childCamOn)
        {
            childCam.Priority = 0;
            demonCam.Priority = 1;
            StartCoroutine(SwitchToDemonCam());
        } else
        {
            childCam.Priority = 1;
            demonCam.Priority = 0;
        }
        childCamOn = !childCamOn;
    }

    private IEnumerator SwitchToDemonCam()
    {
        var timer = 1f;
        var demonOffset = demonCam.GetComponent<CinemachineCameraOffset>();
        var endOffset = demonOffset.m_Offset.z;
        var startOffset = 8f;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            demonOffset.m_Offset.z = Mathf.Lerp(startOffset,endOffset, 1-timer);
            yield return 0;
        }

        demonOffset.m_Offset.z = endOffset;
    }
}
