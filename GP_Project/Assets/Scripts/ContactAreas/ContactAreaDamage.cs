using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAreaDamage : ContactAreaBase
{
    [Header("Damage")]
    [SerializeField]
    private float damagePerTick;
    protected override void DoEffect()
    {
        base.DoEffect();
        GameManager.Instance.Player.PlayerHealthComponent.TakeDamage(damagePerTick);
    }
}
