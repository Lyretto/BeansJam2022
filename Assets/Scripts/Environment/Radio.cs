using System.Collections;
using UnityEngine;

public class Radio : Interactables
{
    [SerializeField] private GameObject turnOnObject;

    private bool activated;
    public override bool IsActivated() => activated;

    public override void Interact()
    {
        turnOnObject.SetActive(!turnOnObject.activeSelf);
        activated = true;
        StartCoroutine(TimedRadio(30));
    }

    private IEnumerator TimedRadio(float seconds)
    {
        Deselect();
        turnOnObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        turnOnObject.SetActive(false);
        activated = false;
    }
}
