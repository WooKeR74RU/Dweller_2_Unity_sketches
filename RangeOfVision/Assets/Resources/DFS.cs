using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class DFS : MonoBehaviour
{

	Texture2D green, red;
	public Dictionary<KeyValuePair<int, int>, int> used = new Dictionary<KeyValuePair<int, int>, int>();
	int visionDistance;
	int operationCount = 0;
	public void startCalculateRangeOfVision(int distance, int unitPositionX, int unitPositionY)
	{
		operationCount = 0;
		green = Resources.Load("2") as Texture2D;
		red = Resources.Load("0") as Texture2D;
		visionDistance = distance;
		used = new Dictionary<KeyValuePair<int, int>, int>();
		dfs(unitPositionX, unitPositionY, unitPositionX, unitPositionY);

	}
	public void dfs(int x, int y, int unitPositionX, int unitPositionY)
	{
		operationCount++;
		if (used.ContainsKey(new KeyValuePair<int, int>(x, y)))
			return;

		used[new KeyValuePair<int, int>(x, y)] = 1;
		int w = Mathf.Abs(x - unitPositionX);
		int h = Mathf.Abs(y - unitPositionY);
		int distanceBetweenCell = (int)Mathf.Ceil(Mathf.Sqrt((float)(w * w + h * h)));
		if (distanceBetweenCell / 48 <= visionDistance)
		{
			if (isEnable(x, y, unitPositionX, unitPositionY))
			{
				//This x,y cell is visible
				showVisionCell(x, y);
			}
			if (x < 0 || y < 0)
				return;
			if ((int)y / 48 < GenerateMap.mapa.Count && (int)(x + 48) / 48 < GenerateMap.mapa[(int)y / 48].Count &&
		GenerateMap.mapa[(int)y / 48][(int)(x + 48) / 48] == false)
				dfs(x + 48, y, unitPositionX, unitPositionY);
			if (x - 48 >= 0 && (int)y / 48 < GenerateMap.mapa.Count && (int)(x - 48) / 48 < GenerateMap.mapa[(int)y / 48].Count &&
GenerateMap.mapa[(int)y / 48][(int)(x - 48) / 48] == false)
				dfs(x - 48, y, unitPositionX, unitPositionY);
			if (y - 48 >= 0 && (int)(y - 48) / 48 < GenerateMap.mapa.Count && (int)(x) / 48 < GenerateMap.mapa[(int)(y - 48) / 48].Count &&
GenerateMap.mapa[(int)(y - 48) / 48][(int)(x) / 48] == false)
				dfs(x, y - 48, unitPositionX, unitPositionY);
			if ((int)(y + 48) / 48 < GenerateMap.mapa.Count && (int)(x) / 48 < GenerateMap.mapa[(int)(y + 48) / 48].Count &&
GenerateMap.mapa[(int)(y + 48) / 48][(int)(x) / 48] == false)
				dfs(x, y + 48, unitPositionX, unitPositionY);
		}
		return;
	}
	public void showVisionCell(int x, int y)
	{
		//return;
		GameObject g = new GameObject("Range");
		g.tag = "Range";
		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(green, new Rect(0, 0, green.width, green.height), new Vector2(0.5f, 0.5f), 1);
		g.transform.position = new Vector2(x, y);
		g.GetComponent<SpriteRenderer>().sortingOrder = 5;
	}
	public void showVisionLine(int x, int y)
	{
		//return;
		GameObject g = new GameObject("Range");
		g.tag = "Range";
		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(red, new Rect(0, 0, red.width, red.height), new Vector2(0.5f, 0.5f), 1);
		g.transform.position = new Vector2(x, y);
		g.GetComponent<SpriteRenderer>().sortingOrder = 6;
	}

	public bool isVisible(int x, int y, int unitPositionX, int unitPositionY)
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
		if ((int)y / 48 < GenerateMap.mapa.Count && (int)x / 48 < GenerateMap.mapa[(int)y / 48].Count &&
		GenerateMap.mapa[(int)y / 48][(int)x / 48] == true)
			return false;
		if (x == unitPositionX)
		{

			while (unitPositionY != y)
			{
				if (unitPositionY < y)
				{
					showVisionLine((int)x, (int)unitPositionY);
					if (GenerateMap.mapa[(int)unitPositionY / 48][(int)x / 48])
						return false;
					unitPositionY += 48;

				}
				else
				{
					showVisionLine((int)unitPositionX, (int)unitPositionY);
					if (GenerateMap.mapa[(int)y / 48][(int)x / 48])
						return false;
					y += 48;

				}
				operationCount++;

			}
			return true;
		}
		if (y == unitPositionY)
		{

			while (unitPositionX != x)
			{
				if (unitPositionX < x)
				{
					showVisionLine((int)unitPositionX, (int)y);
					if (GenerateMap.mapa[(int)y / 48][(int)unitPositionX / 48])
						return false;
					unitPositionX += 48;

				}
				else
				{
					showVisionLine((int)x, (int)y);

					if (GenerateMap.mapa[(int)y / 48][(int)x / 48])
						return false;
					x += 48;
				}
				operationCount++;
			}
			return true;
		}
		bool f = true;
		int step;
		if (unitPositionX < x)
			step = 22;
		else
			step = -22;
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
			showVisionLine((int)unitPositionX + stepCount, (int)mapY);
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
			operationCount++;
			if (GenerateMap.mapa[(int)mapY][(int)mapX])
				f = false;

		}
		return f;
	}
}
