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

    private Animator animator;
    private float timeout = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (hologramActive)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, hologramSpeed * Time.deltaTime);
            animator.SetBool("active", hologramActive);
            animator.SetFloat("Speed", hologramSpeed);
            this.timeout = timeout - Time.deltaTime;
            if (timeout <= 0)
            {
                Destroy(this.gameObject);
            }

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
