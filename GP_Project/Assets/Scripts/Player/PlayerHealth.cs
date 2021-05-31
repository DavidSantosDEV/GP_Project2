using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        //TODO UI STUFF HERE
    }
    protected override void OnDie()
    {
        base.OnDie();
        GameManager.Instance.PlayerDied();
    }

}