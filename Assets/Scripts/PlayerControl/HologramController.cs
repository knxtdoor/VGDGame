using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController player;

    private bool hologramActive = false;
    private Vector3 destination;

    private float hologramSpeed = 2.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hologramActive)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, hologramSpeed * Time.deltaTime);


            if (Vector3.Distance(this.transform.position, destination) < .05)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void DispatchHologram(Vector3 dest)
    {
        hologramActive = true;
        destination = dest;
    }
}
