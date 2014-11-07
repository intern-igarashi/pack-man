/// <summary>
/// Create Network Charactor.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePlayer : Photon.MonoBehaviour 
{
	
	int createType;
	const int pacman_id = 1;
	Dictionary<int, string> createCharactorType;
	
	const int init_charactor_num_max = 5 + pacman_id;

	// file path
	const string prefab_path = "Prefab/";
	sring[] createCharactorPath = {
		"pacman", "redMonster", "blueMonster", "greenMonster", "pinkMonster"
	};

	void InitCharactorType()
	{
		createCharactorType = new Dictionary<int, string> ();
		for (int i = pacman_id; i < init_charactor_num_max; i++)
		{
			createCharactorType [i] = prefab_path + createCharactorPath[i];
		}
	}

	public void CreateNetworkCharactor(int charactorType)
	{
		InitCharactorType ();
		Vector3 createPos = Vector3.zero;

		if (createType == pacman_id)
		{
			createPos = new Vector3(0f, -2f, 0f);
		}
		else
		{
			createPos = new Vector3(charactorType-3f, 0f, 0f);
		}
		PhotonNetwork.Instantiate (createCharactorType[charactorType], createPos, Quaternion.identity, 0);
	}

	public GameObject CreateRoomCharactor()
	{
		InitCharactorType ();
		createType = PhotonNetwork.playerList.Length;

		Vector3 createPos = new Vector3 (((float)createType - 2.5f) * 3, 0f, 0f);
	
		return PhotonNetwork.Instantiate (createCharactorType[createType], createPos, Quaternion.identity, 0);
	}
}
