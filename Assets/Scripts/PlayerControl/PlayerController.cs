using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public float baseSpeed = 1f;
    public float sprintSpeed = 1.5f;
    public float walkSpeed = 0.5f;

    private Animator animator;

    public float wallDetectionDistance = 0.5f;

    //Camera control constants
    public GameObject cameraObj;

    //Hologram ability constants
    public GameObject holoPrefab;
    private HologramController activeHolo;

    //Death handling
    public DeathScreen deathScreen;

    private float previousVelX = 0f;
    private float previousVelY = 0f;
    public float smoothingFactorX = 10f;
    public float smoothingFactorY = 10f;

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
        float targetVelX = inputVec.x * playerMoveSpeed;
        float targetVelY = inputVec.y * playerMoveSpeed;

        previousVelX = Mathf.Lerp(previousVelX, targetVelX, Time.deltaTime * smoothingFactorX);
        previousVelY = Mathf.Lerp(previousVelY, targetVelY, Time.deltaTime * smoothingFactorY);

        animator.SetFloat("velx", previousVelX);
        animator.SetFloat("vely", previousVelY);

        if (doHologram.ReadValue<float>() > 0)
        {
            HandleHologram();
        }
        if (transform.position.y < -100)
        {
            cameraObj.transform.parent = null;
            gameObject.SetActive(false);
            deathScreen.Trigger();
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
            Kill();
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

    public void Kill()
    {
        cameraObj.transform.parent = null;
        gameObject.SetActive(false);
        deathScreen.Trigger();
    }
    public void InteractAnimation()
    {
        animator.SetTrigger("Interact");
    }
}
