using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField]
    private Transform attackPoint=null;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float maxWeaponGauge=20;
    [SerializeField]
    private float shotCost;

    

    private float currentGauge;

    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();

        currentGauge = maxWeaponGauge;
    }

    public void Attack()
    {
        if (currentGauge - shotCost > 0)
        {
            currentGauge -= shotCost;
            _playerAnimation.AttackAnimation();
        }
    }

    public void DoAttackEffect()
    {
        PoolManager.Instance.ReturnObject(ObjectIndex.Bullet);

        Debug.Log("Attack");
    }
}
