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

   public List<WakeAOE> WakeAoes = new();
   private Animator animator;
   private Interactables currentInteractable;
   private Collectables currentCollectable;
   private bool isChild;
   private static readonly int InteractTrigger = Animator.StringToHash("Interact");
   
   private static PlayerController _instance;
   public static PlayerController Instance
   {
       get
       {
           if (_instance == null)
           {
               _instance = FindObjectOfType<PlayerController>();
           }

           return _instance;
       }
   }


   private void OnEnable()
   {
       GameEvents.Instance.tiredTimerExpired.AddListener(() => SwitchInto(PlayerState.Demon));
       GameEvents.Instance.rageDown.AddListener(() => SwitchInto(PlayerState.Child));
       GameEvents.Instance.interactInput.AddListener(Interact);
   }

   private void OnDisable()
   {
       if (!GameEvents.Instance) return;
       GameEvents.Instance.interactInput.RemoveListener(Interact);
       GameEvents.Instance.tiredTimerExpired.RemoveListener(() => SwitchInto(PlayerState.Demon));
       GameEvents.Instance.rageDown.RemoveListener(() => SwitchInto(PlayerState.Child));
   }

   private void Awake()
   {
       animator = GetComponentInChildren<Animator>();
   }

   private void Start()
    {
        //SwitchInto(PlayerState.Child);
        isChild = true;
        demonVisuals.ForEach(d =>  d.SetActive(!isChild));
        childVisuals.ForEach(c => c.SetActive(isChild));
        characterMesh.material = isChild ? childMaterial : demonMaterial;
    }

    public void SwitchInto(PlayerState newState)
    {
        isChild = newState == PlayerState.Child;
        
        if(!isChild)
            DeselectInteractable();
        
        StartCoroutine(BlendState(isChild));
    }
    
    private IEnumerator BlendState(bool isBlendToChild)
    {
        
        animator.SetTrigger(isBlendToChild ? "Calm" : "Sleep");
        GameEvents.Instance.transforming.Invoke( isBlendToChild ? PlayerState.Child : PlayerState.Demon);

        yield return new WaitForSeconds(isBlendToChild ? 1f : 1.5f);
        
        demonVisuals.ForEach(d =>  d.SetActive(!isBlendToChild));
        childVisuals.ForEach(c => c.SetActive(isBlendToChild));
        characterMesh.material = isBlendToChild ? childMaterial : demonMaterial;
        
       
        if(isBlendToChild)
            GameEvents.Instance.calm.Invoke();
        else
            GameEvents.Instance.rage.Invoke();
        
        yield return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && isChild)
        {
            var interactable = (Interactables) other.GetComponent(typeof(Interactables));
            if (interactable && currentCollectable == interactable || interactable.IsActivated()) return;
            if(currentInteractable) currentInteractable.Deselect();
            currentInteractable = interactable;
            currentInteractable.Highlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") && isChild)
        {
            var interactable = (Interactables) other.GetComponent(typeof(Interactables));
            if (interactable && currentInteractable != interactable) return;
            DeselectInteractable();
        }
    }

    private void DeselectInteractable()
    {
        if(currentInteractable) currentInteractable.Deselect();
        currentInteractable = null;
    }

    private void Interact()
    {
        if (currentInteractable == null || !isChild || currentInteractable.IsActivated()) return;
        currentInteractable.Interact();
        currentCollectable = null;
        animator.SetTrigger(InteractTrigger);
    }
}

public enum PlayerState{
    Demon, Child
}
