using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorController : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionAsset actions;

    private bool playerInRange = false;
    private InputAction interact;
    private DoorController elevatorDoor;
    private Animator anim;

    void Start()
    {
        interact = actions.FindActionMap("Player").FindAction("Interact");
        elevatorDoor = GetComponentInChildren<DoorController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && interact.ReadValue<float>() > 0)
        {
            elevatorDoor.CloseDoor();
            anim.SetBool("active", true);
        }
    }


    public void SetPlayerInRange(bool value)
    {
        this.playerInRange = value;
    }
}
