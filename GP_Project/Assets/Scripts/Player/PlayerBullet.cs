using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed=10;

    [SerializeField]
    private Rigidbody2D body;

    private void OnEnable()
    {
        if(GameManager.Instance)
        body.velocity = speed * (GameManager.Instance.Player.transform.right*-1);
    }

    private void OnDisable()
    {
        body.velocity = Vector2.zero;
    }
}