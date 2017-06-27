using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public TextAsset textFile;


	void Start()
	{
		mapLoad();
	}
	private void mapLoad()
	{
		string text = textFile.text;
		int x = 0, y = 0, Mapx = 0, Mapy = 0;
		string[] substrings = text.Split(new char[] { '#', '\r', '\n' });
		GlobalData.map.Add(new List<List<KeyValuePair<int, int>>>());
		for (int i = 0; i < substrings.Length; i++)
		{
			if (substrings[i].Length == 0)
			{
				x = 0;
				y -= GlobalData.SPRITE_SIZE;
				Mapy++;
				Mapx = 0;
				GlobalData.map.Add(new List<List<KeyValuePair<int, int>>>());
				continue;
			}
			string[] c = substrings[i].Split(new char[] { ',', '{', '}' });
			GlobalData.map[Mapy].Add(new List<KeyValuePair<int, int>>());
			for (int j = 0; j < c.Length; j++)
			{
				if (c[j] == "")
					continue;
				int id = Convert.ToInt32(c[j]);
				switch (GlobalData.typeById[id])
				{
					case 0:
						GlobalData.addToMapNewObject(Entity.createItem(Mapx, Mapy, id, j), Mapx, Mapy);
						break;
					case 1:
						GlobalData.addToMapNewObject(Unit.createUnit(Mapx, Mapy, id, j), Mapx, Mapy);
						break;
					case 2:
						GlobalData.addToMapNewObject(Item.createItem(Mapx, Mapy, id, j), Mapx, Mapy);
						break;
				}
			}

			x += GlobalData.SPRITE_SIZE;
			Mapx++;
		}
	}
}