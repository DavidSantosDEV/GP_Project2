using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    [SerializeField]
    private PlayerController _playerController;

    public PlayerController Player
    {
        get => _playerController;
    }

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



        if (!_playerController)
        {
            _playerController = FindObjectOfType<PlayerController>(); //Assuming we only have one player on the scene
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(1);
    }

    

    public void CloseGame()
    {
        if (Debug.isDebugBuild)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }

    public void PlayerDied()
    {
        _playerController.OnDeath();
        //Do game over UI thing.
    }

}
