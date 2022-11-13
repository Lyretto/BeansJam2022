using System.Collections;
using UnityEngine;

public class Sweets : Interactables
{
    [SerializeField] private float wakeTime = 1;
    
    public override bool IsActivated()
    {
        return false;
    }

    public override void Interact()
    {
        GameEvents.Instance.GetComponent<TiredTimer>().AddTime(wakeTime);

        StartCoroutine(Consum());
    }

    private IEnumerator Consum()
    {
        yield return null;
        Destroy(gameObject);
    }
}
