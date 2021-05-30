using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void UpdateMovementAnimation(float horizontal, float vertical)
    {
        myAnimator.SetFloat("Horizontal", horizontal);
        myAnimator.SetFloat("Vertical", vertical);
    }

    public void UpdateGrounded(bool grounded)
    {
        myAnimator.SetBool("Grounded", grounded);
    }

    public void AttackAnimation()
    {
        myAnimator.SetTrigger("Attack");
    }
}
