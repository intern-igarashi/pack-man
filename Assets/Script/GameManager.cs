using UnityEngine;
using System.Collections;

public class GameManager : Photon.MonoBehaviour
{
	int stageLevel = 0;
	int playerCount = 0;

	public bool isPlayGame = false;

	public int controlPlayerType;

	NetworkManager networkManager;

	// Use this for initialization
	void Awake () 
	{
		stageLevel = 0;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (gameObject);
		networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		dots = GameObject.FindGameObjectsWithTag("dot");
//		if (dots.Length <= 0) 
//		{
//			Debug.Log("clear");
//		}
	}

	void OnGUI()
	{
		if (!networkManager.isJoinedRoom)
		{
			return;
		}
		if (!isPlayGame && PhotonNetwork.isMasterClient)
		{
			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2 + 50, 100, 50), "Start Game!"))
			{
				PhotonNetwork.Instantiate("Prefab/StageCreator", Vector3.zero, Quaternion.identity, 0);
				isPlayGame = true;

			}
		}
	}

	public void SpawnNewCharactor(int playerType, GameObject target)
	{
		PhotonNetwork.Instantiate ("Prefab/BlueMonster", Vector3.zero, Quaternion.identity, 0);
		PhotonNetwork.Destroy(target);
	}

	public void SetControlPlayerType(int type){ controlPlayerType = type; }

	public int GetControlPlayerType(){ return controlPlayerType;}

	public int GetSelectLevel() { return stageLevel; }
}
