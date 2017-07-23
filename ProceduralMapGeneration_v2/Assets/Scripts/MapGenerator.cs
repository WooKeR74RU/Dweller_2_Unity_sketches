using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	private const int N = 150;
	private const int M = 250;

	private const int ROOM_TYPES_COUNT = 5;
	private const int ROOM_COUNT = 25;

	private System.Random random = new System.Random();

	private List<Room> rooms = new List<Room>();
	private List<List<List<int>>> field = new List<List<List<int>>>(N);
	private void Start()
	{
		for (int i = 0; i < N; i++)
			field[i] = new List<List<int>>(M);
		for (int i = 0; i < ROOM_COUNT; i++)
			rooms.Add(new Room(i));
	}

	private void buildMap()
	{
		List<int> roomsId = new List<int>();
		for (int i = 0; i < ROOM_COUNT; i++)
			roomsId.Add(random.Next(ROOM_TYPES_COUNT));

		List<Pair<int, int>> enablePoints = new List<Pair<int, int>>();
		enablePoints.Add(new Pair<int, int>(0, 0));

		for (int i = 0; i < roomsId.Count; i++)
		{
			
			int x = 
		
			
		}


	}


	private void displayMap()
	{
		Texture2D textureWall, textureFloor;
		string pathWall = "Textures/Entities/3";
		string pathFloor = "Textures/Entities/1";
		textureWall = Resources.Load(pathWall) as Texture2D;
		textureFloor = Resources.Load(pathFloor) as Texture2D;

		GameObject wall = new GameObject(), floor = new GameObject();
		wall.AddComponent<SpriteRenderer>().sprite = Sprite.Create(textureWall, new Rect(0, 0, textureWall.width, textureWall.height), new Vector2(0.5f, 0.5f), 1);
		floor.AddComponent<SpriteRenderer>().sprite = Sprite.Create(textureFloor, new Rect(0, 0, textureFloor.width, textureFloor.height), new Vector2(0.5f, 0.5f), 1);

		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < M; j++)
			{
				for (int k = 0; k < field[i][j].Count; k++)
				{
					if (field[i][j][k] == 3)
					{
						GameObject gO = Instantiate(wall);
						gO.transform.position = new Vector2(j * 48, i * 48);
					}
					if (field[i][j][k] == 1)
					{
						GameObject gO = Instantiate(floor);
						gO.transform.position = new Vector2(j * 48, i * 48);
					}
				}
			}
		}

	}

}

public class Room
{
	public List<Pair<Pair<int, int>, int>> objectsList = new List<Pair<Pair<int, int>, int>>();
	public HashSet<Pair<int, int>> destructible = new HashSet<Pair<int, int>>();
	public int id;
	public int x, y;
	public int width, height;

	public Room(int id)
	{
		string levelMapPath = "RoomPatterns/" + id.ToString();
		string file = Resources.Load(levelMapPath).ToString();
		string[] elemets = file.Split('\n');
		bool mode = false;
		for (int i = 0; i < elemets.Length; i++)
		{
			if (elemets[i] == "#")
			{
				mode = true;
				continue;
			}
			if (!mode)
			{
				string[] ids = elemets[i].Split(' ');
				string[] pos = ids[0].Split('x');
				int x = Convert.ToInt32(pos[0]), y = Convert.ToInt32(pos[1]);
				for (int j = 1; j < ids.Length; j++)
					objectsList.Add(new Pair<Pair<int, int>, int>(new Pair<int, int>(x, y), Convert.ToInt32(ids[j])));
			}
			else
			{
				string[] pos = elemets[i].Split('x');
				int x = Convert.ToInt32(pos[0]), y = Convert.ToInt32(pos[1]);
				destructible.Add(new Pair<int, int>(x, y));
			}
		}
	}

	public Rect toRect()
	{
		return new Rect(x, y, width, height);
	}

}