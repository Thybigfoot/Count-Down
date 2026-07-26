using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // build index of level 1
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); // so you can confirm it fired in the editor
    }
}