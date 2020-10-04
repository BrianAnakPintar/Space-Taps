using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("game");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
