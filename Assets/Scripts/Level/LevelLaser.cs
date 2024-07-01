using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelLaser : MonoBehaviour
{

    private LineRenderer lr;
    public enum LaserDirection { //used for collision raycast check, can remove if rotation is used to find direction in world coords
    posZ, negZ, posX, negX, posY, negY
    }
    public LaserDirection direction;
    public float maxLength = 50f;
    public LayerMask collisionLayers; // Layers to check for collisions
    public float timer = 0f; //laser switches between on/off every x secs ; 0 is static
    public bool active = true;
    private bool coroutineStarted = false; //coroutine for timer
    public GameObject enemy;
    private AudioSource alarmSound;

    void Start() {
        lr = GetComponent<LineRenderer>();
        alarmSound = GetComponent<AudioSource>();

        //Check if timed or not
        if (timer != 0) {
            StartCoroutine(ShootLaserAfterDelay());
        }
    }

    void FixedUpdate() {
        ShootLaser();
    }

    void ShootLaser() {
        //turn off laser when not active
        if(!active) {
            lr.SetPosition(1, new Vector3(0, 0, 0));
            return;
        }
        //get laser direction and calculate base endpoint
        Vector3 directionVec = GetDirectionVector(direction);
        Vector3 end = Vector3.left * maxLength;

        RaycastHit hit; 
        if (Physics.Raycast(transform.position, directionVec, out hit, maxLength)) {
            if (hit.distance * 2 <= maxLength) {
                //get laser end point
                end = Vector3.left * hit.distance * 2;
                
                //Check if object is player
                if (hit.collider.name == "Player") {
                    if (enemy != null) {
                            EnemyController setPointFunc = enemy.GetComponent<EnemyController>();
                            setPointFunc.Patrol(transform.position + directionVec *hit.distance);
                        }
                    if (!alarmSound.isPlaying) {
                        alarmSound.Play();
                    }
                    //}
                }
            }
        }

        lr.SetPosition(1, end);
    }

    //Handle timed laser
    IEnumerator ShootLaserAfterDelay()
    {
        while(true) {
            yield return new WaitForSeconds(timer);
            active = !active;

            //End if laser timer gets set to 0
            if (timer == 0) {
                break;
            }
        }
    }

    //Convert direction var to a vector
    Vector3 GetDirectionVector(LaserDirection direction)
    {
        switch (direction){
            case LaserDirection.posZ:
                return Vector3.forward;

            case LaserDirection.negZ:
                return Vector3.back;

            case LaserDirection.posX:
                return Vector3.right;

            case LaserDirection.negX:
                return Vector3.left;

            case LaserDirection.posY:
                return Vector3.up;

            case LaserDirection.negY:
                return Vector3.down;

            default:
                return Vector3.forward;
        }
    }
}
