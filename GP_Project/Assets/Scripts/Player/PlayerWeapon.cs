using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform attackPoint=null;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float maxWeaponGauge=20;
    [SerializeField]
    private float shotCost;


    public GameObject BulletPrefab => bulletPrefab;

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
                _playerAnimation.AttackAnimation();
                isAttacking = true;
            }
        }
        
    }

    public void DoAttackEffect() //called by animator
    {
        GameObject bullet = PoolManager.Instance.ReturnObject(bulletPrefab);
        if (bullet)
        {
            bullet.transform.position = attackPoint.position;
            bullet.transform.rotation = attackPoint.rotation;
            bullet.SetActive(true);
            currentGauge -= shotCost;
            UIManager.Instance.SetCharge(currentGauge, maxWeaponGauge);
        }
        Debug.Log("Attack");
    }

    public void ResetComponent()
    {
        currentGauge = maxWeaponGauge;
        isAttacking = false;
        UIManager.Instance.SetCharge(currentGauge, maxWeaponGauge);
    }
}
