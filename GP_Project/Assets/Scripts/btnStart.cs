using UnityEngine;

public class btnStart : MonoBehaviour
{
    public void StartGame()
    {
        UIManager.Instance.HideMainMenuStuff();
        GameManager.Instance.OpenLevel(1);
    }
}
