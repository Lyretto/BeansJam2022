using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private GameObject demonVisuals;
   [SerializeField] private GameObject childVisuals;
    
    private void Awake()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        SwitchInto(PlayerState.Child);
    }

    public void SwitchInto(PlayerState newState)
    {
        var isChild = newState == PlayerState.Child;
        demonVisuals.SetActive(!isChild);
        childVisuals.SetActive(isChild);
        
        
    }
    
}

public enum PlayerState{
    Demon, Child
}
