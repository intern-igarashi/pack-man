using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	int stageLevel = 0;

	// Use this for initialization
	void Awake () 
	{
		stageLevel = 0;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int GetSelectLevel() { return stageLevel; }
	
	public void SetStageLevel(int level) { stageLevel = level; }

}
