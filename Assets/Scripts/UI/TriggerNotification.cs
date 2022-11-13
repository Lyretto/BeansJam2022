using UnityEngine;

public class TriggerNotification : MonoBehaviour
{
    private Animator animator;
    private void OnEnable()
    {
        GameEvents.Instance.transforming.AddListener(OpenNotification);
    }

    private void OnDisable()
    {
        GameEvents.Instance.transforming.AddListener(OpenNotification);
    }

    private void OpenNotification(PlayerState state)
    {
        if(state == PlayerState.Child)
            animator.SetTrigger("Awake");
        else 
            animator.SetTrigger("Sleep");
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
