using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class LoadingFile : MonoBehaviour
{
	// 定数
	public int WIDTH = 19;
	public int HEIGHT = 23;

	// 外部ファイルからのデータを保存
	string[] stageDateArray;

	//
	string[] ALL_FILE_NAME = {"stage_date", "stage_date_2"};

	// 読み込むファイルの名前
	string FileName;
	
	// 
	GameObject manager;

	// 読み込むファイル名を保存
	static Dictionary<int, string> loadFile;

	void FileDictinaryInit()
	{
		loadFile = new Dictionary<int, string>();
		for (int itr = 0; itr < ALL_FILE_NAME.Length; itr++) 
		{
			loadFile[itr] = ALL_FILE_NAME[itr];
		}
	}

	// デバック表示
	void Debug()
	{
		for (int y = 0; y < HEIGHT; y++) 
		{
			string s = "";
			for (int x = 0; x < WIDTH; x++)
			{
				s += stageDateArray[y*WIDTH+x]+",";
			}
			print (s);
		}
	}

	void LoadFileSelect()
	{
		manager = GameObject.Find("GameManager");
		FileDictinaryInit ();
		int level = manager.GetComponent<GameManager>().GetSelectLevel();
		FileName = loadFile[level];
	}

	// マップデータの生成
	public string[] SetDateValue()
	{
		LoadFileSelect ();
		TextAsset stage = Resources.Load (FileName+".csv") as TextAsset;
		stageDateArray = stage.text.Split (new string[]{"\r","\n", ","}, System.StringSplitOptions.RemoveEmptyEntries);
		return stageDateArray;
	}

	// Use this for initialization
	void Start () 
	{
		SetDateValue ();
		Debug ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
