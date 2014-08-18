using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePlayer : Photon.MonoBehaviour 
{
	GameObject gameManager;

	int type;

	const string prefabPath = "Prefab/";

	string pacmanName = prefabPath + "pacman";
	string redMonsterName = prefabPath + "redMonster";
	string blueMonsterName = prefabPath + "blueMonster";
	string greenMonsterName = prefabPath + "greenMonster";
	string pinkMonsterName = prefabPath + "pinkMonster";

	Dictionary<int, string> playerType;
	
	public void CreateRoomCharactor()
	{
		gameManager = GameObject.FindWithTag("GameManager");
		type = PhotonNetwork.playerList.Length;
		playerTypeInit ();
		// ホストならパックマンになる
		if (type == 1)
		{
			PhotonNetwork.Instantiate (playerType[type-1], new Vector3(((float)type-2.5f)*3, 0f, 0f), Quaternion.identity, 0);
			gameManager.GetComponent<GameManager>().SetControlPlayerType(type-1);
		}
		else
		{
			PhotonNetwork.Instantiate (playerType[type], new Vector3(((float)type-2.5f)*3, 0f, 0f), Quaternion.identity, 0);
			gameManager.GetComponent<GameManager>().SetControlPlayerType(type);	
		}
	}

	public void CreateGameSceneCharactor(int charactorType)
	{
		playerTypeInit ();
		if (type == 1)
		{
			PhotonNetwork.Instantiate (playerType[charactorType-1], new Vector3(0f, -2f, 0f), Quaternion.identity, 0);
		}
		else
		{
			PhotonNetwork.Instantiate (playerType[charactorType], new Vector3(charactorType-3f, 0f, 0f), Quaternion.identity, 0);
		}
	}

	void playerTypeInit()
	{
		playerType = new Dictionary<int, string> ();
		playerType [0] = pacmanName;
		playerType [1] = redMonsterName;
		playerType [2] = blueMonsterName;
		playerType [3] = greenMonsterName;
		playerType [4] = pinkMonsterName;
	}
}
