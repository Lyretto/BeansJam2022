using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    private List<GameObject> meshs;
    private void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>().ToList().Select(m => m.gameObject).ToList();
        gameObject.tag = "Interactable";
    }

    public abstract void Interact();

    public void Highlight()
    {
        if(meshs != null && meshs.Count > 0)
            meshs.ForEach(m => m.layer = 10);
    }
}
