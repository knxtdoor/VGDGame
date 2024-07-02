using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonitorController : MonoBehaviour
{
    public InputActionAsset actions;
    public CanvasGroup popup;
    private InputAction interact;
    private bool playerInRange;
    private bool indicatorActive = true;
    void Start()
    {
        interact = actions.FindActionMap("Player").FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && interact.ReadValue<float>() > 0)
        {
            if (indicatorActive == true)
            {
                this.GetComponentInChildren<InteractableIndicator>().gameObject.SetActive(false);
                indicatorActive = false;
            }
            ShowPopup();
        }
        if (!playerInRange && popup.interactable)
        {
            HidePopop();
        }
    }


    public void SetPlayerInRange(bool state)
    {
        this.playerInRange = state;
    }
    public void HidePopop()
    {
        popup.alpha = 0f;
        popup.interactable = false;
        popup.blocksRaycasts = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowPopup()
    {
        popup.alpha = 1f;
        popup.interactable = true;
        popup.blocksRaycasts = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
