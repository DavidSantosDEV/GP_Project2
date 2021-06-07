using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        UIManager.Instance.SetPlayerHealth(currentHealth, maxHealth);
    }
    protected override void OnDie()
    {
        base.OnDie();
        GameManager.Instance.PlayerDied();
    }

    public override void ResetComponent()
    {
        base.ResetComponent();
        UIManager.Instance.SetPlayerHealth(currentHealth, maxHealth);
    }

}
