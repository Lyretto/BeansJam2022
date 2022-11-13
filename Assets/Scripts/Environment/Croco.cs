using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croco : Interactables
{
    [SerializeField] private GameObject trap;
    private bool activated;
    public override bool IsActivated() => activated;

    public override void Interact()
    {
        trap.SetActive(true);
        activated = true;
    }
}
