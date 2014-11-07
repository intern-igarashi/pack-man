using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreator : Photon.MonoBehaviour 
{
	Dictionary <int, string> characterPathDict;

	const string prefabDirectoryPath = "Prefab/";
	string[] characterPrefabFileNames = {
		"pacman", "redMonster", "blueMonster", "greenMonster", "pinkMonster"
	};

	public enum PLAYER_TYPE
	{
		PACMAN = 1,
		RED_MONSTER,
		BLUE_MONSTER,
		GREEN_MONSTER,
		PINK_MONSTER,
	};
	
	void InitCharacterType ()
	{
		characterPathDict = new Dictionary <int, string> ();
		const int init_charactor_num_max = characterPrefabFileNames.Length + PLAYER_TYPE.PACMAN;

		for ( int i = PLAYER_TYPE.PACMAN; i < init_charactor_num_max; i++ )
		{
			characterPathDict [i] = prefabDirectoryPath + characterPrefabFileNames [i];
		}
	}

	public void CreateNetworkCharacter (int characterType)
	{
		InitCharacterType ();
		Vector3 creationPos = Vector3.zero;

		if (characterType == PLAYER_TYPE.PACMAN)
		{
			creationPos = new Vector3 (0f, -2f, 0f);
		}
		else
		{
			creationPos = new Vector3 ((float)characterType - 3f, 0f, 0f);
		}
		PhotonNetwork.Instantiate (characterPathDict [characterType], creationPos, Quaternion.identity, 0);
	}

	public GameObject CreateRoomCharacter ()
	{
		InitCharacterType ();
		int characterType = PhotonNetwork.playerList.Length;

		Vector3 creationPos = new Vector3 (((float)characterType - 2.5f) * 3, 0f, 0f);
	
		return PhotonNetwork.Instantiate (characterPathDict [characterType], creationPos, Quaternion.identity, 0);
	}
}
