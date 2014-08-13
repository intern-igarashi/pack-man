using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	int stageLevel = 0;

	GameObject[] dots;

	// Use this for initialization
	void Awake () 
	{
		stageLevel = 0;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		dots = GameObject.FindGameObjectsWithTag("dot");
		if (dots.Length <= 0) 
		{
			Debug.Log("clear");
		}
	}

	public int GetSelectLevel() { return stageLevel; }
	
	public void SetStageLevel(int level) { stageLevel = level; }

}
