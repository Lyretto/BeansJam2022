using System;
using UnityEngine;

public class TV : Interactables
{
    [SerializeField] private GameObject turnOnObject;
    [SerializeField] private GameObject disableObject;

    private bool activated;
    public override bool IsActivated() => activated;
    
    public override void Interact()
    {
        Deselect();
        turnOnObject.SetActive(!turnOnObject.activeSelf);
        activated = true;
        disableObject.SetActive(false);
    }

    public void HitTV()
    {
        if (activated)
        {
            disableObject.SetActive(true);
            turnOnObject.SetActive(false);
            activated = false;
            GameEvents.Instance.obstructionHit.Invoke(1);
        }
    }
}
