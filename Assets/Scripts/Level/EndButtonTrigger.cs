using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButtonTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    EndButtonController mc;
    void Start()
    {
        mc = GetComponentInParent<EndButtonController>();
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
