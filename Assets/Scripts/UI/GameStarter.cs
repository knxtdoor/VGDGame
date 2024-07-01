using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    public void StartGame(){
        SceneManager.LoadScene("SampleSceneCopy");
        Time.timeScale = 1f;
    }
}
