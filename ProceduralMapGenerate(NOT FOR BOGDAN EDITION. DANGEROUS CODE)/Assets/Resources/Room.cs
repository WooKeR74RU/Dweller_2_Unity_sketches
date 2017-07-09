using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Room : MonoBehaviour {

	public List<KeyValuePair<int, int>> map = new List<KeyValuePair<int, int>>();
	public List<string> texture = new List<string>();
	public int width = 0, height =0 , id;

	public Room(int id_)
	{
		id = id_;
		parse(id_.ToString());
	}
	public void parse(string levelMapName)
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
			map.Add(new KeyValuePair<int, int>(currentX, currentY));
			texture.Add(cellsId[2]);
		}
		width++;
		height++;
	}
}
