using UnityEngine;

public class WakeAOE : MonoBehaviour
{
    [SerializeField] public float modifier = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().WakeAoes.Add(this);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().WakeAoes.Remove(this);
    }
}
