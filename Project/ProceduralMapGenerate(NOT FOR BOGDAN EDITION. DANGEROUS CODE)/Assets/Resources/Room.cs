using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Room
{

	public List<ItemDescription> map = new List<ItemDescription>();
	public List<KeyValuePair<int, int>> entires = new List<KeyValuePair<int, int>>();
	public int[] usedEntires;
	public int width = 0, height = 0;
	int idRoom;
	public Room(int idRoom)
	{
		this.idRoom = idRoom;
		CreateRoom(idRoom.ToString());
	}
	public void CreateRoom(string levelMapName)
	{
		string levelMapPath = "maps/" + levelMapName;
		string mapString = Resources.Load(levelMapPath).ToString();

		string[] cells = mapString.Split(new char[] { '#' });
		for (int i = 0; i < cells.Length; i++)
		{
			if (cells[i].Length == 0)
			{
				continue;
			}
			string[] cellsId = cells[i].Substring(1, cells[i].Length - 2).Split(new char[] { ',', '{', '}' });

			int currentX = Convert.ToInt32(cellsId[1]), currentY = Convert.ToInt32(cellsId[0]);
			width = Math.Max(currentX, width);
			height = Math.Max(currentY, height);
			for(int j=2;j<cellsId.Length;j++)
			{
				map.Add(new ItemDescription(Convert.ToInt32(cellsId[j]),currentX, currentY));
			}
		
			
		}
		
		width++;
		height++;
		int[,] mm = new int[height, width];
		for(int i=0;i<map.Count;i++)
		{
			mm[map[i].y,map[i].x] = map[i].id;
		}
		for (int i = 0; i < height; i++)
			for (int j = 0; j < width;j++)
			{
				try
				{
					if (mm[i, j] == 1 && (mm[i + 1, j] == 0 || mm[i - 1, j] == 0 || mm[i, j + 1] == 0 || mm[i, j - 1] == 0))
					{
						entires.Add(new KeyValuePair<int, int>(j, i));
					}
				}
				catch{
					if (mm[i, j] == 1)
						entires.Add(new KeyValuePair<int, int>(j, i));
				}
			}
		usedEntires = new int[entires.Count];

	}
}
