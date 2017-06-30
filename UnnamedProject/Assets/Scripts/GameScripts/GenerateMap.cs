using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateMap : MonoBehaviour
{


	void Start()
	{
		mapLoad("sketch_map");
	}
	
	public void mapLoad(string levelMapName)
	{
		string levelMapPath = "LevelsMaps/" + levelMapName;
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
				int type = GlobalData.typeById[id];
				if (type == 0)
				{
					GlobalData.addNewObject(Entity.createEntity(currentX, currentY, id));
				}
				if (type == 1)
				{
					GlobalData.addNewObject(Unit.createUnit(currentX, currentY, id));
				}
				if (type == 2)
				{
					GlobalData.addNewObject(Item.createItem(currentX, currentY, id, 1));
				}
			}
		}
		Utilities.FillArray<Pair<int, int>>(maxX+1, maxY+1, 0, ref GlobalData.field, null);

		for (int i = 0; i < GlobalData.entities.Count; i++)
			GlobalData.addToField(0, i, GlobalData.entities[i].x, GlobalData.entities[i].y);
		for (int i = 0; i < GlobalData.units.Count; i++)
			GlobalData.addToField(1, i, GlobalData.units[i].x, GlobalData.units[i].y);
		for (int i = 0; i < GlobalData.items.Count; i++)
			GlobalData.addToField(3, i, GlobalData.items[i].x, GlobalData.items[i].y);
	}
}
