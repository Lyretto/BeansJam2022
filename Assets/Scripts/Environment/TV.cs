using UnityEngine;

public class TV : Interactables
{
    [SerializeField] private GameObject turnOnObject;

    private bool activated;
    public override bool IsActivated() => activated;

    public override void Interact()
    {
        Deselect();
        turnOnObject.SetActive(!turnOnObject.activeSelf);
        activated = true;
    }
}
