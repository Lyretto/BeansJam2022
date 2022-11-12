using UnityEngine;

public class GameManager : MonoBehaviour
{
    float tiredTimer = 0;
    private TiredTimer timer;
    [SerializeField] private float startTime = 10;
    private void Start()
    {
        timer = gameObject.AddComponent<TiredTimer>();
        timer.tiredTime = startTime;
    }
}
