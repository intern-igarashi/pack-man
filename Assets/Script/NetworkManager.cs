using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	GameObject playerMaker;

	const string prefabPath = "Prefab/";
	const string ROOM_NAME = "PAC MAN"; 

	string playerPrefabName = prefabPath+"pacman";
	string playerName = "GUEST_AAA";

	public bool isJoinedRoom = false;

	void Awake()
	{
		PhotonNetwork.automaticallySyncScene = true;

		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated) 
		{
			PhotonNetwork.ConnectUsingSettings ("1.0");
		}

		if (PhotonNetwork.playerName == null) 
		{
			this.playerName = "GUEST" + UnityEngine.Random.Range (1, 999);
			PhotonNetwork.playerName = this.playerName;
		}
		else
		{
			this.playerName = PhotonNetwork.playerName;
		}
	}
		
	void OnJoinedLobby()
	{
		Debug.Log ("ロビーに入りました");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnCreatedRoom()
	{
		Debug.Log ("ルームを作成しました");
	}

	void OnJoinedRoom()
	{
		Debug.Log ("ルームに入室しました");
		StartGame ();
		isJoinedRoom = true;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log ("ルームに入室に失敗しました");
		PhotonNetwork.CreateRoom (ROOM_NAME,true, true, 4);
	}

//	IEnumerator OnLeftRoom()
//	{
//		while (PhotonNetwork.room != null || PhotonNetwork.connected == false) 
//		{
//			yield return 0;
//		}
//
//		Application.LoadLevel (Application.loadedLevel);
//	}

//	void OnGUI()
//	{
//		if (PhotonNetwork.room == null) 
//		{
//			return;
//		}
//		if (GUILayout.Button ("Leave & Quit")) 
//		{
//			PhotonNetwork.LeaveRoom();
//		}
//	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}

	void StartGame()
	{
		PhotonNetwork.Instantiate ("Prefab/GameManager", Vector3.zero, Quaternion.identity, 0);
		playerMaker = PhotonNetwork.Instantiate ("Prefab/PlayerMaker", Vector3.zero, Quaternion.identity, 0);
		playerMaker.GetComponent<CreatePlayer> ().CreateRoomCharactor ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
