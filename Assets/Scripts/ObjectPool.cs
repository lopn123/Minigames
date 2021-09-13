using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private int createObjectCount;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    [SerializeField]
    private GameObject objectPrefab;

    private void Awake()
    {
        instance = this;
        CreateObject();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void CreateObject()
    {
        for (int i = 0; i < createObjectCount; i++)
        {
            GameObject obj = Instantiate(objectPrefab, this.transform);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }

    public void SpawnObject()
    {
        if (objectQueue.Count > 0)
        {
            GameObject obj = objectQueue.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab, this.transform);
            obj.SetActive(true);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}