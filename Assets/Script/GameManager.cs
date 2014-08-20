﻿using UnityEngine;
using System.Collections;

public class GameManager : Photon.MonoBehaviour
{
	int stageLevel;

	public bool isPlayGame = false;
	public int controlPlayerType;
	
	bool isEndGame = false;
	bool isPlayerWin = false;

	int getDotCount = 0;
	int MaxDotCount = 0;
	int targetDotCount = 0;

	float afterStartGameTime = 0f;

	NetworkManager networkManager;

	PhotonView myPhotonView;

	// Use this for initialization
	void Awake () 
	{
		stageLevel = (int)UnityEngine.Random.Range(0f, 1.9f);
		controlPlayerType = PhotonNetwork.playerList.Length;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (gameObject);
		networkManager = GameObject.Find ("NetworkManager").GetComponent<NetworkManager> ();
		myPhotonView = this.gameObject.GetComponent<PhotonView> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPlayGame)
		{
			if (afterStartGameTime < 0.1f)
			{
				myPhotonView.RPC("GetDotCount", PhotonTargets.All);
			}
			else if (getDotCount >= targetDotCount)
			{
				myPhotonView.RPC("PlayerWin", PhotonTargets.All);
				myPhotonView.RPC("EndGame", PhotonTargets.All);
			}
			afterStartGameTime += Time.deltaTime;
		}
		else
		{
			afterStartGameTime = 0f;
		}
	}

	void OnGUI()
	{
		GUIStyle font = new GUIStyle();
		
		if (!networkManager.isJoinedRoom)
		{
			return;
		}
		if (!isPlayGame && PhotonNetwork.isMasterClient)
		{
			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2 + 200, 100, 50), "Start Game!"))
			{
				PhotonNetwork.Instantiate("Prefab/StageCreator", Vector3.zero, Quaternion.identity, 0);
				myPhotonView.RPC("StartGame", PhotonTargets.All);
			}
		}

		if (isPlayGame)
		{
			font.fontSize = 20;
			GUI.TextArea(new Rect(0, Screen.height/2-170, 100, 20), "GET DOTS", font);
			GUI.TextArea(new Rect(0, Screen.height/2-150, 100, 20), getDotCount.ToString() + " / " + targetDotCount.ToString(), font);
		}

		if (isEndGame)
		{
			isPlayGame = false;
			string resultMessage = null;
			font.fontSize = 120;
			if (isPlayerWin)
			{
				if (PhotonNetwork.isMasterClient)
				{
					font.normal.textColor = Color.red;
					resultMessage = "WIN!";
				}
				else
				{
					font.normal.textColor = Color.blue;
					resultMessage = "LOSS";
				}
			}
			else
			{
				if (PhotonNetwork.isMasterClient)
				{
					font.normal.textColor = Color.blue;
					resultMessage = "LOSS";
				}
				else
				{
					font.normal.textColor = Color.red;
					resultMessage = "WIN!";
				}
			}
			GUI.TextArea(new Rect(Screen.width/2-100, Screen.height/2, 200, 50), resultMessage, font);
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

	[RPC]
	void StartGame()
	{
		isPlayGame = true;

	}

	[RPC]
	void GetDotCount()
	{
		GameObject[] dots = GameObject.FindGameObjectsWithTag ("dot");
		getDotCount = -1;
		MaxDotCount = dots.Length;
		if (MaxDotCount > 0)
		{
			targetDotCount = MaxDotCount / 3;
			getDotCount = 0;
		}
//		Debug.Log (getDotCount);
	}

	[RPC]
	void EndGame()
	{
		isEndGame = true;
	}

	[RPC]
	void IncreaseDotCount()
	{
		getDotCount++;
//		Debug.Log (dotCount);
	}

	[RPC]
	void PlayerWin()
	{
		isPlayerWin = true;
	}
}
