using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string currLevelName = "luting_map";

    public static LevelManager instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
