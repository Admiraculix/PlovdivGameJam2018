using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float speed;
	public GameObject targetToFollow;
    public float offsetY;

	void FixedUpdate () {
		transform.position = Vector2.Lerp(transform.position, targetToFollow.transform.position, speed);
		transform.position = new Vector3 (transform.position.x, transform.position.y - offsetY, -10.0f);
	}
}
