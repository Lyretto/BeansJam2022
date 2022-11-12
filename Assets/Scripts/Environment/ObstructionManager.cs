using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstructionManager : MonoBehaviour
{
    private static ObstructionManager _instance;
    [SerializeField] private List<GameObject> obstructionsPrefabs;
    private readonly List<Obstruction> remaining = new ();

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

    private void OnEnable()
    {
        GameEvents.Instance.obstructionHit.AddListener(RemoveObstruction);
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    public void RemoveObstruction(Obstruction destroyedOb)
    {
        if (destroyedOb && !destroyedOb.destroyed) return;
        remaining.Remove(destroyedOb);
        if (remaining.Count <= 0)
        {
            GameEvents.Instance.lastObsctructionDestroyed.Invoke();
        }
    }
}
