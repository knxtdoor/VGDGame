using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorButtonController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject door;
    private DoorController doorController;
    public InputActionAsset actions;
    private InputAction interact;
    private bool indicatorActive = true;
    bool playerInRange;
    public PlayerController playerController;
    private bool animationPlayed = false;

    void Start()
    {
        interact = actions.FindActionMap("Player").FindAction("Interact");
        doorController = door.GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.ReadValue<float>() > 0 && playerInRange && !animationPlayed)
        {
            doorController.OpenDoor();
            if (indicatorActive)
            {
                this.GetComponentInChildren<InteractableIndicator>().gameObject.SetActive(false);
                indicatorActive = false;
            }
            playerController.InteractAnimation();
            animationPlayed = true;
        }

    }

    public void SetPlayerInRange(bool state)
    {
        this.playerInRange = state;
    }
}
