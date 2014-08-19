using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : Photon.MonoBehaviour
{
	const float MOVE_SPEED = 1.5f;
	const float X_MAX = 9.5f;
	const float X_MIN = -9.5f;
	
	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);
	
	// 1フレーム前のポジションを記憶
	public Vector3 beforeUpdatePosition;
	
	//
	bool isHitWall;
	bool isInit = false;
	bool isPlayGame = false;
	
	GameManager gameManager;
	
	Dictionary<int, Vector3> initPosition;
	
	void PositionInit()
	{
		Vector3[] Pos = {
			new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f),
			new Vector3(-1f, 0f, 0f), new Vector3(0f, 0f, 0f),
			new Vector3(1f, 0f, 0f)
		};
		for (int itr = 0; itr < Pos.Length; itr++)
		{
			initPosition[itr] = Pos[itr];
		}
	}

	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
		beforeUpdatePosition = transform.position;
		isHitWall = false;
		collider.isTrigger = true;
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager>();
		initPosition = new Dictionary<int, Vector3> ();
		PositionInit ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPlayGame)
		{
			if (!isInit)
			{
				transform.position = initPosition[gameManager.GetControlPlayerType()];
				isInit = true;
			}
			float speed = MOVE_SPEED;
			beforeUpdatePosition = transform.position;
			
			
			if (photonView.isMine)
			{
				GetInputKey();
			}
			
			if (isHitWall)
			{
				speed = 0;
			}
			
			transform.position += transform.forward * speed * Time.deltaTime;
			
			WarpPosition();
		}
	}

	void GetInputKey()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			isHitWall = false;
			transform.rotation = START_ROTATION*Quaternion.Euler (-90, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			isHitWall = false;
			transform.rotation = START_ROTATION*Quaternion.Euler (90, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			isHitWall = false;
			transform.rotation = START_ROTATION*Quaternion.Euler (180, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			isHitWall = false;
			transform.rotation = START_ROTATION*Quaternion.Euler (0, 0 , 0);
		}
	}
	
	void WarpPosition()
	{
		if (transform.position.x < X_MIN) 
		{
			transform.position = new Vector3(X_MAX, transform.position.y, 0);
		}
		else if (transform.position.x > X_MAX) 
		{
			transform.position = new Vector3(X_MIN, transform.position.y, 0);
		}
	}

	void OnTriggerEnter(Collider hit)
	{
		//Debug.Log ("hit");
		if (hit.transform.tag == "wall")
		{
			isHitWall = true;
			transform.position = beforeUpdatePosition;
		}
	}

	[RPC]
	void StartGame()
	{
		isPlayGame = true;
	}
}
