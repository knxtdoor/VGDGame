using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup cg;
    private bool activated;
    private bool finishedActivating = false;
    private float timeSinceActivated;
    private float fadeInTime = 2f;
    void Start()
    {
        cg = GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }
    }



    public void Trigger()
    {
        this.activated = true;
        Cursor.lockState = CursorLockMode.None;

    }
}
