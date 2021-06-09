using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAreaHeal : ContactAreaBase
{
    [Header("Heal")]
    [SerializeField]
    private float healPerTick = 10;
    protected override void DoEffect()
    {
        base.DoEffect();
        GameManager.Instance.Player.PlayerHealthComponent.Heal(healPerTick);
    }
}
