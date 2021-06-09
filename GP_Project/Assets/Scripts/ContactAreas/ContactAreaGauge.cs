using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactAreaGauge : ContactAreaBase
{
    [Header("Gauge")]
    [SerializeField]
    private float gaugePerTick=10;
    protected override void DoEffect()
    {
        base.DoEffect();
        GameManager.Instance.Player.PlayerWeaponComponent.AddGauge(gaugePerTick);
    }
}
