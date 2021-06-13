using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    [SerializeField]
    private PlayerController _playerController;

    private Vector2 startingPoint=Vector2.zero; //level starting point

    [SerializeField]
    private int timeLevelReset=3;
    [SerializeField]
    private int gameOverTime=4;

    private int currentLevel;

    Portal[] Allportals;

    public PlayerController Player
    {
        get => _playerController;
    }

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform.parent);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene openScene,LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject sp = GameObject.FindWithTag("StartingPoint");
            if (sp)
            {
                Debug.Log(sp);
                startingPoint = sp.transform.position; //Try and find I guess
            }

            Allportals = FindObjectsOfType<Portal>();

            Allportals = Allportals.Where(val => val.PortalIsActive != false).ToArray();

            if (!_playerController)
            {
                _playerController = FindObjectOfType<PlayerController>();
                if (_playerController)
                {
                    DontDestroyOnLoad(_playerController.gameObject);
                    UIManager.Instance.ShowGameplayStuff();
                    if (startingPoint != Vector2.zero)
                    {
                        _playerController.transform.position = startingPoint;
                    }
                }
                //Assuming we only have one player on the scene
            }
            else
            {
                if (startingPoint != Vector2.zero)
                {
                    _playerController.transform.position = startingPoint;
                }else
                {
                    _playerController.transform.position = Vector2.zero;
                }
            }
        }
        else
        {
            UIManager.Instance.ShowMainMenuStuff();
            UIManager.Instance.HideGameplayStuff();
            if (_playerController)
            {
                Destroy(_playerController.gameObject);
            }
        }
    }

    public void OnPortalUsed()
    {
        foreach(Portal p in Allportals)
        {
            if (!p.PortalUsed)
            {
                return;
            }
        }
        NextLevel();
    }

    public void OpenLevel(int index)
    {
        if (index != currentLevel)
        {
            startingPoint = Vector2.zero;
        }
        StartCoroutine(LoadScene(index));
    }

    private void NextLevel()
    {
        int level = currentLevel + 1;
        if (level > SceneManager.sceneCountInBuildSettings-1)
        {
            StartCoroutine(GameOverWin());
        }
        else
        {
            OpenLevel(level);
        }
    }

    private IEnumerator GameOverWin()
    {
        Destroy(_playerController.gameObject);
        _playerController = null;

        UIManager.Instance.ShowWinScreen();
        yield return new WaitForSeconds(gameOverTime);
        OpenLevel(0);
    }

    private IEnumerator LoadScene(int indexLevel)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(indexLevel);
        UIManager.Instance.HideWinScreen();
        UIManager.Instance.ShowLoadingStuff();
        UIManager.Instance.SetProgressLoading(0);
        while (!async.isDone)
        {
            UIManager.Instance.SetProgressLoading(async.progress);
            yield return null;
        }
        UIManager.Instance.SetProgressLoading(async.progress);
        yield return null;
        UIManager.Instance.HideLoadingStuff();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void CloseGame()
    {
        if (Debug.isDebugBuild)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }


    private void ReloadCurrentScene()
    {
        //use current level variable
    }

    public void PlayerDied()
    {
        _playerController.OnDeath();
        UIManager.Instance.ShowDeathScreen();
        StartCoroutine(DeathRoutine());
    }

    private void ResetLevel()
    {
        foreach(Portal p in Allportals)
        {
            p.PortalUsed = false;
        }
        _playerController.gameObject.transform.position = startingPoint;
        _playerController.ResetComponents();
    }

    private IEnumerator DeathRoutine()
    {
        UIManager.Instance.SetRestartText("Restarting in: " + timeLevelReset);
        for(int i = timeLevelReset;  i>0; i--)
        {
            UIManager.Instance.SetRestartText("Restarting in: " + i);
            yield return new WaitForSeconds(1);
        }
        UIManager.Instance.HideDeathScreen();
        ResetLevel();
        ReloadCurrentScene();
    }


   // private void Update()
    //{
      //  Debug.Log("working");
    //}
}
