using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

    public bool falls = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falls)
        {
            Rigidbody2D tempRigidBody = transform.parent.gameObject.AddComponent<Rigidbody2D>();
            tempRigidBody.gravityScale = 1;
            transform.parent.gameObject.tag = "Floor";
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            falls = false;
        }
    }
}
