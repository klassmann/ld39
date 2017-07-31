using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public enum CharDirection {
		Up,
		Down,
		Left,
		Right
	}


	private int _life = 4;
	private bool _gotKey = false;


	public int Life 
	{
		get
		{
			return _life;
		}
		set
		{
			_life = value;
			UpdateHearts();
		}
	}

	public bool GotKey
	{
		get
		{
			return _gotKey;
		}
		set
		{
			_gotKey = value;
			UpdateKey();
		}
	}

	private CharDirection Direction = CharDirection.Right;

	private Vector3 moveRight;
	private Vector3 moveLeft;
	private Vector3 moveUp;
	private Vector3 moveDown;

	public GameObject[] Hearts;
	public GameObject UI_Key;

	public Sprite HeartFull;
	public Sprite HeartEmpty;
	public Sprite KeyEmpty;
	public Sprite KeyGot;

	AudioSource audioWalk;
	AudioClip clipWalking;
	AudioClip clipDeath;


	// Use this for initialization
	void Start () {
		moveRight = Vector3.right / 2f;
		moveLeft = Vector3.left / 2f;
		moveDown = Vector3.down / 2f;
		moveUp = Vector3.up / 2f;
		audioWalk = GetComponent<AudioSource>();
	}

	public void RestoreHearts()
	{
		Life = 4;
	}

	public void GetKey()
	{
		GotKey = true;
	}

	private void UpdateHearts()
	{
		for(var i = 0; i < Hearts.Length; ++i)
		{
			var image = Hearts[i].GetComponent<Image>();
			var currentLife = Life - 1;

			if (currentLife >= i)
			{
				image.sprite = HeartFull;
			}
			else
			{
				image.sprite = HeartEmpty;
			}
		}
	}

	private void UpdateKey()
	{
		var image = UI_Key.GetComponent<Image>();
		if (GotKey)
		{
			image.sprite = KeyGot;
		} 
		else 
		{
			image.sprite = KeyEmpty;
		}
	}

	private void Move(CharDirection direction)
	{
		Life -= 1;
		audioWalk.Play();
		// if (Direction != direction)
		Vector3 originalPos = transform.position;

		if (direction == CharDirection.Up)
			transform.position += moveUp;

		if (direction == CharDirection.Down)
			transform.position += moveDown;

		if (direction == CharDirection.Left)
			transform.position += moveLeft;

		if (direction == CharDirection.Right)
			transform.position += moveRight;

		Direction = direction;

		Vector3 moveDirection = gameObject.transform.position - originalPos;
		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		UpdateHearts();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(CharDirection.Right);
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(CharDirection.Left);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Move(CharDirection.Up);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Move(CharDirection.Down);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "heart") {
			coll.gameObject.SendMessage("Pickup", gameObject);
			DestroyObject(coll.gameObject, 2.0f);
		} else if (coll.gameObject.tag == "key") {
			coll.gameObject.SendMessage("Pickup", gameObject);
			DestroyObject(coll.gameObject, 2.0f);
		} else if (coll.gameObject.tag == "door") {
			if (GotKey) {
				coll.gameObject.SendMessage("Open");
			}
		}
	}

}
