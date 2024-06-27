using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{



    //Input related constants
    public InputActionAsset actions;
    private InputAction moveAction;
    private InputAction doHologram;
    private InputAction look;
    private InputAction doWalk;
    private InputAction doSprint;


    //Movement and speed constants
    private Rigidbody rb;
    public float playerMoveSpeed = .3f;

    public float baseSpeed = 3f;
    public float sprintSpeed = 4.5f;
    public float walkSpeed = 1.5f;


    //Camera control constants
    public GameObject cameraObj;



    //Hologram ability constants
    public GameObject holoPrefab;
    private HologramController activeHolo;

    // Start is called before the first frame update
    void Start()
    {
        InputActionMap playerActionMap = actions.FindActionMap("Player");

        //Get input objects to read in Update();
        moveAction = playerActionMap.FindAction("Move");
        doHologram = playerActionMap.FindAction("Ability1");
        look = playerActionMap.FindAction("Look");
        doWalk = playerActionMap.FindAction("Walk");
        doSprint = playerActionMap.FindAction("Sprint");


        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update player speed
        getSpeed();


        ThirdPersonCamera tpc = cameraObj.GetComponent<ThirdPersonCamera>();
        float mouseX = look.ReadValue<Vector2>().x;
        transform.Rotate(new Vector3(0, mouseX * tpc.sensitivity, 0));

        //Get movement (WASD) input, and create movement vector
        Vector2 inputVec = moveAction.ReadValue<Vector2>();
        Vector3 moveVec = (transform.forward * (inputVec.y * playerMoveSpeed)) + (transform.right * (inputVec.x * playerMoveSpeed));
        moveVec.y = rb.velocity.y;
        rb.velocity = moveVec;


        //Handle the hologram ability
        if (doHologram.ReadValue<float>() > 0)
        {
            HandleHologram();
        }
    }



    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Enemy")
        {
            cameraObj.transform.parent = null;
            gameObject.SetActive(false);
        }
    }

    void getSpeed()
    {

        if (doSprint.ReadValue<float>() > 0)
        {
            playerMoveSpeed = sprintSpeed;
        }
        else if (doWalk.ReadValue<float>() > 0)
        {
            playerMoveSpeed = walkSpeed;
        }
        else
        {
            playerMoveSpeed = baseSpeed;
        }
    }

    void HandleHologram()
    {
        if (activeHolo == null)
        {
            GameObject newHologram = Instantiate(holoPrefab);
            newHologram.transform.SetPositionAndRotation(this.transform.position + (this.transform.forward * 2), this.transform.rotation);
            activeHolo = newHologram.GetComponent<HologramController>();
            activeHolo.player = this;
            Vector3 holoDest = this.transform.position + (this.transform.forward * 10);
            activeHolo.DispatchHologram(holoDest);
        }
    }
}
