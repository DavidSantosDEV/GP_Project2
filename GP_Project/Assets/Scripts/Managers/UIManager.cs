using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;

    [Header("MainMenu")]
    [SerializeField]
    private GameObject mainMenuParent;

    [Header("Death")]
    [SerializeField]
    private GameObject deathScreenParent;
    [SerializeField]
    private Text restartText;

    [Header("Loading")]
    [SerializeField]
    private GameObject parentLoading;
    [SerializeField]
    private Image progressBar;

    [Header("Player")]
    [SerializeField]
    private GameObject playerUI;
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
            DontDestroyOnLoad(gameObject.transform.parent);
            DontDestroyOnLoad(mainCanvas);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HideMainMenuStuff()
    {

        mainMenuParent.SetActive(false);
    }

    public void ShowMainMenuStuff()
    {
        mainMenuParent.SetActive(true);
    }


    public void ShowGameplayStuff()
    {
        playerUI.SetActive(true);
        HideMainMenuStuff();
    }

    public void HideGameplayStuff()
    {
        playerUI.SetActive(false);
    }

    public void SetCharge(float charge, float maxCharge)
    {
        playerChargeImg.fillAmount = charge/maxCharge;
    }

    public void SetPlayerHealth(float health, float maxHealth)
    {
        if(playerHealthImg)
        playerHealthImg.fillAmount = health/maxHealth;
    }


    public void ShowLoadingStuff()
    {
        parentLoading.SetActive(true);
    }

    public void HideLoadingStuff()
    {
        parentLoading.SetActive(false);
    }

    public void SetProgressLoading(float val)
    {
        progressBar.fillAmount = val;
    }

    public void ShowDeathScreen()
    {
        deathScreenParent.SetActive(true);
    }

    public void SetRestartText(string val)
    {
        restartText.text = val;
    }

    public void HideDeathScreen()
    {
        deathScreenParent.SetActive(false);
    }
}
