using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class LoadingFile : MonoBehaviour
{
	public class Layer2D
	{
		public int width;
		public int height;
		private int[] array = null;


		public void Create(int width, int height)
		{
			this.width = width;
			this.height = height;
			array = new int[width*height];
		}

		public int Get(int x, int y)
		{
			if (x < 0 || x >= width) { return -1; }
			if (y < 0 || y >= height) { return -1; }
			return array [y * height + x];
		}

		public void Set(int x, int y, int value)
		{
			if (x < 0 || x >= width) { return; }
			if (y < 0 || y >= height) { return; }
			array [y * height + x] = value;
		}

		public void Debug()
		{
			print ("[Layer2D] (w,h)=(" + width + "," + height + ")");
			for (int y = 0; y < height; y++)
			{
				string s = "";
				for (int x = 0; x < width; x++)
				{
					s += Get (x, y) + ",";
				}
				print (s);
			}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		TextAsset stage = Resources.Load ("stage_date") as TextAsset;

		string[] stageArrayDate = stage.text.Split (new string[]{"\r","\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		for (int y = 0; y < stageArrayDate.Length; y++) 
		{
			print (stageArrayDate[y]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
