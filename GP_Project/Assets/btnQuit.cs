using UnityEngine;

public class btnQuit : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.CloseGame();
    }
}
