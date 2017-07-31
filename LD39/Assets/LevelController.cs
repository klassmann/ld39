using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour {

	AudioSource audioSource;

	public AudioClip musicClip;
	public AudioClip winClip;
	public AudioClip loseClip;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayMusic()
	{
		audioSource.clip = musicClip;
		audioSource.Play();
		audioSource.loop = true;
	}

	public void PlayWin()
	{
		audioSource.Stop();
		audioSource.clip = winClip;
		audioSource.Play();
		audioSource.loop = false;
	}

	public void PlayLose()
	{
		audioSource.Stop();
		audioSource.clip = loseClip;
		audioSource.Play();
		audioSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene("Level_1");
		}
		else if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
