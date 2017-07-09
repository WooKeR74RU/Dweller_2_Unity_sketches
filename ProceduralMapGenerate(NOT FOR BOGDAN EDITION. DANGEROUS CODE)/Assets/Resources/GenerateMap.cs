using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateMap : MonoBehaviour
{


	
	public void mapLoad(string levelMapName,int posx,int posy)
	{
		string levelMapPath = "maps/" + levelMapName;
		string mapString = Resources.Load(levelMapPath).ToString();

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
				
				//StartWork.mapa[currentY + posy][currentX + posx] = id;
			}
		}
		
	}
	
}
