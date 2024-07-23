using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    public void StartGame()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelManager.instance.currLevelName);
    }

    public void TitleScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}
