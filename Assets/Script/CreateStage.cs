using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStage : Photon.MonoBehaviour 
{
	const string FILE_PATH = "Prefab/";

	// ゲームオブジェクト
	string cube = "cube";
	string pathway = "pathway";
	string dot = "dot";
	string pacman = "pacman";
	
	// 外部ファイルからのデータ
	string[] stageData;
	LoadingFile loadFile;
	int width, height;
	
	// ポジションの補正
	const float OFFSET_Y = 11.0f;
	const float OFFSET_X = -9.0f;
	
	Dictionary<string, string> ObjectType;
	public Dictionary<int, int> PlayerStartArrayPos;
	
	public float GetOffsetX() { return OFFSET_X; }
	public float GetOffsetY() { return OFFSET_Y; }
	
	void ObjectTypeInit()
	{
		string[] ObjectName = {
			cube, pathway, dot, null, null, null, null, pacman
		};
		for (int itr = 0; itr < ObjectName.Length; itr++) 
		{
			ObjectType.Add (itr.ToString(), FILE_PATH + ObjectName[itr]);
		}
	}
	
	// ステージの生成
	void Create(int h, int w, string[] date)
	{
		string createObject = null;
		int playerNum = 0;
		
		ObjectTypeInit ();
		for (int y = 0; y < h; y++) 
		{
			for (int x = 0; x < w; x++)
			{
				//string dateType = date[y*w+x];
				createObject = ObjectType[date[y*w+x]];
				PhotonNetwork.Instantiate (createObject, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), Quaternion.identity, 0);
//				if (createObject == pacman)
//				{
//					PlayerStartArrayPos[playerNum] = y*w+x;
//					playerNum++;
//				}
			}
		}
	}
	
	void Awake()
	{
		ObjectType = new Dictionary<string, string> ();
		PlayerStartArrayPos = new Dictionary<int, int> ();
	}
	
	// Use this for initialization
	void Start () 
	{
		loadFile = GetComponent<LoadingFile> ();
		stageData = loadFile.SetDataValue();
		width = loadFile.WIDTH;
		height = loadFile.HEIGHT;
//		cube = (GameObject)Resources.Load ("Prefab/cube");
//		pathway = (GameObject)Resources.Load ("Prefab/pathway");
//		dot = (GameObject)Resources.Load ("Prefab/dot");
//		pacman = (GameObject)Resources.Load ("Prefab/pacman");
		Create (height, width, stageData);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
