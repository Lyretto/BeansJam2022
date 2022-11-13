using System;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    public bool destroyed;
    public bool patched;
    public float multiplier = 1;
    private Collider collider;
    [SerializeField] private GameObject destroyedObject;
    [SerializeField] private GameObject goodObject;


    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        destroyedObject.SetActive(false);
        goodObject.SetActive(true);
    }

    public void HitObstruction()
    {
        GameEvents.Instance.obstructionHit.Invoke(multiplier);
        if (patched)
        {
            patched = false;
            return;
        }

        collider.enabled = false;
        destroyed = true;
        goodObject.SetActive(false);
        destroyedObject.SetActive(true);
        ObstructionManager.Instance.RemoveObstruction(this);
    }

    private void OnEnable()
    {
        ObstructionManager.Instance.remaining.Add(this);
    }
}
