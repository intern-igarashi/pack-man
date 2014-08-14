﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStage : MonoBehaviour 
{
	// ゲームオブジェクト
	GameObject cube;
	GameObject pathway;
	GameObject dot;
	GameObject player;
	
	// 外部ファイルからのデータ
	string[] stageData;
	LoadingFile loadFile;
	int width, height;
	
	// ポジションの補正
	const float OFFSET_Y = 11.0f;
	const float OFFSET_X = -9.0f;

	// 
	Dictionary<string, GameObject> ObjectType;
	Dictionary<int, int> PlayerStartArrayPos;
	
	public float GetOffsetX() { return OFFSET_X; }
	public float GetOffsetY() { return OFFSET_Y; }
	
	//
	void ObjectTypeInit()
	{
		GameObject[] ObjectName = {
			cube, pathway, dot, null, null, null, null, player
		};
		for (int itr = 0; itr < ObjectName.Length; itr++) 
		{
			ObjectType.Add (itr.ToString(), ObjectName[itr]);
		}
	}
	
	// ステージの生成
	void Create(int h, int w, string[] date)
	{
		GameObject createObject = null;
		int playerNum = 0;
		
		ObjectTypeInit ();
		for (int y = 0; y < h; y++) 
		{
			for (int x = 0; x < w; x++)
			{
				//string dateType = date[y*w+x];
				createObject = ObjectType[date[y*w+x]];
				Instantiate (createObject, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), Quaternion.Euler(0f, 0f, 0f));
				if (createObject == player)
				{
					PlayerStartArrayPos[playerNum] = y*w+x;
					playerNum++;
				}
			}
		}
	}
	
	void Awake()
	{
		ObjectType = new Dictionary<string, GameObject> ();
		PlayerStartArrayPos = new Dictionary<int, int> ();
	}
	
	// Use this for initialization
	void Start () 
	{
		loadFile = GetComponent<LoadingFile> ();
		stageData = loadFile.SetDataValue();
		width = loadFile.WIDTH;
		height = loadFile.HEIGHT;
		cube = (GameObject)Resources.Load ("Prefab/cube");
		pathway = (GameObject)Resources.Load ("Prefab/pathway");
		dot = (GameObject)Resources.Load ("Prefab/dot");
		player = (GameObject)Resources.Load ("Prefab/player");
		Create (height, width, stageData);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
