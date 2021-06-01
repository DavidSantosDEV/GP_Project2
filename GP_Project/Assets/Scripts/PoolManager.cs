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


    public int Ammount => ammount;

    public bool CanIncrease => canIncrease;

    public GameObject ObjectPrefab => objectPrefab;
}

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    ObjectsToPool[] objectsToPools;

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
        foreach(ObjectsToPool obj in objectsToPools)
        {
            for(int i=0; i < obj.Ammount; i++)
            {
                GameObject _object = Instantiate(obj.ObjectPrefab, transform);
                _object.SetActive(false);
            }
        }
    }

    public GameObject ReturnObject(ObjectIndex obj)
    {
        return null;
    }
}
