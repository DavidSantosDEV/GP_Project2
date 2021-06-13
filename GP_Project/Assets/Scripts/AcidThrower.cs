using System.Collections;
using UnityEngine;

public class AcidThrower : MonoBehaviour
{
    [SerializeField]
    private GameObject acidPrefab;

    [SerializeField]
    private float timeRandomSpawn;
    [SerializeField]
    private float timeTolerance;

    private float extentsCol; //Size of the collider's X
    void Awake()
    {
        extentsCol = GetComponent<BoxCollider2D>().bounds.extents.x;
    }

    private void Start()
    {
        StartCoroutine(nameof(RandomBubbleSpawning));
    }

    private IEnumerator RandomBubbleSpawning()
    {
        while (enabled)
        {
            GameObject acidBubble = PoolManager.Instance?.ReturnObject(acidPrefab);
            if (acidBubble)
            {
                float x = Random.Range(-extentsCol, extentsCol);
                //Debug.Log(x);
                acidBubble.transform.position = new Vector2(x, transform.position.y); //The x is a random value between the edges of the collider |-------| so the acid spawns in between randomly
                acidBubble.transform.rotation = transform.rotation;
                acidBubble.SetActive(true);
            }
            yield return new WaitForSeconds(timeRandomSpawn - Random.Range(0, timeTolerance));
        }
    }
}
