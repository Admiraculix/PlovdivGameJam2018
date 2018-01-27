using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearScript : MonoBehaviour {

    public bool counterRotation;
    public float speed;
	
	void Update () {
        if (counterRotation)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime * -1.0f));
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
        }
    }
}
