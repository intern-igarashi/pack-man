using UnityEngine;
using System.Collections;

public class GameManager : Photon.MonoBehaviour
{
	int stageLevel = 0;

	public bool isPlayerGame = false;

	GameObject[] dots;
	public int controlPlayerType;	

	// Use this for initialization
	void Awake () 
	{
		stageLevel = 0;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		dots = GameObject.FindGameObjectsWithTag("dot");
//		if (dots.Length <= 0) 
//		{
//			Debug.Log("clear");
//		}

	}

	public void SetControlPlayerType(int type){ controlPlayerType = type; }

	public int GetControlPlayerType(){ return controlPlayerType;}

	public int GetSelectLevel() { return stageLevel; }
	
	public void SetStageLevel(int level) { stageLevel = level; }

}
