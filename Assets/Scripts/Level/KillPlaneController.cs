using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    void OnTriggerEnter(Collider c)
    {
        player.Kill();

    }
}
