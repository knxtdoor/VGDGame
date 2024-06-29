using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonTriggerController : MonoBehaviour
{
    // Start is called before the first frame update

    DoorButtonController dbc;
    void Start()
    {
        dbc = GetComponentInParent<DoorButtonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            dbc.SetPlayerInRange(true);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            dbc.SetPlayerInRange(false);
        }
    }
}
