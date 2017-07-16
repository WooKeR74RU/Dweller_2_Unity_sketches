using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class StartWork : MonoBehaviour
{
	int mapWidth = 500;
	int corridorWidth = 3;
	int roomCount = 50;

	Dictionary<KeyValuePair<int, int>, bool[]> visitedPoints = new Dictionary<KeyValuePair<int, int>, bool[]>();
	Dictionary<KeyValuePair<int, int>, int> enablesBuildRoomPosition = new Dictionary<KeyValuePair<int, int>, int>();
	Dictionary<string, Texture2D> getTextureByName = new Dictionary<string, Texture2D>();
	Dictionary<KeyValuePair<int, int>, Room> getRoomByStartPosition = new Dictionary<KeyValuePair<int, int>, Room>();
	Dictionary<KeyValuePair<int, int>, int> roomComponent = new Dictionary<KeyValuePair<int, int>, int>();

	List<Room> rooms = new List<Room>();
	List<List<int>> map = new List<List<int>>();
	List<ItemDescription> RoadElements = new List<ItemDescription>();

	System.Random rnd = new System.Random();

	public void Start()
	{
		loadResources();
		startGenerate();
		BuildPaths();
		//surroundFloor(2);
		BuildWall();
		//dfs(getRoomByStartPosition[new KeyValuePair<int, int>(10,10)].entires[0].Key, getRoomByStartPosition[new KeyValuePair<int, int>(10, 10)].entires[0].Value);
	}
	void BuildWall()
	{
		for(int i=1;i<mapWidth-1;i++)
		{
			for (int j = 1; j < mapWidth-1; j++)
			{
				if(map[i][j] == 0 && (map[i+1][j] == 1 || map[i - 1][j] == 1 || map[i ][j+1] == 1 || map[i][j-1] == 1))
				{
					map[i][j] = 3;
					addEntity("3", j, i);
				}
			}
		}
	}
	void BuildPaths()
	{
		KeyValuePair<KeyValuePair<int, int>, Room> cur = getNearlyRoom(new KeyValuePair<int, int>(0, 0));
		while (roomComponent.Count < getRoomByStartPosition.Count)
		{
				KeyValuePair<KeyValuePair<int, int>, Room> next = getNearlyRoom(cur.Key);
				KeyValuePair<int, int> ent1 = getEntire(cur);
				KeyValuePair<int, int> ent2 = getEntire(next);
				Debug.Log(ent1.ToString() + " " + ent2.ToString());
				buildPathBetweenTwoCells(ent1, ent2);
				cur = next;
		}
		for (int i = 0; i < RoadElements.Count; i++)
		{
			map[RoadElements[i].y][RoadElements[i].x] = 1;
			addEntity(RoadElements[i].id.ToString(), RoadElements[i].x, RoadElements[i].y);
		}
	}
	KeyValuePair<int,int> getEntire(KeyValuePair<KeyValuePair<int, int>, Room> a)
	{

		double d = 1000000000;
		KeyValuePair<int, int> rez = new KeyValuePair<int, int>();
		for (int i=0;i<a.Value.entires.Count;i++)
		{
			double x = Math.Abs(a.Value.entires[i].Key - a.Key.Key);
			double y = Math.Abs(a.Value.entires[i].Value - a.Key.Value);
			double d1 = Math.Sqrt(x * x + y * y);
			if (d1 < d)
			{
				d = d1;
				rez = new KeyValuePair<int,int>(a.Value.entires[i].Key+a.Key.Key, a.Value.entires[i].Value + a.Key.Value);
			}
		}
		return rez;
	}
	KeyValuePair<KeyValuePair<int, int>, Room> getNearlyRoom(KeyValuePair<int,int> pos)
	{
		double d = 1000000000;
		KeyValuePair<KeyValuePair<int, int>, Room> rez = new KeyValuePair<KeyValuePair<int, int>, Room>();
		foreach (KeyValuePair<KeyValuePair<int,int>,Room> en in getRoomByStartPosition)
		{
			double x = Math.Abs(en.Key.Key - pos.Key);
			double y = Math.Abs(en.Key.Value - pos.Value);
			double d1 = Math.Sqrt(x * x + y * y);
			if (d1 < d && !roomComponent.ContainsKey(en.Key) && !(en.Key.Key == pos.Key && en.Key.Value == pos.Value))
			{
				d = d1;
				rez = en;
			}
		}
		roomComponent[rez.Key] = 1;
		return rez;
	}
	void loadResources()
	{
		for (int i = 1; i < 10; i++)
		{
			try
			{
				Texture2D rr = Resources.Load(i.ToString()) as Texture2D;
				getTextureByName[rr.name] = rr;
			}
			catch { }
		}
	
		for (int i = 1; i <= 5; i++)
		{
			rooms.Add(new Room(i));
		}
	}
	KeyValuePair<int, int> getRandomBuildPosition()
	{
		return enablesBuildRoomPosition.ElementAt(rnd.Next(0, enablesBuildRoomPosition.Count)).Key;
	}

	void startGenerate()
	{
		for (int i = 0; i < mapWidth; i++)
		{
			map.Add(new List<int>());
			for (int j = 0; j < mapWidth; j++)
			{
				map[i].Add(0);
			}
		}
		
		int curRoomCount = 0;
		enablesBuildRoomPosition[new KeyValuePair<int, int>(10, 10)] = 1;
		while (curRoomCount < roomCount)
		{
			int k = rnd.Next(rooms.Count);
			int cc = 0;
			KeyValuePair<int, int> pos = getRandomBuildPosition();

			while (cc < 5 && !isEnableBuildRoom(pos.Key, pos.Value, rooms[k]))
			{ cc++; pos = getRandomBuildPosition(); }
			if (cc < 5)
				recalculateEnablePosition(pos.Key, pos.Value, rooms[k]);
			curRoomCount++;
			if (cc < 5)
				BuildRoom(rooms[k], pos.Key, pos.Value);
		}
	}
	void BuildRoom(Room c, int x, int y)
	{
		for (int i = 0; i < c.map.Count; i++)
		{
			int x_= c.map[i].x + x, y_ = c.map[i].y + y;
			addEntity(c.map[i].id.ToString(), x_, y_);
			map[y_][x_] = c.map[i].id;
			enablesBuildRoomPosition.Remove(new KeyValuePair<int, int>(x_, y_));
		}
		getRoomByStartPosition[new KeyValuePair<int, int>(x, y)] = c;
	}
	public void reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	bool isEnableBuildRoom(int x, int y, Room cc)
	{
		if (x <= corridorWidth || y <= corridorWidth || y + corridorWidth >= mapWidth || x + corridorWidth >= mapWidth)
			return false;
		for (int i = x; i < x + cc.width; i++)
		{
			if (!isPointEnable(i, y))
				return false;
			if (!isPointEnable(i, y + cc.height - 1))
				return false;
		}
		for (int i = y; i < y + cc.height; i++)
		{
			if (!isPointEnable(x, i))
				return false;
			if (!isPointEnable(x + cc.width - 1, i))
				return false;
		}
		return true;
	}
	bool isPointEnable(int x, int y)
	{
		try
		{
			for (int i = x; i < x + corridorWidth; i++)
				if (map[y][i] != 0)
				{ enablesBuildRoomPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = x; i > x - corridorWidth; i--)
				if (map[y][i] != 0)
				{ enablesBuildRoomPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = y; i < y + corridorWidth; i++)
				if (map[i][x] != 0)
				{ enablesBuildRoomPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = y; i > y - corridorWidth; i--)
				if (map[i][x] != 0)
				{ enablesBuildRoomPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
		}
		catch { return false; }
		return true;
	}
	void recalculateEnablePosition(int posx, int posy, Room cc)
	{

		int w = cc.width, h = cc.height;
		try
		{
			int x1 = posx - corridorWidth, y1 = posy - corridorWidth;
			if (map[y1][x1] == 0)
				enablesBuildRoomPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx - corridorWidth, y1 = posy + corridorWidth + h - 1;
			if (map[y1][x1] == 0)
				enablesBuildRoomPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx + corridorWidth + w - 1, y1 = posy - corridorWidth;
			if (map[y1][x1] == 0)
				enablesBuildRoomPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx + corridorWidth + w - 1, y1 = posy + corridorWidth + h - 1;
			if (map[y1][x1] == 0)
				enablesBuildRoomPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
	}
	void addEntity(string textureName, int x, int y)
	{
		GameObject g = new GameObject();
		Texture2D t = getTextureByName[textureName];

		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		g.GetComponent<SpriteRenderer>().sortingOrder = 33;
		//g.GetComponent<SpriteRenderer>().color = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		g.transform.position = new Vector2(x * 48, y * 48);
	}

	void surroundFloor(int radius)
	{
		foreach(KeyValuePair<KeyValuePair<int,int>,Room> ent in getRoomByStartPosition)
		{
			int x = ent.Key.Key;
			int y = ent.Key.Value;
			Room c = ent.Value;
			int currentCorridorWidth = radius;
			for (int l = 1; l <= currentCorridorWidth; l++)
			{
				int x1 = x - l, y1 = y - l;
				for (int i = x1; i < x1 + c.width + 2 * l; i++)
				{
					if (map[y1][i] == 0)
					{ addEntity("1", i, y1); map[y1][i] = 1; }
					if (map[y1 + c.height + 2 * l - 1][i] == 0)
					{ addEntity("1", i, y1 + c.height + 2 * l - 1); map[y1 + c.height + 2 * l - 1][i] = 1; }
				}
				for (int i = y1 + 1; i < y1 + c.height + 2 * l - 1; i++)
				{
					if (map[i][x1] == 0)
					{ addEntity("1", x1, i);map[i][x1] = 1; }
					if (map[i][x1 + c.width + 2 * l - 1] == 0)
					{ addEntity("1", x1 + c.width + 2 * l - 1, i); map[i][x1 + c.width + 2 * l - 1] = 1; }
				}
			}
		}
	}
	void buildPathBetweenTwoCells(KeyValuePair<int, int> from, KeyValuePair<int, int> to)
	{
		RoadElements.Add(new ItemDescription(1, from.Key, from.Value));
		if (from.Key == to.Key && from.Value == to.Value)
		{
			visitedPoints.Clear();
			return;
		}																									
	
		List<int> move = getPriorityMoveVector(from, to);
		if (!visitedPoints.ContainsKey(from))
		{
			visitedPoints[from] = new bool[5];
		}
		for (int j = 0; j < move.Count; j++)
		{
			KeyValuePair<int, int> point = getMovePoint(from, move[j]);
				if (point.Value < 0 || point.Key < 0 || map.Count <= point.Value || map[point.Value].Count <= point.Key || map[point.Value][point.Key] != 0)
				{
					visitedPoints[from][move[j]] = true;
				}
		}
		for (int i = 0; i < move.Count; i++)
		{
			KeyValuePair<int, int> point = getMovePoint(from, move[i]);
			bool v = false;
			if (visitedPoints.ContainsKey(point))
				for (int j = 1; j < 5; j++)
				{
					if (visitedPoints[point][j] == false)
						v = true;
				}
			else
				v = true;
			if((point.Key == to.Key && point.Value == to.Value))
			{
				visitedPoints[from][move[i]] = true;
				buildPathBetweenTwoCells(point, to);
				return;
			}
			
			if (v && !visitedPoints[from][move[i]] && (map[point.Value][point.Key] == 0))
			{
				visitedPoints[from][move[i]] = true;
				buildPathBetweenTwoCells(point, to);
				return;
			}
			visitedPoints[from][move[i]] = true;
		}
	}
	KeyValuePair<int, int> getMovePoint(KeyValuePair<int, int> a, int move)
	{
		switch (move)
		{
			case 1:
				return new KeyValuePair<int, int>(a.Key, a.Value + 1);
				break;
			case 2:
				return new KeyValuePair<int, int>(a.Key - 1, a.Value);
				break;
			case 3:
				return new KeyValuePair<int, int>(a.Key, a.Value - 1);
				break;
			case 4:
				return new KeyValuePair<int, int>(a.Key + 1, a.Value);
				break;

		}
		return new KeyValuePair<int, int>();

	}
	List<int> getPriorityMoveVector(KeyValuePair<int, int> a, KeyValuePair<int, int> b)
	{
		List<int> priorityQueue = new List<int>();
		KeyValuePair<int, int> vector = new KeyValuePair<int, int>(b.Key - a.Key, b.Value - a.Value);
		KeyValuePair<int, int> absVector = new KeyValuePair<int, int>(Math.Abs(vector.Key), Math.Abs(vector.Value));
		if(absVector.Key >= absVector.Value)
		{
			priorityQueue.Add(4);
			priorityQueue.Add(1);
			priorityQueue.Add(3);
			priorityQueue.Add(2);
			if(vector.Value < 0)
			{
				int c = priorityQueue[1];
				priorityQueue[1] = priorityQueue[2];
				priorityQueue[2] = c;
			}
			if(vector.Key < 0)
			{
				int c = priorityQueue[0];
				priorityQueue[0] = priorityQueue[3];
				priorityQueue[3] = c;
			}

		}
		else
		{
			priorityQueue.Add(1);
			priorityQueue.Add(4);
			priorityQueue.Add(2);
			priorityQueue.Add(3);

			if (vector.Key < 0)
			{
				int c = priorityQueue[1];
				priorityQueue[1] = priorityQueue[2];
				priorityQueue[2] = c;
			}
			if (vector.Value < 0)
			{
				int c = priorityQueue[0];
				priorityQueue[0] = priorityQueue[3];
				priorityQueue[3] = c;
			}
		}
		return priorityQueue;
	}

	//DFS FOR TEST
	int[,] uu = new int[300,300];
	void dfs(int x,int y)
	{
		if (x < 0 || y < 0 || uu[y,x] == 1  || map[y][x] != 1)
			return;
		uu[y,x] = 1;
		GameObject g = new GameObject();
		Texture2D t = getTextureByName["2"];

		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		g.GetComponent<SpriteRenderer>().sortingOrder = 55;
		//g.GetComponent<SpriteRenderer>().color = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		g.transform.position = new Vector2(x * 48, y * 48);
		dfs(x + 1, y);
		dfs(x - 1, y);
		dfs(x, y+1);
		dfs(x, y-1);
	}
}
