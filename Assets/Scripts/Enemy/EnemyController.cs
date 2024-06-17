
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 1f;
    public float rotateSpeed = 220f;
    public Transform enemy;
    public Transform player;

    private Vector3 pointA = new Vector3(0, 1, 15); //location to move from
    private Vector3 pointB = new Vector3(10, 1, 15); //location to move towrds
    private Vector3 moveTo;
    private Vector3 lookTo;

    private GameObject huntTarget = null;


    private Mesh mesh;

    public float visionRange = 5;
    public float FOV = 120;

    // Start is called before the first frame update
    void Start()
    {
        moveTo = pointB;
        lookTo = pointB;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookTo - this.transform.position), rotateSpeed * Time.deltaTime);

        CheckForTarget();


        // start movng backwards when at a point
        if (huntTarget == null)
        {
            if (transform.position == moveTo)
            {

                if (moveTo == pointB)
                {
                    moveTo = pointA; // Move towards the start position
                    lookTo = pointA;
                }
                else
                {
                    moveTo = pointB;   // Move towards the end position
                    lookTo = pointB;

                }
            }
        }
        else
        {
            moveTo = huntTarget.transform.position;
            lookTo = huntTarget.transform.position;
        }
    }


    void CheckForTarget()
    {
        float degreeIncrement = Mathf.PI / 8;
        float facingDirection = Mathf.Atan2(this.transform.forward.z, this.transform.forward.x);
        float FOVrad = Mathf.Deg2Rad * FOV;

        for (float i = facingDirection - (FOVrad / 2); i < facingDirection + (FOVrad / 2); i += degreeIncrement)
        {
            Vector3 currDirection = new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i));
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, currDirection, out rayHit, visionRange))
            {
                string collisionTag = rayHit.collider.gameObject.tag;
                if (collisionTag == "Player" || collisionTag == "Hologram")
                {
                    Debug.Log("Player/Hologram in field of vision");
                    huntTarget = rayHit.collider.gameObject;
                }
            }
        }

    }
}
