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

    //private AcidThrower myThrower;
    private PoolHandler myHandler;
    private Animator myAnim;
    private void Awake()
    {
        myAnim=GetComponent<Animator>();
        myHandler = GetComponent<PoolHandler>();
        mybody = GetComponent<Rigidbody2D>();
        if (!mybody)
        {
            mybody = gameObject.AddComponent<Rigidbody2D>();
            mybody.gravityScale = 0;
        }
    }

    private void OnEnable()
    {
        myAnim.SetTrigger("Grow");
        mybody.velocity = Vector2.up * speed;
    }

    private void OnDisable()
    {
        mybody.velocity = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        myHandler.DeActivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIG");
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.PlayerHealthComponent?.TakeDamage(damage);
            myAnim.SetTrigger("Blow");
        }
        
        //myHandler.DeActivate();
    }
}
