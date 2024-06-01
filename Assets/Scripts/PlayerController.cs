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
    // Start is called before the first frame update
    void Start()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        characterController = gameObject.AddComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVec = moveAction.ReadValue<Vector2>();
        Debug.Log(moveVec);
        characterController.Move((Vector3)moveVec);

    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
}
