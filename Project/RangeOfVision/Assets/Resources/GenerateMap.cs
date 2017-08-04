using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateMap : MonoBehaviour
{

	public static List<List<bool>> mapa = new List<List<bool>>();
	void Start()
	{
		Load("sketch_map");
	}
	
	public void Load(string levelMapName)
	{
		string levelMapPath = "LevelsMaps/" + levelMapName;
		string mapString = Resources.Load(levelMapPath).ToString();
		for (int i = 0; i < 100; i++)
		{
			mapa.Add(new List<bool>());
		for (int j = 0; j < 100; j++)
		{
				mapa[i].Add(false);
		}
		}
		string[] cells = mapString.Split(new char[] { '#' });
		int maxX = -1, maxY = -1;
		for (int i = 0; i < cells.Length; i++)
		{
			if (cells[i].Length == 0)
			{
				continue;
			}
			string[] cellsId = cells[i].Substring(1, cells[i].Length - 2).Split(new char[] { ',', '{', '}' });

			int currentX = Convert.ToInt32(cellsId[1]), currentY = Convert.ToInt32(cellsId[0]);
			maxX = Math.Max(currentX, maxX);
			maxY = Math.Max(currentY, maxY);
			for (int j = 2; j < cellsId.Length; j++)
			{
				int id = Convert.ToInt32(cellsId[j]);
				GameObject g = new GameObject();
				Texture2D t = Resources.Load(id.ToString()) as Texture2D;
				g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0, 0),1);
				g.GetComponent<SpriteRenderer>().sortingOrder = j;
				g.transform.position = new Vector2(currentX*48, currentY*48);
				if(id == 3)
				{
					mapa[currentY][currentX] = true;
				}
			}
		}

	}
}
