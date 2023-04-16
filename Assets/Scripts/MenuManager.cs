using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadEndMenu()
    {
        SceneManager.LoadScene("EndMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
