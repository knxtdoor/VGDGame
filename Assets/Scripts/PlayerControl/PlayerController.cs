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

    private InputAction doHologram;

    private CharacterController characterController;
    private Rigidbody rb;
    public float playerMoveSpeed = .3f;

    public float baseSpeed = 3f;
    public float sprintSpeed = 4.5f;
    public float walkSpeed = 1.5f;

    public GameObject holoPrefab;
    public GameObject cameraObj;
    private HologramController activeHolo;

    // Start is called before the first frame update
    void Start()
    {
        moveAction = actions.FindActionMap("Player").FindAction("Move");
        doHologram = actions.FindActionMap("Player").FindAction("Ability1");
        characterController = gameObject.AddComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();

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

        if (activeHolo == null && doHologram.ReadValue<float>() > 0)
        {
            GameObject newHologram = Instantiate(holoPrefab);
            newHologram.gameObject.transform.position = this.transform.position + (this.transform.forward * 2);
            newHologram.gameObject.transform.rotation = this.transform.rotation;
            activeHolo = newHologram.GetComponent<HologramController>();
            activeHolo.player = this;
            Vector3 holoDest = this.transform.position + (this.transform.forward * 10);
            activeHolo.DispatchHologram(holoDest);
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMoveSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            playerMoveSpeed = walkSpeed;
        }
        else
        {
            playerMoveSpeed = baseSpeed;
        }
    }
}
