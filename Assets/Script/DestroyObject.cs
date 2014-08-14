using UnityEngine;
using System.Collections;

// デバック用のScript
// コマンドでオブジェクトの全消去

public class DestroyObject : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.D)) 
		{
			DestroyObject(gameObject);
		}
	}
}
