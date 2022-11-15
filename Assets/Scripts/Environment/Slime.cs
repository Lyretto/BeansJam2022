using UnityEngine;

public class Slime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().isChild)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/In Schleim stecken");
            GameEvents.Instance.rage.Invoke();
            Destroy(transform.parent.gameObject, 2f);
        }
    }
}
