using UnityEngine;
using System.Collections;

public class CreateStage : MonoBehaviour 
{
	// ゲームオブジェクト
	// インスペクターで設定
	public GameObject cube;
	public GameObject pathway;
	public GameObject player;

	// 外部ファイルからのデータ
	string[] stageDate;
	LoadingFile loadFile;
	int width, height;

	// ポジションの補正
	const float OFFSET_Y = 11.0f;
	const float OFFSET_X = -9.0f;

	// ステージの生成
	void Create(int h, int w, string[] date)
	{
		GameObject createObject = null;
		for (int y = 0; y < h; y++) 
		{
			for (int x = 0; x < w; x++)
			{
				switch (date[y*w+x])
				{
				case "0":
					createObject = cube;
					break;
				case "1":
					createObject = cube;
					break;
				case "2":
					createObject = cube;
					break;
				case "3":
					createObject = cube;
					break;
				case "4":
					createObject = cube;
					break;
				case "5":
					createObject = cube;
					break;
				case "6":
					createObject = pathway;
					break;
				case "7":
					createObject = cube;
					break;
				case "8":
					createObject = player;
					break;
				}
				Instantiate (createObject, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), Quaternion.Euler(0f, 0f, 0f));
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		loadFile = GetComponent<LoadingFile> ();
		stageDate = loadFile.SetDateValue();
		width = loadFile.WIDTH;
		height = loadFile.HEIGHT;
		Create (height, width, stageDate);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
