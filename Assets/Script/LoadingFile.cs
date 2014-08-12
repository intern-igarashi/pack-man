﻿using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class LoadingFile : MonoBehaviour
{
	// 定数
	public int WIDTH = 19;
	public int HEIGHT = 23;

	// 外部ファイルからのデータを保存
	string[] stageDateArray;

	// 読み込むファイルの名前
	string FileName = "stage";

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
	public string[] SetDateValue()
	{
		TextAsset stage = Resources.Load (FileName) as TextAsset;
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