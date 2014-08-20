using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : Photon.MonoBehaviour
{
	const float MOVE_SPEED = 1.5f;
	const float X_MAX = 9.5f;
	const float X_MIN = -9.5f;

	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);
	Quaternion beforeRotation;
	
	// 1フレーム前のポジションを記憶.
	Vector3 beforeUpdatePosition;

	//
	bool isHitWall;
	bool isInit = false;
	bool isPowerUp = false;
	
	GameManager gameManager;

	PhotonView myPhotonView;


	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
		beforeUpdatePosition = transform.position;
		isHitWall = false;
		collider.isTrigger = true;
		gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager>();
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

			beforeUpdatePosition = transform.position;
			beforeRotation = transform.rotation;

			if (photonView.isMine)
			{
				GetInputKey();
			}


			if (Physics.Raycast(transform.position, Vector3.forward, 1f))
			{
				isHitWall = true;
				Debug.Log ("hit");
			}
			if (Physics.Raycast(transform.position - Vector3.left, Vector3.forward, 1f))
			{
				isHitWall = true;
				Debug.Log ("hit");	
			}
			if (Physics.Raycast(transform.position - Vector3.right, Vector3.forward, 1f))
			{
				isHitWall = true;
				Debug.Log ("hit");
			}
			
			if (isHitWall)
			{
				transform.rotation = beforeRotation;
			}

			transform.position += transform.forward * speed * Time.deltaTime;

			WarpPosition();
		}
	}
	
	void GetInputKey()
	{
		isHitWall = false;

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			transform.rotation = START_ROTATION*Quaternion.Euler (-90, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			transform.rotation = START_ROTATION*Quaternion.Euler (90, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			transform.rotation = START_ROTATION*Quaternion.Euler (180, 0 , 0);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
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

			int offsetX = Mathf.RoundToInt(beforeUpdatePosition.x);
			int offsetY = Mathf.RoundToInt(beforeUpdatePosition.y);
			float revisX = beforeUpdatePosition.x;
			float revisY = beforeUpdatePosition.y;

			if (Mathf.Abs(revisX - offsetX) < 0.1f)
			{
				revisX = Mathf.Round(beforeUpdatePosition.x);
			}
			if (Mathf.Abs(revisY - offsetY) < 0.1f)
			{
				revisY = Mathf.Round(beforeUpdatePosition.y);
			}

			transform.position = new Vector3(revisX, revisY, 0f);

//			transform.position = beforeUpdatePosition;
		}
		else if (hit.transform.tag == "enemy" && gameManager.isPlayGame)
		{
			if (!isPowerUp)
			{
				PhotonView gameManagerPV = gameManager.GetComponent<PhotonView>();
				gameManagerPV.RPC("EndGame", PhotonTargets.All);
			}
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

			int offsetX = Mathf.RoundToInt(beforeUpdatePosition.x);
			int offsetY = Mathf.RoundToInt(beforeUpdatePosition.y);
			float revisX = beforeUpdatePosition.x;
			float revisY = beforeUpdatePosition.y;
			
			if (Mathf.Abs(revisX - offsetX) < 0.2f)
			{
				revisX = Mathf.Round(beforeUpdatePosition.x);
			}
			if (Mathf.Abs(revisY - offsetY) < 0.2f)
			{
				revisY = Mathf.Round(beforeUpdatePosition.y);
			}

			transform.position = new Vector3(revisX, revisY, 0f);
		}
		else if (hit.transform.tag == "dot") 
		{
			hit.gameObject.GetComponent<EatDot>().Eaten();
		}
		else if (hit.transform.tag == "powerDot")
		{
			hit.gameObject.GetComponent<EatDot>().Eaten();
			myPhotonView.RPC("PowerUp", PhotonTargets.All);
		}
	}

	[RPC]
	void PowerUp()
	{
		isPowerUp = true;
	}
}
