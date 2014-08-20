using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : Photon.MonoBehaviour
{
	const float MOVE_SPEED = 1.5f;
	const float X_MAX = 9.5f;
	const float X_MIN = -9.5f;

	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);
	
	// 1フレーム前のポジションを記憶.
	Vector3 beforeUpdatePosition;
	Vector3 velocity;

	//
	bool isHitWall;
	bool isInit = false;
	
	GameManager gameManager;

	PhotonView myPhotonView;
	
	Dictionary<int, Vector3> initPosition;
	
//	void PositionInit()
//	{
//		Vector3[] Pos = {
//			new Vector3(0f, -2f, 0f), new Vector3(0f, 0f, 0f),
//			new Vector3(-1f, 0f, 0f), new Vector3(0f, 0f, 0f),
//			new Vector3(1f, 0f, 0f)
//		};
//		for (int itr = 0; itr < Pos.Length; itr++)
//		{
//			initPosition[itr] = Pos[itr];
//		}
//	}
	
	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
		beforeUpdatePosition = transform.position;
		isHitWall = false;
		collider.isTrigger = true;
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager>();
		initPosition = new Dictionary<int, Vector3> ();
//		PositionInit ();
		myPhotonView = PhotonView.Get (this);
		if (!myPhotonView.isMine)
		{
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameManager.isPlayGame)
		{
			if (!isInit)
			{
				transform.position = new Vector3(0f, -2f, 0f);
				isInit = true;
			}
			float speed = MOVE_SPEED;

//			Vector3 pos = transform.position;
//			beforeUpdatePosition = new Vector3((int)pos.x, (int)pos.y, 0f);

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
		if (hit.transform.tag == "wall")
		{
			isHitWall = true;

			int revisX = Mathf.RoundToInt(beforeUpdatePosition.x);
			int revisY = Mathf.RoundToInt(beforeUpdatePosition.y);
			
			transform.position = new Vector3((float)revisX, (float)revisY, 0f);

//			transform.position = beforeUpdatePosition;
		}
		else if (hit.transform.tag == "enemy")
		{
			PhotonView gameManagerPV = gameManager.GetComponent<PhotonView>();
			gameManagerPV.RPC("EndGame", PhotonTargets.All);
		}
	}
	
	void OnTriggerStay(Collider hit)
	{
//		PhotonView hitPhotonView;
//		hitPhotonView = hit.GetComponent<PhotonView>();
//		Debug.Log (hitPhotonView);
		if (hit.transform.tag == "wall")
		{
			isHitWall = true;

			int revisX = Mathf.RoundToInt(beforeUpdatePosition.x);
			int revisY = Mathf.RoundToInt(beforeUpdatePosition.y);

			transform.position = new Vector3((float)revisX, (float)revisY, 0f);
		}
		else if (hit.transform.tag == "dot") 
		{
			hit.gameObject.GetComponent<EatDot>().Eaten();
		}
	}
}
