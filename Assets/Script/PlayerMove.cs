using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	private const float MOVE_SPEED = 1.5f; 

	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);

	// 1フレーム前のポジションを記憶
	public Vector3 beforeUpdatePosition;

	//
	bool isHitWall;

	RaycastHit ray;

	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
		beforeUpdatePosition = transform.position;
		isHitWall = false;
		collider.isTrigger = true;
		ray = new RaycastHit ();
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

		if (Physics.Raycast (transform.position, Vector3.forward, out ray, 2.0f))
		{
			Debug.Log ("hit");
			float hitDistance = ray.distance;
			Vector3 hitPosition = ray.point;
			transform.position = hitPosition + Vector3.back*hitDistance;
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
		else if (hit.transform.tag == "pathway") 
		{
			DestroyObject(hit.gameObject);
		}
	}
}

