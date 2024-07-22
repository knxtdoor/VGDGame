using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        float newY = pos.y + (.0002f * Mathf.Sin(1.5f * Time.time));
        this.transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
