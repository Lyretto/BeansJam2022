using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Karton : Interactables
{
    // [SerializeField] private GameObject box;
    // [SerializeField] private GameObject foldedBox;
     private bool activated;
    public override bool IsActivated() => activated;
    //
    // public override void Interact()
    // {
    //     box.SetActive(false);
    //     foldedBox.SetActive(true);
    //     activated = true;
    // }
    //
    // private void Awake()
    // {
    //     collider = GetComponent<BoxCollider>();
    // }
    //
    // private void Start()
    // {
    //     destroyedObject.SetActive(false);
    //     goodObject.SetActive(true);
    // }
    //
    // public void HitObstruction()
    // {
    //     GameEvents.Instance.obstructionHit.Invoke(this);
    //     if (patched)
    //     {
    //         patched = false;
    //         return;
    //     }
    //
    //     collider.enabled = false;
    //     destroyed = true;
    //     goodObject.SetActive(false);
    //     destroyedObject.SetActive(true);
    //     ObstructionManager.Instance.remaining.Remove(this);
    // }
    //
    // private void OnEnable()
    // {
    //     ObstructionManager.Instance.remaining.Add(this);
    // }
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (activated && other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().isChild)
    //     {
    //         disableObject.SetActive(true);
    //         turnOnObject.SetActive(false);
    //         activated = false;
    //     }
    // }
    public override void Interact()
    {
        
    }
}
