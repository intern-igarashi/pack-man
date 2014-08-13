using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateStage : MonoBehaviour 
{
	// ゲームオブジェクト
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

	static Dictionary<string, GameObject> ObjectType;

	void ObjectTypeInit()
	{
		GameObject[] ObjectName = {
			cube, cube, cube, cube, cube, 
			cube, pathway, cube, player
		};
		for (int itr = 0; itr < ObjectName.Length; itr++) 
		{
			ObjectType.Add (itr.ToString(), ObjectName[itr]);
		}
	}

	// ステージの生成
	void Create(int h, int w, string[] date)
	{
		Debug.Log ("a");
		GameObject createObject = null;
		ObjectTypeInit ();
		for (int y = 0; y < h; y++) 
		{
			for (int x = 0; x < w; x++)
			{
				//string dateType = date[y*w+x];
				createObject = ObjectType[date[y*w+x]];
				Instantiate (createObject, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), Quaternion.Euler(0f, 0f, 0f));
			}
		}
	}

	void Awake()
	{
		ObjectType = new Dictionary<string, GameObject> ();
	}

	// Use this for initialization
	void Start () 
	{
		loadFile = GetComponent<LoadingFile> ();
		stageDate = loadFile.SetDateValue();
		width = loadFile.WIDTH;
		height = loadFile.HEIGHT;
		cube = (GameObject)Resources.Load ("Prefab/cube");
		pathway = (GameObject)Resources.Load ("Prefab/pathway");
		player = (GameObject)Resources.Load ("Prefab/player");
		Create (height, width, stageDate);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
