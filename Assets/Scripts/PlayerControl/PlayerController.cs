using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
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
    private float playerMoveSpeed = .3f;

    public float baseSpeed = 3f;
    public float sprintSpeed = 4.5f;
    public float walkSpeed = 1.5f;

    private Animator animator;

    public float wallDetectionDistance = 0.5f;



    //Camera control constants
    public GameObject cameraObj;



    //Hologram ability constants
    public GameObject holoPrefab;
    private HologramController activeHolo;


    //Death handling
    public DeathScreen deathScreen;

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
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
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

        moveVec *= playerMoveSpeed;

        if (!IsHittingWall(moveVec))
        {
            rb.velocity = new Vector3(moveVec.x, rb.velocity.y, moveVec.z);
        }
        animator.SetFloat("Speed", inputVec.y * playerMoveSpeed);
        animator.SetFloat("SpeedHorizontal", inputVec.x * playerMoveSpeed);
        
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

        if (collision.gameObject.tag == "Enemy")
        {
            cameraObj.transform.parent = null;
            gameObject.SetActive(false);
            deathScreen.Trigger();
        }
    }

    void OnAnimatorMove()
    {
        if (animator)
        {
            Vector3 newPosition = animator.rootPosition;
            newPosition.y = rb.position.y;
            rb.MovePosition(newPosition);

            Quaternion newRotation = animator.rootRotation;
            rb.MoveRotation(newRotation);
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

    bool IsHittingWall(Vector3 moveDirection)
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, moveDirection, out hit, wallDetectionDistance))
        {
            if (hit.collider.tag != "Ground")
            {
                return true;
            }
        }
        return false;
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
