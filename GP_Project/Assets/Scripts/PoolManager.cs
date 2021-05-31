using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectIndex
{
    Bullet= 0,
    AcidBubbles = 1,
}


[System.Serializable]
public class ObjectsToPool
{
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    private int ammount;
    [SerializeField]
    private bool canIncrease=false;
    
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InstantiateObjects();
    }

    private void InstantiateObjects()
    {

    }

    public GameObject ReturnObject(ObjectIndex obj)
    {
        return null;
    }
}
