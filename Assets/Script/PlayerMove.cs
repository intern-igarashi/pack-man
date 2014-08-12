using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour 
{
	private const float MOVE_SPEED = 0.1f; 

	Quaternion START_ROTATION = Quaternion.Euler (0, -90, 0);

	// Use this for initialization
	void Start () 
	{
		transform.rotation = START_ROTATION;
	}
	
	// Update is called once per frame
	void Update () 
	{
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

		transform.position += transform.forward*MOVE_SPEED;
	}
}

