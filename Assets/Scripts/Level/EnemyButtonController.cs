using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyButtonController : MonoBehaviour
{
    public GameObject enemy;
    private EnemyController enemyController;
    public InputActionAsset actions;
    private InputAction interact;
    public float time = 3f;
    private bool playerInRange;
    private bool indicatorActive = true;


    void Start()
    {
        // Find the action map and action for the pause
        interact = actions.FindActionMap("Player").FindAction("Interact");
        enemyController = enemy.GetComponent<EnemyController>();
        // pauseAction.Enable();

        // Register the callback for the action
        // pauseAction.performed += ctx => OnPauseButtonPressed();
    }

    void Update()
    {
        Debug.Log("check set up.");
        if (interact.ReadValue<float>() > 0 && playerInRange)
        {
            if (indicatorActive)
            {
                this.GetComponentInChildren<InteractableIndicator>().gameObject.SetActive(false);
                indicatorActive = false;
            }
            Debug.Log("successfully set up.");
            enemyController.PauseForSeconds(time);
        }

    }
    public void SetPlayerInRange(bool state)
    {
        this.playerInRange = state;
    }

    // void OnDestroy()
    // {
    //     // Unregister the callback when the object is destroyed to avoid memory leaks
    //     pauseAction.performed -= ctx => OnPauseButtonPressed();
    // }
}
