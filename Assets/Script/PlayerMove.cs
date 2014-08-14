using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	//
	const float MOVE_SPEED = 3.0f;
	const float X_MAX = 9.5f;
	const float X_MIN = -9.5f;

	//
	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);

	// 1フレーム前のポジションを記憶
	public Vector3 beforeUpdatePosition;

	//
	bool isHitWall;

	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
		beforeUpdatePosition = transform.position;
		isHitWall = false;
		collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float speed = MOVE_SPEED;
		beforeUpdatePosition = transform.position;

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

		if (isHitWall)
		{
			speed = 0;
		}

		transform.position += transform.forward*speed*Time.deltaTime;

		if (transform.position.x < X_MIN) 
		{
			transform.position = new Vector3(X_MAX, transform.position.y, 0);
		}
		else if (transform.position.x > X_MAX) 
		{
			transform.position = new Vector3(X_MIN, transform.position.y, 0);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.tag == "wall")
		{
			isHitWall = true;
			transform.position = beforeUpdatePosition;
		}
	}

	void OnTriggerStay(Collider hit)
	{
		if (collider.transform.tag == "wall")
		{
			isHitWall = true;
			transform.position = beforeUpdatePosition;
		}
		else if (hit.transform.tag == "dot") 
		{
			DestroyObject(hit.gameObject);
		}
	}
}

