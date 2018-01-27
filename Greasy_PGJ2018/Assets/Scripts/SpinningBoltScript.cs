using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBoltScript : MonoBehaviour {

    float randomTimeInterval;
    float randomSpinDegrees;
    float randomSpeed;

    private void Start()
    {
        randomTimeInterval = Random.Range(2.0f, 5.0f);
        randomSpinDegrees = Random.Range(0.0f, 360.0f);
        randomSpeed = Random.Range(30.0f, 300.0f); 
        StartCoroutine(SpinAndWait());
    }

    IEnumerator SpinAndWait()
    {
        yield return new WaitForSecondsRealtime(randomTimeInterval);

        randomTimeInterval = Random.Range(2.0f, 5.0f);
        randomSpinDegrees = Random.Range(0.0f, 360.0f);
        randomSpeed = Random.Range(30.0f, 300.0f);
        StartCoroutine(SpinAndWait());
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0.0f, 0.0f, randomSpinDegrees)), randomSpeed * Time.deltaTime);
    }
}
