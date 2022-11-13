using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    private List<GameObject> meshs;
    public abstract bool IsActivated();
    private void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>().ToList().Select(m => m.gameObject).ToList();
        gameObject.tag = "Interactable";
    }

    public abstract void Interact();

    public void Highlight()
    {
        if(meshs is {Count: > 0})
            meshs.ForEach(m => m.layer = 10);
    }

    public void Deselect()
    {
        if(meshs is {Count: > 0})
            meshs.ForEach(m => m.layer = 0);
    }
}
