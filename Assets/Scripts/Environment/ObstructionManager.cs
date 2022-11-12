using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstructionManager : MonoBehaviour
{
    private static ObstructionManager _instance;
    [SerializeField] private List<GameObject> obstructionsPrefabs;
    private readonly List<Obscrutction> remaining = new ();

    public static ObstructionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObstructionManager>();
            }

            return _instance;
        }
    }
    
    public void SpawnRandomObstruction(Vector3 position, Transform parent)
    {
        var newObstruction =  Instantiate(obstructionsPrefabs[Random.Range(0, obstructionsPrefabs.Count)], position, Quaternion.identity, parent ? parent : transform);
        remaining.Add(newObstruction.GetComponent<Obscrutction>());
    }

    public void RemoveObstruction(Obscrutction destroyedOb)
    {
        remaining.Remove(destroyedOb);
        if (remaining.Count <= 0)
        {
            //TODO GAME END
        }
    }
}
