using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidThrower : MonoBehaviour
{
    [SerializeField]
    private GameObject acidPrefab;

    [SerializeField]
    private float timeRandomSpawn;
    [SerializeField]
    private float timeTolerance;


    public GameObject AcidPrefab=>acidPrefab;

    private BoxCollider2D col;

    private float widthCol;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        widthCol = col.bounds.size.x;
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(RandomBubbleSpawning));
    }

    private IEnumerator RandomBubbleSpawning()
    {
        while (enabled)
        {

            yield return new WaitForSeconds(timeRandomSpawn - Random.Range(0, timeTolerance));

        }
    }
}
