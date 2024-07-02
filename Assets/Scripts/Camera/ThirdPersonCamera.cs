using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject player;

    public InputActionAsset actions;
    private InputAction mouse;
    public float sensitivity = 1;

    private float rotateVertical = 0;
    private float rotateHorizontal = 0;
    private float cameraDistance = 5;

    private Vector3 oldPlayerPos;


    void Start()
    {
        mouse = actions.FindActionMap("Player").FindAction("Look");
        transform.LookAt(player.transform);
        Cursor.lockState = CursorLockMode.Locked;
        oldPlayerPos = player.transform.position;
    }

    void FixedUpdate()
    {

        Vector3 playerOffset = player.transform.position - oldPlayerPos;
        this.transform.position += playerOffset;
        //Read mouse input for camera rotation
        Vector2 mouseRead = mouse.ReadValue<Vector2>();
        rotateVertical = mouseRead.y;
        rotateHorizontal = mouseRead.x;
        //Rotate camera based on mouse input, ignoring collisions
        transform.RotateAround(player.transform.position, transform.right, rotateVertical * sensitivity);
        transform.RotateAround(player.transform.position, transform.up, rotateHorizontal * sensitivity);
        // Limit x rotation to prevent rotating around characters head or feet
        float xAngle = transform.eulerAngles.x;
        if (xAngle > 180)
        {
            xAngle -= 360;
        }
        float angleLimit = 65;
        if (xAngle < -angleLimit)
        {
            transform.RotateAround(player.transform.position, transform.right, -angleLimit - xAngle);
        }
        else if (xAngle > angleLimit)
        {
            transform.RotateAround(player.transform.position, transform.right, angleLimit - xAngle);

        }


        //Get vector between player and new camera location, normalized
        Vector3 rayDir = (this.transform.position - player.transform.position).normalized;

        //Raycast between the player and the camera
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, rayDir, out hit, cameraDistance))
        {
            //If there is an object in the way, place the camera at the collision point
            this.transform.position = player.transform.position + (rayDir * hit.distance);
        }
        else
        {
            //Otherwise place it at max camera distance
            this.transform.position = player.transform.position + (rayDir * cameraDistance);

        }

        oldPlayerPos = player.transform.position;

    }



}
