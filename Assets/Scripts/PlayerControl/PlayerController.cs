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
    public float playerMoveSpeed = .05f;

    // Start is called before the first frame update
    void Start()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        characterController = gameObject.AddComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputVec = moveAction.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(inputVec.x * playerMoveSpeed, rb.velocity.y, inputVec.y * playerMoveSpeed);
        // characterController.Move(moveVec * playerMoveSpeed);
        rb.velocity = moveVec;


    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
}
