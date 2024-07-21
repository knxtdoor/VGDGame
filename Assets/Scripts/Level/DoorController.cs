using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public bool isOpen;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOpen", isOpen);
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
