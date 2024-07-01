using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public InputActionAsset actions;

    private InputAction moveAction;

    private Rigidbody rb;
    public float playerMoveSpeed = .3f;

    public float baseSpeed = 3f;
    public float sprintSpeed = 4.5f;
    public float walkSpeed = 1.5f;

    private Animator animator;

    public float wallDetectionDistance = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {        
        Vector2 inputVec = moveAction.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        getSpeed();

        moveVec *= playerMoveSpeed;

        if (!IsHittingWall(moveVec))
        {
            rb.velocity = new Vector3(moveVec.x, rb.velocity.y, moveVec.z);
        }

        animator.SetFloat("Speed", new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude);

        if (inputVec != Vector2.zero)
        {
            Quaternion facing = Quaternion.LookRotation(new Vector3(moveVec.x, 0, moveVec.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, facing, Time.deltaTime * 10f);
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
            gameObject.SetActive(false);
        }
    }

    void OnAnimatorMove()
    {
        if (animator)
        {
            Vector3 newPosition = animator.rootPosition;
            newPosition.y = rb.position.y;
            rb.MovePosition(newPosition);
            rb.MoveRotation(animator.rootRotation);
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
}
