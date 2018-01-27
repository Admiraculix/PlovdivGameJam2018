using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBoltScript : MonoBehaviour {

    float randomTimeInterval;
    float randomSpinDegrees;
    Rigidbody2D objectRigidBody;

    private void Start()
    {
        objectRigidBody = GetComponent<Rigidbody2D>();
        randomTimeInterval = Random.Range(2.0f, 6.0f);
        randomSpinDegrees = Random.Range(0.0f, 360.0f);
        StartCoroutine(SpinAndWait());
    }

    IEnumerator SpinAndWait()
    {
        yield return new WaitForSecondsRealtime(randomTimeInterval);

        randomTimeInterval = Random.Range(2.0f, 6.0f);
        randomSpinDegrees = Random.Range(0.0f, 360.0f);
        StartCoroutine(SpinAndWait());
    }

    private void Update()
    {
        objectRigidBody.MoveRotation(randomSpinDegrees);
    }
}
