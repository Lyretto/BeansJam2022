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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (activated && other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().isChild)
        {
            disableObject.SetActive(true);
            turnOnObject.SetActive(false);
            activated = false;
        }
    }
}
