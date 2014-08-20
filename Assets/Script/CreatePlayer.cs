using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePlayer : Photon.MonoBehaviour 
{
	GameObject gameManager;

	int type;

	enum PLAYER_TYPE
	{
		PACMAN = 1,
		RED_MONSTER,
		BLUE_MONSTER,
		GREEN_MONSTER,
		PINK_MONSTER
	};

	const string prefabPath = "Prefab/";

	string pacmanName = prefabPath + "pacman";
	string redMonsterName = prefabPath + "redMonster";
	string blueMonsterName = prefabPath + "blueMonster";
	string greenMonsterName = prefabPath + "greenMonster";
	string pinkMonsterName = prefabPath + "pinkMonster";

	Dictionary<int, string> playerType;
	
//	public void CreateRoomCharactor()
//	{
//		gameManager = GameObject.FindWithTag("GameManager");
//		type = PhotonNetwork.playerList.Length;
//		playerTypeInit ();
//		
//		// ホストならパックマンになる.
//		if (type == (int)PLAYER_TYPE.PACMAN)
//		{
//			PhotonNetwork.Instantiate (playerType[(int)PLAYER_TYPE.PACMAN], new Vector3(((float)type-2.5f)*3, 0f, 0f), Quaternion.identity, 0);
//			gameManager.GetComponent<GameManager>().SetControlPlayerType((int)PLAYER_TYPE.PACMAN);
//		}
//		else
//		{
//			PhotonNetwork.Instantiate (playerType[type], new Vector3(((float)type-2.5f) * 3, 0f, 0f), Quaternion.identity, 0);
//			gameManager.GetComponent<GameManager>().SetControlPlayerType(type);	
//		}
//	}

	public GameObject CreateRoomCharactor()
	{
		type = PhotonNetwork.playerList.Length;
		playerTypeInit ();
		
		// ホストならパックマンになる.
		if (type == (int)PLAYER_TYPE.PACMAN)
		{
			return PhotonNetwork.Instantiate (playerType[(int)PLAYER_TYPE.PACMAN], new Vector3(((float)type-2.5f)*3, 0f, 0f), Quaternion.identity, 0);
		}
		else
		{
			return PhotonNetwork.Instantiate (playerType[type], new Vector3(((float)type-2.5f) * 3, 0f, 0f), Quaternion.identity, 0);
		}
	}

	public void InitPlayerType(int charactorType)
	{
		playerTypeInit ();
		if (type == (int)PLAYER_TYPE.PACMAN)
		{
			PhotonNetwork.Instantiate (playerType[(int)PLAYER_TYPE.PACMAN], new Vector3(0f, -2f, 0f), Quaternion.identity, 0);
		}
		else
		{
			PhotonNetwork.Instantiate (playerType[charactorType], new Vector3(charactorType-3f, 0f, 0f), Quaternion.identity, 0);
		}
	}

	void playerTypeInit()
	{
		playerType = new Dictionary<int, string> ();
		playerType [(int)PLAYER_TYPE.PACMAN] = pacmanName;
		playerType [(int)PLAYER_TYPE.RED_MONSTER] = redMonsterName;
		playerType [(int)PLAYER_TYPE.BLUE_MONSTER] = blueMonsterName;
		playerType [(int)PLAYER_TYPE.GREEN_MONSTER] = greenMonsterName;
		playerType [(int)PLAYER_TYPE.PINK_MONSTER] = pinkMonsterName;
	}
}
