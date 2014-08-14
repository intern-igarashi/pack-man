using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFollowPlayer : MonoBehaviour 
{
	const string start = "";
	const string goal = "7";
	const int COST_MAX = 100000000;

	class Edge
	{
		public int to;
		public int cost;
		Dictionary<string, int> dataType = new Dictionary<string, int> (); 
		public Edge(int to, string type)
		{
			DataTypeInit();
			this.to = to;
			this.cost = dataType[type];
		}
		void DataTypeInit()
		{
			dataType.Add ("0", 10);
			dataType.Add ("1", 10);
			dataType.Add ("2", 1);
			dataType.Add ("3", 10);
			dataType.Add ("4", 10);
			dataType.Add ("5", 10);
			dataType.Add ("6", 10);
			dataType.Add ("7", 10);
		}
	}

	class Node
	{
		public Edge[] edges;

		public int cost;
		public int no;
		public int from;
		public Vector3 position;

		public Node(int no, Vector3 position, Edge[] edges)
		{
			this.no = no;
			this.position = position;
			this.edges = edges;

			this.cost = COST_MAX;
			this.from = -1;
		}
	}

	private Node[] nodes;

	private List<Node> openList;
	private List<Node> closeList;

	private Node startNode;
	private Node goalNode;

	void SetCost()
	{
		LoadingFile data = Camera.main.GetComponent<LoadingFile>();
		string[] stageData = data.SetDataValue ();

		this.nodes = new Node[data.HEIGHT * data.WIDTH];

		const float OFFSET_Y = 11.0f;
		const float OFFSET_X = -9.0f;

		for (int y = 0; y < data.HEIGHT; y++) 
		{
			for (int x = 0; x < data.WIDTH; x++)
			{
				if (y == 0 && x == 0)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
				else if (y == 0 && x == data.WIDTH-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
				else if (y == data.HEIGHT-1 && x == 0)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH])
					});
				}
				else if (y == data.HEIGHT-1 && x == data.WIDTH-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH])
					});
				}
				else if (y == 0 && x < data.WIDTH-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
				else if (y == data.HEIGHT-1 && x < data.WIDTH-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH])
					});
				}
				else if (x == 0 && y < data.HEIGHT-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
				else if (x == data.WIDTH-1 && y < data.HEIGHT-1)
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
				else
				{
					this.nodes[y*data.WIDTH+x] = new Node(y*data.WIDTH+x, new Vector3(x+OFFSET_X, -y+OFFSET_Y, 0f), new Edge[]{
						new Edge(y*data.WIDTH+x-1, stageData[y*data.WIDTH+x-1]),
						new Edge(y*data.WIDTH+x+1, stageData[y*data.WIDTH+x+1]),
						new Edge(y*data.WIDTH+x-data.WIDTH, stageData[y*data.WIDTH+x-data.WIDTH]),
						new Edge(y*data.WIDTH+x+data.WIDTH, stageData[y*data.WIDTH+x+data.WIDTH])
					});
				}
			}
		}
		SearchRoot (400, 56);
		Move ();
	}

	void SearchRoot(int goal, int start)
	{
		this.goalNode = this.nodes[goal];
		this.startNode = this.nodes [start];
		this.openList = new List<Node> ();
		this.closeList = new List<Node> ();

		this.openList.Add (this.startNode);
		this.startNode.cost = this.Heuristic(this.startNode);

		while (true)
		{
			if (this.openList.Count == 0) break;

			int openListNo = -1;
			int nodeNo = -1;
			Node n = null;
			int minCost = COST_MAX;
			for (int itr = 0; itr < this.openList.Count; itr++)
			{
				if (this.openList[itr].cost < minCost)
				{
					openListNo = itr;
					nodeNo = this.openList[itr].no;
					n = this.openList[itr];
					minCost = n.cost;
				}
			}

			if (n == this.goalNode)
			{
				break;
			}
			else
			{
				this.closeList.Add(n);
				this.openList.Remove(n);
			}

			for (int itr = 0; itr < n.edges.Length; itr++)
			{
				Node m = this.nodes[n.edges[itr].to];

				int ga = n.cost - this.Heuristic(n);
				int ha = this.Heuristic(n);
				int costNM = n.edges[itr].cost;
				int fm = ga + ha + costNM;

				if (this.openList.IndexOf(m) != -1)
				{
					if (fm < m.cost)
					{
						m.cost = fm;
						m.from = n.no;
					}
				}
				else if (this.closeList.IndexOf(m) != -1)
				{
					if (fm < m.cost)
					{
						m.cost = fm;
						m.from = n.no;

						this.openList.Add(m);
						this.closeList.Remove(m);
					}
				}
				else
				{
					m.cost = fm;
					m.from = n.no;

					this.openList.Add (m);
				}
			}
		}
	}

	// ヒューリスティク関数
	int Heuristic(Node n)
	{
		Vector3 line = goalNode.position - n.position;
		return (int)line.sqrMagnitude;
	}


	float s_time;

	void Move()
	{
		s_time += Time.deltaTime;
		int num = Mathf.Max (0, nodes.Length - (int)s_time);
		transform.position += Vector3.Normalize(nodes [num].position-transform.position)*Time.deltaTime*3.0f;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetCost ();
	}
}
