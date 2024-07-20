using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonTriggerController : MonoBehaviour
{
    EnemyButtonController ebc;
    void Start()
    {
        ebc = GetComponentInParent<EnemyButtonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            ebc.SetPlayerInRange(true);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            ebc.SetPlayerInRange(false);
        }
    }
}
