using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth=100;

    protected float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
        if (currentHealth == 0)
        {
            OnDie();
        }
    }

    protected virtual void OnDie() //Maybe if I had done an interface and then a base class i would only need to make it abstract 
    {
        Debug.Log(gameObject + " has died");
    }
}