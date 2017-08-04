using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class UnitVisibleDistanceCalculated : MonoBehaviour
{
	//mapa[(int)mapY][(int)mapX] true or false. true if object in this cell not available for vision(it's opaque)
	public bool isVisible(int visionDistance, int x, int y, int unitPositionX, int unitPositionY)
	{
		int w = Mathf.Abs(x - unitPositionX);
		int h = Mathf.Abs(y - unitPositionY);
		int distanceBetweenCell = (int)Mathf.Ceil(Mathf.Sqrt((float)(w * w + h * h)));
		if (distanceBetweenCell / 48 <= visionDistance)
		{
			return isEnable(x, y, unitPositionX, unitPositionY);
		}
		return false;
	}
	bool isEnable(float x, float y, float unitPositionX, float unitPositionY)
	{
		if (x < 0 || y < 0)
			return false;
		int mapX = (int)x / 48, mapY = (int)y / 48;
		if (mapY < GenerateMap.mapa.Count && mapX < GenerateMap.mapa[mapY].Count &&
		GenerateMap.mapa[mapY][mapX] == true)
			return false;
		
		if (x == unitPositionX)
		{

			while (unitPositionY != y)
			{
				if (unitPositionY < y)
				{
					if (GenerateMap.mapa[(int)unitPositionY / 48][(int)x / 48])
						return false;
					unitPositionY += 48;

				}
				else
				{
					if (GenerateMap.mapa[(int)y / 48][(int)x / 48])
						return false;
					y += 48;

				}
			}
			return true;
		}
		if (y == unitPositionY)
		{

			while (unitPositionX != x)
			{
				if (unitPositionX < x)
				{
					if (GenerateMap.mapa[(int)y / 48][(int)unitPositionX / 48])
						return false;
					unitPositionX += 48;

				}
				else
				{
					if (GenerateMap.mapa[(int)y / 48][(int)x / 48])
						return false;
					x += 48;
				}
			}
			return true;
		}
		int step;
		if (unitPositionX < x)
			step = 4;
		else
			step = -4;
		int coefficient;
		if (y < unitPositionY)
			coefficient = -1;
		else
			coefficient = 1;
		
		int stepCount = 0;
		float tg = Math.Abs(y - unitPositionY) / Math.Abs(x - unitPositionX);
		while (unitPositionX + stepCount != x)
		{
			stepCount += step;
			double mapY = coefficient * Math.Abs(tg * stepCount);
			mapY += unitPositionY;
			mapY = mapY / 48;
			double mapX = (unitPositionX + stepCount) / 48;
			// Round 0.5 to lower
			if (mapY - (int)mapY == 0.5)
				continue;
			else
				mapY = Math.Round(mapY);
			if (mapX - (int)mapX == 0.5)
				continue;
			else
				mapX = Math.Round(mapX);
			//mapa[(int)mapY][(int)mapX] true or false. true if object in this cell not available for vision(it's opaque)
			if (GenerateMap.mapa[(int)mapY][(int)mapX])
				return false;

		}
		return true;
	}
}
