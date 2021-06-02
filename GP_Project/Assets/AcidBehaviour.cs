using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBehaviour : MonoBehaviour
{
    [SerializeField]
    private float damage=20;
    [SerializeField]
    private float speed=2;

    private Rigidbody2D mybody;

    private AcidThrower myThrower;
    
    private void Awake()
    {
        myThrower = FindObjectOfType<AcidThrower>(); //Lets assume there is only one

        mybody = GetComponent<Rigidbody2D>();
        if (!mybody)
        {
            mybody = gameObject.AddComponent<Rigidbody2D>();
            mybody.gravityScale = 0;
        }
    }

    private void OnEnable()
    {
        mybody.velocity = Vector2.up * speed;
    }

    private void OnDisable()
    {
        mybody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.PlayerHealthComponent?.TakeDamage(damage);
        }
        PoolManager.Instance.DeSpawn(gameObject, myThrower.AcidPrefab);
    }
}
