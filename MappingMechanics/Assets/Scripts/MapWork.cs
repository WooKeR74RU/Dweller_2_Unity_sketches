using System;
using System.Collections.Generic;
using UnityEngine;

public class MapDescription
{
	public string mapName;
	public int N, M;
	public Pair<int, int> segmentSize;
	public Pair<int, int> startSegment;
	public Pair<int, int> startPoint;
	public MapDescription(string mapName)
	{
		string path = "Maps/" + mapName + "/description";
		string desc = (Resources.Load(path) as TextAsset).text;
		string[] parameters = desc.Split(' ', '\n');
		for (int i = 0; i < parameters.Length; i += 2)
		{
			if (parameters[i] == "MapName")
			{
				mapName = parameters[i + 1];
			}
			if (parameters[i] == "MatrixSize")
			{
				string[] pos = parameters[i + 1].Split('x');
				N = Convert.ToInt32(pos[1]);
				M = Convert.ToInt32(pos[0]);
			}
			if (parameters[i] == "SegmentSize")
			{
				string[] pos = parameters[i + 1].Split('x');
				segmentSize = new Pair<int, int>(Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]));
			}
			if (parameters[i] == "StartSegment")
			{
				string[] pos = parameters[i + 1].Split('x');
				startSegment = new Pair<int, int>(Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]));
			}
			if (parameters[i] == "StartPoint")
			{
				string[] pos = parameters[i + 1].Split('x');
				startPoint = new Pair<int, int>(Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]));
			}
		}
	}
}

public class MapContent
{
	public int N, M;
	public List<List<List<Pair<int, int>>>> field; //type, id in array of type
	public List<Entity> entities = new List<Entity>();
	public List<Item> items = new List<Item>();
	public List<Unit> units = new List<Unit>();
	public MapContent(string path)
	{
		UnityEngine.Object file = Resources.Load(path);
		if (file == null)
		{
			N = M = 0;
			field = new List<List<List<Pair<int, int>>>>(N);
			return;
		}

		string text = file.ToString();
		string[] elemets = text.Split('\n');

		N = Convert.ToInt32(elemets[0]);
		M = Convert.ToInt32(elemets[1]);
		field = new List<List<List<Pair<int, int>>>>(N);
		for (int i = 0; i < N; i++)
			field[i] = new List<List<Pair<int, int>>>(M);

		for (int i = 2; i < elemets.Length; i++)
		{
			string[] ids = elemets[i].Split(' ');
			string[] pos = ids[0].Split('x');
			int x = Convert.ToInt32(pos[0]), y = Convert.ToInt32(pos[1]);
			for (int j = 1; j < ids.Length; j++)
			{
				int id = Convert.ToInt32(ids[j]);
				int type = GlobalData.typeById[id];
				if (type == 0)
				{
					field[y][x].Add(new Pair<int, int>(type, entities.Count));
					entities.Add(new Entity(id));
				}
				if (type == 1)
				{
					field[y][x].Add(new Pair<int, int>(type, units.Count));
					entities.Add(new Unit(id));
				}
				if (type == 2)
				{
					field[y][x].Add(new Pair<int, int>(type, items.Count));
					entities.Add(new Item(id));
				}
			}
		}
	}
}

public class MapSegment
{
	public int x, y; //x, y in matrix of segments map
	public MapContent map;
	public MapSegment(string mapName, int x, int y)
	{
		this.x = x;
		this.y = y;
		string path = "Maps/" + mapName + "/" + x.ToString() + "x" + y.ToString();
		map = new MapContent(path);
	}
}