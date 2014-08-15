using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private const string ROOM_NAME = "test";

	void Awake()
	{
		if (!PhotonNetwork.connected) 
		{
			PhotonNetwork.ConnectUsingSettings("v1.0");
		}
		PhotonNetwork.playerName = PlayerPrefs.GetString ("playerName", "Guest" + Random.Range (1, 999));
	}

	void OnGUI()
	{
		if (!PhotonNetwork.connected) 
		{
			Debug.Log("接続できてません");
			return;
		}
		if (PhotonNetwork.room != null) 
		{
			return;
		}

		GUILayout.BeginArea (new Rect((Screen.width-400)/2, (Screen.height - 300)/2, 400, 300));
		
		GUILayout.Label("Main Menu");

		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Player name:", GUILayout.Width(150));
			PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
			if (GUILayout.Button ("GO"))
			{
				PhotonNetwork.JoinRoom(ROOM_NAME);
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.Space (15);

		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("CREAT ROOM:", GUILayout.Width(150));
			GUILayout.Label(ROOM_NAME, GUILayout.Width (150));
			if (GUILayout.Button("GO"))
			{
				PhotonNetwork.CreateRoom(ROOM_NAME, true, true, 10);
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.EndArea ();
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
