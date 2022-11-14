using UnityEngine;

public class Croco : Interactables
{
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject can;
    private bool activated;
    public override bool IsActivated() => activated;

    public override void Interact()
    {
        can.SetActive(false);
        trap.SetActive(true);
        activated = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Schleim umwerfen", transform.position);
    }
}
