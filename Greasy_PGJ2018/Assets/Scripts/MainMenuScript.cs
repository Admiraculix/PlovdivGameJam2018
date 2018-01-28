using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public AudioClip loopMusic;
    AudioSource audioSRX;

    private void Start()
    {
        audioSRX = GetComponent<AudioSource>();
        audioSRX.clip = loopMusic;
        audioSRX.loop = true;
        audioSRX.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayDemo()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void PlayCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
