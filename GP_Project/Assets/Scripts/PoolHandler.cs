using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolHandler : MonoBehaviour
{
    private Queue<GameObject> myQueue = null;

    public void SettupQueue(Queue<GameObject> poolQueue)
    {
        myQueue = poolQueue;
    }

    public void DeActivate()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            PoolManager.Instance.DeSpawn(gameObject, myQueue);
        }
    }
}
