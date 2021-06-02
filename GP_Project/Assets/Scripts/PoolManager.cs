using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private ObjectsToPool[] objectsToPools;

    private Dictionary<GameObject, Queue<GameObject>> queues =
        new Dictionary<GameObject, Queue<GameObject>>();

    private Dictionary<GameObject, bool> queueCanGrow = new Dictionary<GameObject, bool>();

    //private Dictionary<GameObject, Queue<GameObject>> prefabAndOriginalQueue = new Dictionary<GameObject, Queue<GameObject>>();

    public static PoolManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InstantiateObjects();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InstantiateObjects()
    {
        foreach(ObjectsToPool obj in objectsToPools)
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            queueCanGrow.Add(obj.ObjectPrefab, obj.CanIncrease);
            //Queue<GameObject> queueObjects = new Queue<GameObject>();
            for(int i=0; i < obj.Ammount; i++)
            {
                GameObject _object = Instantiate(obj.ObjectPrefab, transform);
                _object.SetActive(false);
                newQueue.Enqueue(_object);
            }
            queues.Add(obj.ObjectPrefab, newQueue);
        }
    }

    private GameObject CreateNew(GameObject objectToInstantiate, Vector3 newPos, Quaternion newRotation, Transform newParent)
    {
        GameObject newGameObject= Instantiate(objectToInstantiate,newPos,newRotation,newParent);
        newGameObject.SetActive(false);
        return newGameObject;
    }

    public GameObject ReturnObject(GameObject prefab)
    {
        if(queues.TryGetValue(prefab, out Queue<GameObject> queue))
        {
            if (queue.Count == 0)
            {
                if(queueCanGrow.TryGetValue(prefab, out bool val))
                {
                    if (val)
                    {
                        return CreateNew(prefab, transform.position, prefab.transform.rotation, transform);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null; //It cant increase :)
                }
            }
            else
            {
                GameObject newObject= queue.Dequeue();
                if (newObject)
                {
                    return newObject;
                }
                else
                {
                    if (queueCanGrow.TryGetValue(prefab, out bool val))
                    {
                        if (val)
                        {
                            return CreateNew(prefab, transform.position, prefab.transform.rotation, transform);
                        }
                        else
                        {
                            return null;
                        }
                        
                    }
                    else
                    {
                        return null; //It cant increase :)
                    }
                }
            }
        }
        else
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            queues.Add(prefab, newQueue);
            return CreateNew(prefab, transform.position, prefab.transform.rotation, transform);
        }
    }


    public void DeSpawn(GameObject objectToDespawn, GameObject prefab)
    {
        //Queue<GameObject> theq = queues.GroupBy(p => prefab).ToDictionary(g => g.Key, g => g.Select(pp => pp.Key)); 
        
        if ( queues.TryGetValue(prefab,out Queue<GameObject> newQueue))
        {
            newQueue.Enqueue(objectToDespawn);
        }
        else
        {
            Destroy(objectToDespawn);
        }
    }

}
