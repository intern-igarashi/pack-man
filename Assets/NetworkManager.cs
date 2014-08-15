using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour
{
	const string prefabPass = "Prefab/";
	const string ROOM_NAME = "PAC MAN"; 

	string playerPrefabName = prefabPass+"pacman";
	string playerName = "GUEST_AAA";


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

		PhotonNetwork.Instantiate (playerPrefabName, getPositionRandomly(), Quaternion.identity, 0);
	}

	Vector3 getPositionRandomly()
	{
		return new Vector3 (Random.Range(-3.0f, 3.0f), 1, Random.Range(-3.0f, 3.0f));
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
