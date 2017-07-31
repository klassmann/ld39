using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//public static GameManager instance = null;

	AudioSource audioPlayer;
	public AudioClip Music;

	// Use this for initialization
	void Start () {
		audioPlayer = GetComponent<AudioSource>();
	}


	public void StartMusic()
	{
		audioPlayer.clip = Music;
		audioPlayer.Play();
	}

	public void StopMusic()
	{
		audioPlayer.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
