using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 1f;
    public Transform enemy;
    public Transform player;

    private Vector3 pointA = new Vector3(0, 1, 15); //location to move from
    private Vector3 pointB = new Vector3(10, 1, 15); //location to move towrds
    private Vector3 moveTo;

    private bool hunting = false;

    // Start is called before the first frame update
    void Start()
    {
        moveTo = pointB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);

        Vector3 directionOfPlayer = player.position - transform.position;
        float angleBetween = Vector3.Angle(transform.forward, directionOfPlayer);
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (((angleBetween > 0 && angleBetween < 60) || (angleBetween > 300 && angleBetween < 360)) && playerDistance < 5)
        {
            //Player is in cone of vision, now check for obstruction
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, directionOfPlayer, out rayHit, 5))
            {
                if (rayHit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Player in field of vision");
                    hunting = true;
                }
            }
        }


        // start movng backwards when at a point
        if (!hunting)
        {
            if (transform.position == moveTo)
            {

                if (moveTo == pointB)
                {
                    moveTo = pointA; // Move towards the start position
                }
                else
                {
                    moveTo = pointB;   // Move towards the end position
                }
            }
        }
        else
        {
            moveTo = player.position;
        }
    }
}
