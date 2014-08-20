using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	GameObject playerMaker;

	const string prefabPath = "Prefab/";
	const string ROOM_NAME = "PAC MAN"; 

	string playerName = "GUEST_AAA";
	string roomName = ROOM_NAME;

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
	}

	void OnMasterClientSwitched()
	{
		PhotonNetwork.LeaveRoom ();
		isJoinedRoom = false;
	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());

		if (!isJoinedRoom)
		{
			this.roomName = GUILayout.TextField (this.roomName);
			
			if (GUILayout.Button("Create Room", GUILayout.Width(100)))
			{
				PhotonNetwork.CreateRoom(this.roomName, true, true, 4);
			}
			foreach (RoomInfo game in PhotonNetwork.GetRoomList())
			{
				if (GUILayout.Button("\"" + game.name + "\"" + " Join Room" + " " + game.playerCount + "/" + game.maxPlayers))
				{
					PhotonNetwork.JoinRoom(this.roomName);
				}
			}
		}
		else
		{
			if (PhotonNetwork.playerList.Length == 1)
			{
				if (GUILayout.Button("Leave Room", GUILayout.Width(100)))
				{
					PhotonNetwork.LeaveRoom();
					isJoinedRoom = false;
				}
			}
		}
	}

	void StartGame()
	{
		if (PhotonNetwork.isMasterClient)
		{
			PhotonNetwork.Instantiate ("Prefab/GameManager", Vector3.zero, Quaternion.identity, 0);
		}
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
