using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FOV
{
	private const double fault1 = 0.5;
	private const double fault2 = 0.5;
	private const double fault3 = 0.3; //approved

	private Dictionary<KeyValuePair<int, int>, bool> view = new Dictionary<KeyValuePair<int, int>, bool>();
	int N = 100;
	int M = 100;
	private Dictionary<KeyValuePair<int, int>, bool> used = new Dictionary<KeyValuePair<int, int>, bool>();
	int curX;
	int curY;
	int range;
	int inside;

	private KeyValuePair<int, int>[] dir = { new KeyValuePair<int, int>(0, -1), new KeyValuePair<int, int>(1, -1), new KeyValuePair<int, int>(1, 0), new KeyValuePair<int, int>(1, 1), new KeyValuePair<int, int>(0, 1), new KeyValuePair<int, int>(-1, 1), new KeyValuePair<int, int>(-1, 0), new KeyValuePair<int, int>(-1, -1) };

	private double dist(int x1, int y1, int x2, int y2)
	{
		double dst = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		return dst;
	}
	private bool onField(int x, int y)
	{
		if (0 <= y && y < N && 0 <= x && x < M)
			return true;
		return false;
	}
	private void buildLine(ref int a, ref int b, ref int c, int x1, int y1, int x2, int y2)
	{
		a = y1 - y2;
		b = x2 - x1;
		c = x1 * y2 - x2 * y1;
	}
	private double distToLine(int a, int b, int c, int x, int y)
	{
		int numerator = Math.Abs(a * x + b * y + c);
		double denominator = Math.Sqrt(a * a + b * b);
		double dist = numerator / denominator;
		return dist;
	}
	//private bool getOpacityInCell(int x, int y)
	//{
	//	for (int i = 0; i < GlobalData.field[y][x].Count; i++)
	//	{
	//		int curType = GlobalData.field[y][x][i].x;
	//		int curId = GlobalData.field[y][x][i].y;
	//		if (curType == 0 && GlobalData.entities[curId].opacity)
	//			return true;
	//		if (curType == 1 && GlobalData.units[curId].opacity)
	//			return true;
	//		if (curType == 2 && GlobalData.items[curId].opacity)
	//			return true;
	//	}
	//	return false;
	//}

	public void visibleCell(int x, int y)
	{
		int inside = this.inside;

		int shiftX = x - curX;
		int shiftY = y - curY;
		int signX = shiftX < 0 ? -1 : 1;
		int signY = shiftY < 0 ? -1 : 1;
		int trueX = Math.Abs(shiftX);
		int trueY = Math.Abs(shiftY);

		int a = 0, b = 0, c = 0;
		buildLine(ref a, ref b, ref c, 0, 0, trueX, trueY);

		int tmpY = 0;
		int prevX = 0, prevY = 0;
		for (int nowX = 0; nowX <= trueX; nowX++)
		{
			while (tmpY <= trueY && distToLine(a, b, c, nowX, tmpY) > fault1)
				tmpY++;
			for (int nowY = tmpY; nowY <= trueY && distToLine(a, b, c, nowX, nowY) < fault2; nowY++)
			{
				//transparency
				
				if (GenerateMap.mapa[curY + nowY * signY][curX + nowX * signX])
				{
					inside--;
					if (inside == 0)
					{
						view[new KeyValuePair<int, int>(curX + nowX * signX, curY + nowY * signY)] = true;
						return;
					}
				}
				if (nowX - prevX == 1 && nowY - prevY == 1)
				{
					if (GenerateMap.mapa[curY + prevY * signY][curX + (prevX + 1) * signX] &&
						GenerateMap.mapa[curY + (prevY + 1) * signY][curX + prevX * signX])
					{
						inside--;
						if (inside == 0)
							return;
					}
				}
				prevX = nowX;
				prevY = nowY;
			}
		}
		view[new KeyValuePair<int, int>(x, y)] = true;
	}

	private void dfs(int x, int y)
	{
		visibleCell(x, y);
		used[new KeyValuePair<int, int>(x, y)] = true;
		for (int i = 0; i < dir.Length; i++)
		{
			int newX = x + dir[i].Key;
			int newY = y + dir[i].Value;
			if (onField(newX, newY) && !used.ContainsKey(new KeyValuePair<int, int>(newX, newY)) && dist(curX, curY, newX, newY) - range < fault3)
				dfs(newX, newY);
		}
	}

	public void updateView(int x, int y, int range, int inside)
	{
		view.Clear();
		used.Clear();
		curX = x;
		curY = y;
		this.range = range;
		this.inside = inside;
		for (int i = 0; i < 30; i++)
		{
			for (int j = 0; j < 30; j++)
			{
				view[new KeyValuePair<int, int>(i, j)] = true;
			}
		}
		//dfs(curX, curY);
	}

	Texture2D green;
	public void showVisionCell(int x, int y)
	{
		GameObject g = new GameObject("Range");
		g.tag = "Range";
		g.AddComponent<SpriteRenderer>().sprite = Sprite.Create(green, new Rect(0, 0, green.width, green.height), new Vector2(0.5f, 0.5f), 1);
		g.transform.position = new Vector2(x * 48, y * 48);
		g.GetComponent<SpriteRenderer>().sortingOrder = 5;
	}
	public Dictionary<KeyValuePair<int, int>, bool> getView()
	{
		//green = Resources.Load("2") as Texture2D;
		//foreach (KeyValuePair<KeyValuePair<int, int>, bool> ent in view)
		//{
		//	if (ent.Value)
		//		showVisionCell(ent.Key.Key, ent.Key.Value);
		//}
		return view;
	}
}