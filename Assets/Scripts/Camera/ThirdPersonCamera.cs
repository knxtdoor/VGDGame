using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
    public float positionSmoothTime = 1f;		// a public variable to adjust smoothing of camera motion
    public float rotationSmoothTime = 1f;
    public float positionMaxSpeed = 50f;        //max speed camera can move
    public float rotationMaxSpeed = 50f;
    public Transform player;

    public Vector3 offset = new Vector3(0, 4, -5);

    protected Vector3 currentPositionCorrectionVelocity;


    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {

        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentPositionCorrectionVelocity, positionSmoothTime, positionMaxSpeed, Time.deltaTime);
            transform.rotation = Quaternion.Euler(30, 0, 0);        }
    }
}
