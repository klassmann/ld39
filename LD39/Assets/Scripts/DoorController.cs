using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoorController : MonoBehaviour {

	bool isOpened = false;
	bool IsOpened {
		get {
			return isOpened;
		}
		set {
			isOpened = value;
			updateDoorState();
		}
	}

	public Sprite DoorOpened;
	public Sprite DoorLocked;
	SpriteRenderer Renderer;
	AudioSource audio;

	void updateDoorState()
	{
		if (isOpened) {
			Renderer.sprite = DoorOpened;
		} else {
			Renderer.sprite = DoorLocked;
		}
	}

	void Start() {
		Renderer = GetComponent<SpriteRenderer>();
		audio = GetComponent<AudioSource>();
	}

	void Open() {
		IsOpened = true;
		audio.Play();
	}

	void Close() {
		IsOpened = false;
	}

}
