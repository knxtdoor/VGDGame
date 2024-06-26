using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction moveAction;

    private CharacterController characterController;
    private Rigidbody rb;
    public float playerMoveSpeed = .3f;

    public float baseSpeed = 3f;
    public float sprintSpeed = 4.5f;
    public float walkSpeed = 1.5f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        characterController = gameObject.AddComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //update player speed
        getSpeed();
        

        Vector2 inputVec = moveAction.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(inputVec.x * playerMoveSpeed, rb.velocity.y, inputVec.y * playerMoveSpeed);
        // characterController.Move(moveVec * playerMoveSpeed);
        rb.velocity = moveVec;

        animator.SetFloat("Speed", new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude);

    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }

    void getSpeed() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            playerMoveSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl)) {
            playerMoveSpeed = walkSpeed;
        }
        else {
            playerMoveSpeed = baseSpeed;
        }
    }
}
