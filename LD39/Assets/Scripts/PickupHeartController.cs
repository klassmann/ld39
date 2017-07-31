using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHeartController : MonoBehaviour {


	AudioSource audio;
	SpriteRenderer renderer;
	bool collected = false;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		renderer = GetComponent<SpriteRenderer>();
	}

	void Pickup(GameObject go) {
		if (!collected){
			audio.Play();
			renderer.enabled = false;
			go.SendMessage("RestoreHearts");
			collected = true;
		}
	}
}
