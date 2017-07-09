using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class StartWork : MonoBehaviour
{
	int mapWidth = 500;
	int corridorWidth = 2;
	List<Room> rooms = new List<Room>();
	Dictionary<KeyValuePair<int, int>, int> enablesPosition = new Dictionary<KeyValuePair<int, int>, int>();
	List<List<int>> mapa = new List<List<int>>();
	System.Random rnd = new System.Random();
	List<List<bool>> used = new List<List<bool>>();

	Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
	public void Start()
	{
		loadRoom();
		startGenerate();
		Floor();
		for(int i=1;i<mapWidth-1;i++)
		{
			for(int j=1;j<mapWidth-1;j++)
			{
				if (mapa[i][j] == 0 && (mapa[i + 1][j] == 1 || mapa[i - 1][j] == 1 || mapa[i][j + 1] == 1 || mapa[i][j - 1] == 1))
				{
					AddGO("3", j, i, true);
				}
			}
		}
	}
	void loadRoom()
	{
		for (int i = 1; i < 4; i++)
		{
			Texture2D rr = Resources.Load(i.ToString()) as Texture2D;
			textures[rr.name] = rr;
		}
		for (int i = 1; i <= 5; i++)
		{
			rooms.Add(new Room(i));
		}
	}
	KeyValuePair<int, int> getRandomPosition()
	{
		return enablesPosition.ElementAt(rnd.Next(0, enablesPosition.Count)).Key;
	}

	void startGenerate()
	{
		for (int i = 0; i < mapWidth; i++)
		{
			mapa.Add(new List<int>());
			used.Add(new List<bool>());
			for (int j = 0; j < mapWidth; j++)
			{
				mapa[i].Add(0);
				used[i].Add(false);
			}
		}
		int roomCount = 70;
		int curRoomCount = 0;
		enablesPosition[new KeyValuePair<int, int>(10, 10)] = 1;
		while (curRoomCount < roomCount)
		{
			int k = rnd.Next(rooms.Count);
			int cc = 0;
			KeyValuePair<int, int> pos = getRandomPosition();

			while (cc < 5 && !isEnable(pos.Key, pos.Value, rooms[k]))
			{ cc++; pos = getRandomPosition(); }
			if (cc < 5)
				recalculateEnablePosition(pos.Key, pos.Value, rooms[k]);
			curRoomCount++;
			if (cc < 5)
				BuildRoom(rooms[k], pos.Key, pos.Value);

			//GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("remove");
			//for (var i = 0; i < gameObjects.Length; i++)
			//{
			//	Destroy(gameObjects[i]);
			//}
			//foreach (KeyValuePair<KeyValuePair<int, int>, int> entry in enablesPosition)
			//{

			//	GameObject g = new GameObject("ede");
			//	g.tag = "remove";
			//	Texture2D t = textures["2"];

			//	g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
			//	g.GetComponent<SpriteRenderer>().sortingOrder = 33;
			//	//g.GetComponent<SpriteRenderer>().color = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
			//	g.transform.position = new Vector2(entry.Key.Key * 48, entry.Key.Value * 48);

			//}


		}
	}
	List<KeyValuePair<Room, KeyValuePair<int, int>>> r = new List<KeyValuePair<Room, KeyValuePair<int, int>>>();
	void BuildRoom(Room c, int x, int y)
	{
		for (int i = 0; i < c.map.Count; i++)
		{
			AddGO(c.texture[i], c.map[i].Key + x, c.map[i].Value + y,true);
		}
		r.Add(new KeyValuePair<Room, KeyValuePair<int, int>>(c, new KeyValuePair<int, int>(x, y)));
		
		//for (int i = 0; i < c.height; i++)
		//{
		//	for (int j = 0; j < c.width; j++)
		//		mapa[y+i][x+j] = 1;
		//}
	}
	public void reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	bool isEnable(int x, int y, Room cc)
	{
		if (x <= corridorWidth || y <= corridorWidth || y + corridorWidth >= mapWidth || x + corridorWidth >= mapWidth)
			return false;
		for (int i = x; i < x + cc.width; i++)
		{
			if (!pointCheck(i, y))
				return false;
			if (!pointCheck(i, y + cc.height - 1))
				return false;
		}
		for (int i = y; i < y + cc.height; i++)
		{
			if (!pointCheck(x, i))
				return false;
			if (!pointCheck(x + cc.width - 1, i))
				return false;
		}
		return true;
	}
	bool pointCheck(int x, int y)
	{
		try
		{
			for (int i = x; i < x + corridorWidth; i++)
				if (mapa[y][i] != 0)
				{ enablesPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = x; i > x - corridorWidth; i--)
				if (mapa[y][i] != 0)
				{ enablesPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = y; i < y + corridorWidth; i++)
				if (mapa[i][x] != 0)
				{ enablesPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
			for (int i = y; i > y - corridorWidth; i--)
				if (mapa[i][x] != 0)
				{ enablesPosition.Remove(new KeyValuePair<int, int>(x, y)); return false; }
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
			if (mapa[y1][x1] == 0)
				enablesPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx - corridorWidth, y1 = posy + corridorWidth + h - 1;
			if (mapa[y1][x1] == 0)
				enablesPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx + corridorWidth + w - 1, y1 = posy - corridorWidth;
			if (mapa[y1][x1] == 0)
				enablesPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
		try
		{
			int x1 = posx + corridorWidth + w - 1, y1 = posy + corridorWidth + h - 1;
			if (mapa[y1][x1] == 0)
				enablesPosition[new KeyValuePair<int, int>(x1, y1)] = 1;
		}
		catch { }
	}
	void AddGO(string name, int x, int y, bool check)
	{
		GameObject g = new GameObject();
		Texture2D t = textures[name];

		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		g.GetComponent<SpriteRenderer>().sortingOrder = 33;
		//g.GetComponent<SpriteRenderer>().color = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));
		g.transform.position = new Vector2(x * 48, y * 48);
		if (check)
		{
			mapa[y][x] = Convert.ToInt32(name);
			enablesPosition.Remove(new KeyValuePair<int, int>(x, y));
		}
	}

	Dictionary<KeyValuePair<int, int>, int> entires = new Dictionary<KeyValuePair<int, int>, int>();
	void findEntires()
	{
		int x = 0, y = 0;
		for (int i = 0; i < mapWidth; i++)
		{
			for (int j = 0; j < mapWidth; j++)
			{
				if (mapa[i][j] == 1 && (mapa[i + 1][j] == 0 || mapa[i - 1][j] == 0 || mapa[i][j + 1] == 0 || mapa[i][j - 1] == 0))
				{
					x = j;
					y = i;
					entires[new KeyValuePair<int, int>(j, i)] = 1;
				}
			}
		}
		//dfs(x, y);
	}
	void Floor()
	{
		for (int j = 0; j < r.Count; j++)
		{
			int x = r[j].Value.Key;
			int y = r[j].Value.Value;
			Room c = r[j].Key;
			int ww = corridorWidth;
			for (int l = 1; l <= ww; l++)
			{
				int x1 = x - l, y1 = y - l;
				for (int i = x1; i < x1 + c.width + 2 * l; i++)
				{
					if (mapa[y1][i] == 0)
						AddGO("1", i, y1, true);
					if (mapa[y1 + c.height + 2 * l - 1][i] == 0)
						AddGO("1", i, y1 + c.height + 2 * l - 1, true);
					//mapa[y1][i] = 2;
					//mapa[y1 + c.height + 1][i] = 2;
				}
				for (int i = y1 + 1; i < y1 + c.height + 2 * l - 1; i++)
				{
					if (mapa[i][x1] == 0)
						AddGO("1", x1, i, true);
					if (mapa[i][x1 + c.width + 2 * l - 1] == 0)
						AddGO("1", x1 + c.width + 2 * l - 1, i, true);

					//mapa[i][x1] = 2;
					//mapa[i][x1 + c.width + 1] = 2;
				}
			}
		}
	}
}
