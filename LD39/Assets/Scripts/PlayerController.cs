using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public enum PlayerState
	{
		New,
		Win,
		Lose
	}


	public enum CharDirection {
		Up,
		Down,
		Left,
		Right
	}


	private PlayerState State = PlayerState.New;
	private int _life = 5;
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

	//private CharDirection Direction = CharDirection.Right;

	private Vector3 moveRight;
	private Vector3 moveLeft;
	private Vector3 moveUp;
	private Vector3 moveDown;

	public GameObject LevelController;

	public GameObject[] UI_Hearts;
	public GameObject UI_Key;

	public Sprite HeartFull;
	public Sprite HeartEmpty;
	public Sprite KeyEmpty;
	public Sprite KeyGot;

	AudioSource audioWalk;
	AudioClip clipWalking;
	AudioClip clipDeath;

	public int Pos_X = 4;
	public int Pos_Y = 4;
	public int Max_X = 8;
	public int Max_Y = 10;


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
		Life = 5;
	}

	public void GetKey()
	{
		GotKey = true;
	}

	void Win()
	{
		Debug.Log("Win");
		if (LevelController != null)
			LevelController.SendMessage("PlayWin");
		State = PlayerState.Win;
	}

	void Lose()
	{
		Debug.Log("Losing");
		if (LevelController != null)
			LevelController.SendMessage("PlayLose");
		State = PlayerState.Lose;
	}

	private void UpdateHearts()
	{
		for(var i = 0; i < UI_Hearts.Length; ++i)
		{
			var image = UI_Hearts[i].GetComponent<Image>();
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

		if (Life < 0)
		{
			Lose();
			return;
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

		if (State != PlayerState.New)
			return;

		// if (Direction != direction)
		Vector3 originalPos = transform.position;

		if (direction == CharDirection.Up && Pos_Y == 2)
			return;

		if (direction == CharDirection.Down && Pos_Y == Max_Y - 1)
			return;

		if (direction == CharDirection.Left && Pos_X == 2)
			return;

		if (direction == CharDirection.Right && Pos_X == Max_X - 1)
			return;


		if (direction == CharDirection.Up)
		{
			transform.position += moveUp;
			Pos_Y -= 1;
		}

		if (direction == CharDirection.Down)
		{
			transform.position += moveDown;
			Pos_Y += 1;
		}

		if (direction == CharDirection.Left)
		{
			transform.position += moveLeft;
			Pos_X -= 1;
		}

		if (direction == CharDirection.Right)
		{
			transform.position += moveRight;
			Pos_X += 1;
		}
		
		Life -= 1;
		audioWalk.Play();

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
				Win();
			}
		}
	}

}
