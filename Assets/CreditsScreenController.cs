using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreenController : MonoBehaviour
{
    // Start is called before the first frame update


    public CanvasGroup titleScreen;
    public CanvasGroup creditsScreen;

    public void OpenCredits()
    {
        titleScreen.alpha = 0;
        titleScreen.interactable = false;
        titleScreen.blocksRaycasts = false;

        creditsScreen.alpha = 1f;
        creditsScreen.interactable = true;
        creditsScreen.blocksRaycasts = true;
    }

    public void CloseCredits()
    {
        titleScreen.alpha = 1f;
        titleScreen.interactable = true;
        titleScreen.blocksRaycasts = true;

        creditsScreen.alpha = 0f;
        creditsScreen.interactable = false;
        creditsScreen.blocksRaycasts = false;
    }

}
