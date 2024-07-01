using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    MonitorController mc;
    void Start()
    {
        mc = GetComponentInParent<MonitorController>();
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            mc.SetPlayerInRange(true);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            mc.SetPlayerInRange(false);
        }
    }
}
