using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 1f;
    public Transform enemy;
    public Transform player;

    private Vector3 pointA = new Vector3(0, 1, 15); //location to move from
    private Vector3 pointB = new Vector3(10, 1, 15); //location to move towrds
    private Vector3 moveTo;
    
    // Start is called before the first frame update
    void Start(){
        moveTo = pointB;
    }

    // Update is called once per frame
    void Update(){
        //Move
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        
        //check if player is near
        if (Vector3.Distance(enemy.position, player.position) < 5) {
            print("located");
        }

        // start movng backwards when at a point
        if (transform.position == moveTo) { 
            
            if (moveTo == pointB){
                moveTo = pointA; // Move towards the start position
            }
            else {
                moveTo = pointB;   // Move towards the end position
            }
        }
    }
}
