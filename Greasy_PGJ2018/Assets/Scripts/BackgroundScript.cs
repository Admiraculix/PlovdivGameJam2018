using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public AudioClip loopMusic;
    AudioSource audioSRX;

	void Start () {
        audioSRX = GetComponent<AudioSource>();
        audioSRX.clip = loopMusic;
        audioSRX.loop = true;
        audioSRX.Play();
    }
}
