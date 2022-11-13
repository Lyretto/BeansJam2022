using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject itemToSpread;
    [SerializeField] private int objectAmount = 10;

    [SerializeField] private float maxWidth = 10;
    [SerializeField] private float maxLength = 10;
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private LayerMask objectLayer;

    private int maxBuilding;
    private int buildingCounter = 0;
    [SerializeField] private float buildingPercentage = 0.5f;

    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<GameObject> respawnableObj;

    private int bufferCount = 100;
    
    // Start is called before the first frame update
    void Start()
    {        
        GenerateMap();
    }

    private void OnEnable()
    {
        GameEvents.Instance.transforming.AddListener(SpawnOnRuntime);
    }

    private void OnDisable()
    {
        GameEvents.Instance.transforming.RemoveListener(SpawnOnRuntime);
    }

    void SpawnOnRuntime(PlayerState state)
    {
        int spawnOnRuntimeBufferCount = 30;
        if(state == PlayerState.Child)
        {
            for (int i = 0; i < 3; i++)
            {
                itemToSpread = respawnableObj[Random.Range(0, respawnableObj.Count)];
                if (spawnOnRuntimeBufferCount <= 0) return;
                spawnOnRuntimeBufferCount--;

                Vector3 randPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 0, Random.Range(-maxLength, maxLength)) + transform.position;
                if (CheckPosition(randPosition)) PlaceObject(randPosition);
                else GeneratePosition();
            }
        }
    }

    void GenerateMap()
    {
        maxBuilding = (int)(buildingPercentage * objectAmount);
        for (int i = 0; i < objectAmount; i++)
        {
            ChooseObject();
        }
    }

    void GeneratePosition()
    {
        if (bufferCount <= 0) return;
        bufferCount--;
        
        Vector3 randPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 0, Random.Range(-maxLength, maxLength))+transform.position;
        if (CheckPosition(randPosition)) PlaceObject(randPosition);
        else GeneratePosition();
    }

    bool CheckPosition(Vector3 pos)
    {
        distance = itemToSpread.GetComponent<SphereCollider>().radius;
        if(Physics.CheckSphere(pos, distance, objectLayer)) return false;
        else return true;
    }

    void PlaceObject(Vector3 pos)
    {
        GameObject clone = Instantiate(itemToSpread, pos, Quaternion.identity);
        clone.transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
        itemToSpread = null;
    }

    void ChooseObject()
    {

        //Gebäude auswählen
        if (buildingCounter < maxBuilding)
        {
            itemToSpread = buildings[Random.Range(0, buildings.Count)];
            buildingCounter++;
        }
        else
        {
            itemToSpread = objects[Random.Range(0, objects.Count)];
        }

        bufferCount += objectAmount;
        GeneratePosition();
    }
}
