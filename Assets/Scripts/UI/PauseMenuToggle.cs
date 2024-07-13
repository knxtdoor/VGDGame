using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{

    public CanvasGroup pauseMenu;
    public CanvasGroup controlsMenu;


    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseMenu.interactable)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Unpause()
    {
        Debug.Log("Unpause");
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
        pauseMenu.alpha = 0f;

        controlsMenu.interactable = false;
        controlsMenu.blocksRaycasts = false;
        controlsMenu.alpha = 0f;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;
        pauseMenu.alpha = 1f;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }


    public void OpenControls()
    {
        controlsMenu.interactable = true;
        controlsMenu.blocksRaycasts = true;
        controlsMenu.alpha = 1f;
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
    }
    public void CloseControls()
    {
        controlsMenu.interactable = false;
        controlsMenu.blocksRaycasts = false;
        controlsMenu.alpha = 0f;
        pauseMenu.alpha = 1f;
        pauseMenu.interactable = true;
    }


}
