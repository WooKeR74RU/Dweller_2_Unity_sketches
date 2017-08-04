using System;
using System.Collections.Generic;

public class FOV
{
	private Unit unitPointer;
	public Dictionary<Pair<int, int>, bool> view = new Dictionary<Pair<int, int>, bool>(); //0 - visible, 1 - confirmed
	private HashSet<Pair<int, int>> used = new HashSet<Pair<int, int>>();

	public FOV(Unit unitPointer)
	{
		this.unitPointer = unitPointer;
	}

	private const double fault1 = 0.5;
	private const double fault2 = 0.5;
	private const double fault3 = 0.3; //approved

	private double dist(int x1, int y1, int x2, int y2)
	{
		double dst = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		return dst;
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

	private bool getOpacityCell(int worldX, int worldY)
	{
		List<BaseObject> list;
		list = unitPointer.adr.levelPointer.map.getObjectsListInPoint(worldX, worldY);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].opacity)
				return true;
		}
		return false;
	}

	private void visibleCell(int worldX, int worldY)
	{
		int inside = unitPointer.obstaclePassCount;

		int shiftX = worldX - unitPointer.adr.worldX;
		int shiftY = worldY - unitPointer.adr.worldY;
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
				if (getOpacityCell(unitPointer.adr.worldX + nowX * signX, unitPointer.adr.worldY + nowY * signY))
				{
					inside--;
					if (inside == 0)
					{
						if (unitPointer.adr.worldX + nowX * signX == worldX && unitPointer.adr.worldY + nowY * signY == worldY)
							view[new Pair<int, int>(worldX, worldY)] = false;
						return;
					}
				}
				if (nowX - prevX == 1 && nowY - prevY == 1)
				{
					if (getOpacityCell(unitPointer.adr.worldX + (prevX + 1) * signX, unitPointer.adr.worldY + prevY * signY) && getOpacityCell(unitPointer.adr.worldX + prevX * signX, unitPointer.adr.worldY + (prevY + 1) * signY))
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

		view[new Pair<int, int>(worldX, worldY)] = false;
	}

	private void postprocessing(int worldX, int worldY)
	{
		if (!view.ContainsKey(new Pair<int, int>(worldX, worldY)) || view[new Pair<int, int>(worldX, worldY)])
			return;
		view[new Pair<int, int>(worldX, worldY)] = true;
		for (int i = 0; i < Unit.dir4.Length; i++)
		{
			int toX = worldX + Unit.dir4[i].first;
			int toY = worldY + Unit.dir4[i].second;
			if (unitPointer.adr.levelPointer.map.onField(toX, toY))
				postprocessing(toX, toY);
		}
	}

	private void dfs(int worldX, int worldY)
	{
		visibleCell(worldX, worldY);
		used.Add(new Pair<int, int>(worldX, worldY));
		for (int i = 0; i < Unit.dir4.Length; i++)
		{
			int toX = worldX + Unit.dir4[i].first;
			int toY = worldY + Unit.dir4[i].second;
			if (unitPointer.adr.levelPointer.map.onField(toX, toY) && !used.Contains(new Pair<int, int>(toX, toY)) && dist(unitPointer.adr.worldX, unitPointer.adr.worldY, toX, toY) - unitPointer.range < fault3)
				dfs(toX, toY);
		}
	}

	public void updateView()
	{
		view.Clear();
		used.Clear();
		dfs(unitPointer.adr.worldX, unitPointer.adr.worldY);
		postprocessing(unitPointer.adr.worldX, unitPointer.adr.worldY);
	}

}