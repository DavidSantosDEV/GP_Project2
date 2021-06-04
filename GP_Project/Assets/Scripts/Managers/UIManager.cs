using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private Image playerChargeImg;
    [SerializeField]
    private Image playerHealthImg;


    public static UIManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCharge(float charge, float maxCharge)
    {
        playerChargeImg.fillAmount = charge/maxCharge;
    }

    public void SetPlayerHealth(float health, float maxHealth)
    {
        playerHealthImg.fillAmount = health/maxHealth;
    }
}
