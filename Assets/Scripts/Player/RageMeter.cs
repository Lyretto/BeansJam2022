using UnityEngine;

public class RageMeter : MonoBehaviour
{
    private float rageCount;
    private float rageMulti = 1;
    public float ragePerObstruction = 1;
    public float startRage = 5;

    private void OnEnable()
    {
        GameEvents.Instance.obstructionHit.AddListener((obstruction) => OnDestroyObstruction(obstruction.multiplier));
    }
    
    private void OnDisable()
    {
        GameEvents.Instance.obstructionHit.RemoveListener((obstruction) => OnDestroyObstruction(obstruction.multiplier));
    }


    private void Start()
    {
        ResetMeter();
    }

    public void ResetMeter()
    {
        rageCount = startRage;
    }
    
    private void OnDestroyObstruction(float obstructionMulti = 1)
    {
        rageCount -= ragePerObstruction * rageMulti * obstructionMulti;

        if (rageCount > 0) return;
        GameEvents.Instance.transforming.Invoke(PlayerState.Child);
    }
    
}
