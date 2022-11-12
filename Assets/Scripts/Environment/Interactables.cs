using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    private List<GameObject> meshs;
    private void Awake()
    {
        meshs.AddRange( GetComponentsInChildren<MeshRenderer>().ToList().Select(m => m.gameObject));
    }

    public abstract void Interact();

    public void Highlight()
    {
        meshs.ForEach(m => m.layer = 10);
    }
}
