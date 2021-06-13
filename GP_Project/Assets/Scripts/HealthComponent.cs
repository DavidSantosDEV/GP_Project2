using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth=100;

    protected float currentHealth;

    private bool isDead=false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {
        if (isDead==false)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
            if (currentHealth == 0)
            {
                OnDie();
            }
        }    
    }

    protected virtual void OnDie() //Maybe if I had done an interface and then a base class i would only need to make it abstract 
    {
        isDead = true;
        Debug.Log(gameObject + " has died");
    }

    public virtual void ResetComponent()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public virtual void Heal(float ammount)
    {
        currentHealth = Mathf.Clamp(currentHealth + ammount, 0, maxHealth);
    }
}
