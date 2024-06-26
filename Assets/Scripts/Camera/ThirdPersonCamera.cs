using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class ThirdPersonCamera : MonoBehaviour
{
    public float positionSmoothTime = 1f;		// a public variable to adjust smoothing of camera motion
    public float rotationSmoothTime = 1f;
    public float positionMaxSpeed = 50f;        //max speed camera can move
    public float rotationMaxSpeed = 50f;
    private Transform desiredPose;			// the desired pose for the camera, specified by a transform in the game
    public GameObject player;



    public InputActionAsset actions;
    public float sensitivity = 1;
    private InputAction mouse;

    private float rotateHorizontal = 0;
    private float rotateVertical = 0;


    void Start()
    {
        this.desiredPose = player.transform.Find("CameraPos");
        mouse = actions.FindActionMap("Player").FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

    }
    void FixedUpdate()
    {

        // transform.position = player.transform.position + 5;
        Vector2 mouseRead = mouse.ReadValue<Vector2>();
        rotateHorizontal = mouseRead.x;
        rotateVertical = mouseRead.y;
        transform.RotateAround(player.transform.position, -Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        transform.RotateAround(player.transform.position, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player


    }


    void LateUpdate()
    {
        if (desiredPose != null)
        {
            // transform.rotation = desiredPose.rotation;
            // transform.position = desiredPose.transform.position;

        }
    }

}
