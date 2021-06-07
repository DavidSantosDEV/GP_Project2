using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed=10;

    [SerializeField]
    private Rigidbody2D body;

    private PoolHandler myHandler;

    private void Awake()
    {
        myHandler = GetComponent<PoolHandler>();
    }

    private void OnEnable()
    {
        if(GameManager.Instance && GameManager.Instance.Player!=null)
        body.velocity = speed * (GameManager.Instance.Player.transform.right*-1);
    }

    private void OnDisable()
    {
        body.velocity = Vector2.zero;
        
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Do other stuff here I guess

        myHandler.DeActivate();
    }

    
}
