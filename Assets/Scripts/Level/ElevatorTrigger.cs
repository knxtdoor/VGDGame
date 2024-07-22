using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    ElevatorController parent;
    void Start()
    {
        parent = GetComponentInParent<ElevatorController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            parent.SetPlayerInRange(true);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            parent.SetPlayerInRange(false);
        }
    }
}
