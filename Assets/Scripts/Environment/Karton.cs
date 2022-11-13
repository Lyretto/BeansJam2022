using UnityEngine;

public class Karton : Interactables
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject foldedBox;
    [SerializeField] private GameObject destructOb;
    private BoxCollider boxCollider;
    private bool activated;
    public override bool IsActivated() => activated;
    
    public override void Interact()
    {
        box.SetActive(true);
        foldedBox.SetActive(false);
        destructOb.SetActive(false);
        boxCollider.enabled = true;
        activated = true;
    }
    
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        box.SetActive(false);
        foldedBox.SetActive(true);
    }
    
    public void HitBox()
    {
        if (activated)
        {
            foldedBox.SetActive(true);
            box.SetActive(false);
            destructOb.SetActive(true);
            activated = false;
            boxCollider.enabled = false;
            GameEvents.Instance.obstructionHit.Invoke(1);
        }
    }
}
