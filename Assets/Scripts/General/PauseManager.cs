using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool paused = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (paused)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0;
            }
            paused = !paused;
        }

    }
}
