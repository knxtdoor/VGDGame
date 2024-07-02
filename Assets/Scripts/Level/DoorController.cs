using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool isOpen;
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetDoor(bool open)
    {
        anim.SetBool("isOpen", open);
        this.isOpen = open;
    }

    public void OpenDoor()
    {
        SetDoor(true);
    }
    public void CloseDoor()
    {
        SetDoor(false);
    }
    public void ToggleDoor()
    {
        SetDoor(!isOpen);
    }

}
