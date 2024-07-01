using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraDetection : MonoBehaviour
{
    public float detectionRange = 0f;
    public LayerMask detectionLayers;
    public Transform player; //Note - this is required and doesn't saved in the prefab, gotta be added to each instance individually
    public GameObject enemy;
    public float baseIntensity = 7f;
    public float redIntensity = 15f;

    private Light spotlight;

    void Start() {
        spotlight = GetComponent<Light>();
        detectionRange = spotlight.range;
    }

    void Update() {
        //only check for player if there is a player
        if (player != null) {
            DetectPlayer();
        }
        else {
            Debug.Log("please attach a player transform to the spotlight");
        }
    }

    void DetectPlayer(){

        //Get direction, angle, and distance from camera to the player.
        Vector3 directionToPlayer = player.position - transform.position;
        float distToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        //Check if the player is under spotlight
        if (distToPlayer <= detectionRange && angleToPlayer <= spotlight.spotAngle / 2) {
                //Check if camera can actually see player (could be behind a wall or something)
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, detectionLayers)) {
                    if (hit.collider.name == "Player") {
                        spotlight.color = Color.red;
                        spotlight.intensity = redIntensity;
                        if (enemy != null) {
                            EnemyController setPointFunc = enemy.GetComponent<EnemyController>();
                            setPointFunc.Patrol(player.position);
                        }
                    }
                    else {
                        spotlight.color = Color.white;
                        spotlight.intensity = baseIntensity;
                    }
                }
        }
        else {
            spotlight.color = Color.white;
            spotlight.intensity = baseIntensity;
        }
    }
}