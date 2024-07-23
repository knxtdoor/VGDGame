using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollision : MonoBehaviour
{

    private List<string> levelNames = new List<string>(){
        "Luting_map",
        "ZachLevel",
        "Will_Map",
        "Sagar_Level",
        "EndGame"
    };
    public int currLevel = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene(levelNames[currLevel + 1]);
            Time.timeScale = 1f;
        }
    }
}
