using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private List<GameObject> demonVisuals;
   [SerializeField] private List<GameObject> childVisuals;
   [SerializeField] private Material childMaterial;
   [SerializeField] private Material demonMaterial;
   [SerializeField] private SkinnedMeshRenderer characterMesh;

   private Interactables currentInteractable;
   private Collectables currentCollectable;
   
   
   private void OnEnable()
   {
       GameEvents.Instance.tiredTimerExpired.AddListener(() => SwitchInto(PlayerState.Demon));
   }

   private void OnDisable()
   {
       if (!GameEvents.Instance) return;
       GameEvents.Instance.tiredTimerExpired.RemoveListener(() => SwitchInto(PlayerState.Demon));
   }
   
   private void Start()
    {
        SwitchInto(PlayerState.Child);
    }

    public void SwitchInto(PlayerState newState)
    {
        var isChild = newState == PlayerState.Child;
        demonVisuals.ForEach(d =>  d.SetActive(!isChild));
        childVisuals.ForEach(c => c.SetActive(isChild));
        characterMesh.material = isChild ? childMaterial : demonMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.GetComponent<Interactables>();
        }
    }
}

public enum PlayerState{
    Demon, Child
}
