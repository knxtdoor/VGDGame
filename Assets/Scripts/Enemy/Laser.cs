using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(0, 0, 5));
        }

    }
}
