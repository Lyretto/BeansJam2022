using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private List<GameObject> demonVisuals;
   [SerializeField] private List<GameObject> childVisuals;
   [SerializeField] private Material childMaterial;
   [SerializeField] private Material demonMaterial;
   [SerializeField] private SkinnedMeshRenderer characterMesh;

   private Animator animator;
   private Interactables currentInteractable;
   private Collectables currentCollectable;
   private static readonly int InteractTrigger = Animator.StringToHash("Interact");


   private void OnEnable()
   {
       GameEvents.Instance.tiredTimerExpired.AddListener(() => SwitchInto(PlayerState.Demon));
       GameEvents.Instance.interactInput.AddListener(Interact);
   }

   private void OnDisable()
   {
       if (!GameEvents.Instance) return;
       GameEvents.Instance.interactInput.RemoveListener(Interact);
       GameEvents.Instance.tiredTimerExpired.RemoveListener(() => SwitchInto(PlayerState.Demon));
   }

   private void Awake()
   {
       animator = GetComponentInChildren<Animator>();
   }

   private void Start()
    {
        //SwitchInto(PlayerState.Child);
        var isChild = true;
        demonVisuals.ForEach(d =>  d.SetActive(!isChild));
        childVisuals.ForEach(c => c.SetActive(isChild));
        characterMesh.material = isChild ? childMaterial : demonMaterial;
    }

    public void SwitchInto(PlayerState newState)
    {
        var isChild = newState == PlayerState.Child;
        StartCoroutine(BlendState(isChild));
    }
    
    private IEnumerator BlendState(bool isChild)
    {
        
        animator.SetTrigger(isChild ? "Calm" : "Sleep");
        GameEvents.Instance.transforming.Invoke( isChild ? PlayerState.Child : PlayerState.Demon);

        yield return new WaitForSeconds(2f);
        
        demonVisuals.ForEach(d =>  d.SetActive(!isChild));
        childVisuals.ForEach(c => c.SetActive(isChild));
        characterMesh.material = isChild ? childMaterial : demonMaterial;
        
       
        if(isChild)
            GameEvents.Instance.calm.Invoke();
        else
            GameEvents.Instance.rage.Invoke();
        
        yield return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactables>();
        }
    }

    public void Interact()
    {
        if (!currentInteractable) return;
        currentCollectable.Interact();
        animator.SetTrigger(InteractTrigger);
    }
}

public enum PlayerState{
    Demon, Child
}
