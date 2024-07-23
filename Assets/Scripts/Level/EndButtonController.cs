using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndButtonController : MonoBehaviour
{
    public InputActionAsset actions;
    public CanvasGroup cg;
    private InputAction interact;
    private bool playerInRange;
    private bool indicatorActive = true;

    private bool activated = false;
    private bool finishedActivating = false;
    private float timeSinceActivated;
    private float fadeInTime = 2f; void Start()
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
            activated = true;
        }
        if (finishedActivating)
        {
            return;
        }
        if (activated)
        {

            timeSinceActivated += Time.deltaTime;
            if (timeSinceActivated < fadeInTime)
            {
                cg.alpha = timeSinceActivated / fadeInTime;
            }
            else
            {
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
                Time.timeScale = 0f;
                finishedActivating = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void SetPlayerInRange(bool state)
    {
        this.playerInRange = state;
    }




}
