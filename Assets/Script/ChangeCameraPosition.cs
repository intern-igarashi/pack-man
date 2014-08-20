using UnityEngine;
using System.Collections;

public class ChangeCameraPosition : Photon.MonoBehaviour
{
	GameManager gameManager;
	NetworkManager networkManager;

	// Use this for initialization
	void Start () 
	{
		//GameObject go = PhotonNetwork.Instantiate ("Prefab/GameManager", Vector3.zero, Quaternion.identity, 0);
		//gameManager = go.GetComponent<GameManager> ();
		networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (networkManager.isJoinedRoom)
		{
			if (gameManager == null)
			{
				gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager>();
			}
			if (gameManager.isPlayGame)
			{
				transform.position = new Vector3(0f, 0f, -20f);
			}
		}
		else
		{
			transform.position = new Vector3(0f, 0f, -10f);
		}
	}
}
