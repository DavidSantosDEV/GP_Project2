using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed=2;
    [SerializeField]
    private float jumpForce=20;

    private float horizontalInput;

    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myBody.velocity = new Vector2(horizontalInput*movementSpeed, myBody.velocity.y);
    }

    public void UpdateHorizontalInput(float newInputX)
    {
        horizontalInput = newInputX;
    }

    public void Jump()
    {
        myBody.AddForce(new Vector2(0,jumpForce));
    }
}
