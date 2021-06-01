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
    private bool isAttacking=false;

    public bool IsAttacking
    {
        set
        {
            isAttacking = value;
        }
    }


    private PlayerAnimation _playerAnimation;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();

        currentGauge = maxWeaponGauge;
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            if (currentGauge - shotCost > 0)
            {
                currentGauge -= shotCost;
                _playerAnimation.AttackAnimation();
                isAttacking = true;
            }
        }
        
    }

    public void DoAttackEffect()
    {
        //PoolManager.Instance.ReturnObject(ObjectIndex.Bullet);

        Debug.Log("Attack");
    }
}
