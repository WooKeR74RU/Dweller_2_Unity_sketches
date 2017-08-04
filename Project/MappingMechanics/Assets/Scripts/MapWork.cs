using System;
using System.Collections.Generic;
using UnityEngine;

public class Adress
{
	public Level levelPointer;
	public int idInField;

	public int worldX, worldY;
	public int segX, segY;
	public int x, y; //z!!!!!
	public int segMatrixX
	{
		get
		{
			return segX - levelPointer.map.centerSegX + 1;
		}
	}
	public int segMatrixY
	{
		get
		{
			return segY - levelPointer.map.centerSegY + 1;
		}
	}

	public Adress(int worldX, int worldY, Level levelPointer)
	{
		this.worldX = worldX;
		this.worldY = worldY;
		segX = worldX / levelPointer.map.mapDesc.segSizeM;
		segY = worldY / levelPointer.map.mapDesc.segSizeN;
		this.levelPointer = levelPointer;
		x = worldX % levelPointer.map.mapDesc.segSizeM;
		y = worldY % levelPointer.map.mapDesc.segSizeN;
	}
}

public class Map
{
	public MapDescription mapDesc;
	public SegmentContent[,] curSegMatrix = new SegmentContent[3, 3];
	public int centerSegX, centerSegY;

	public Level levelPointer;

	public void initialize(string mapName, Level levelPointer)
	{
		this.levelPointer = levelPointer;	

		mapDesc = new MapDescription(mapName);
		loadSegmentMatrix(mapDesc.startSegX, mapDesc.startSegY);

		addObjToMap(mapDesc.worldStartPointX, mapDesc.worldStartPointY, GlobalData.game.player);
	}

	public void loadSegmentMatrix(int startSegX, int startSegY)
	{
		centerSegX = startSegX;
		centerSegY = startSegY;
		for (int segmentX = startSegX - 1; segmentX <= startSegX + 1; segmentX++)
		{
			for (int segmentY = startSegY - 1; segmentY <= startSegY + 1; segmentY++)
			{
				int segMatrixX = segmentX - startSegX + 1;
				int segMatrixY = segmentY - startSegY + 1;
				curSegMatrix[segMatrixY, segMatrixX] = new SegmentContent(segmentX, segmentY, levelPointer);
			}
		}
	}

	public void updateAreaUp()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 3; j++)
				curSegMatrix[i, j] = curSegMatrix[i + 1, j];
		}
		centerSegY++;
		for (int i = 0; i < 3; i++)
			curSegMatrix[2, i] = new SegmentContent(centerSegX + i - 1, centerSegY + 1, levelPointer);
	}
	public void updateAreaRight()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 2; j++)
				curSegMatrix[i, j] = curSegMatrix[i, j + 1];
		}
		centerSegX++;
		for (int i = 0; i < 3; i++)
			curSegMatrix[i, 2] = new SegmentContent(centerSegX + 1, centerSegY + i - 1, levelPointer);
	}
	public void updateAreaDown()
	{
		for (int i = 2; i > 0; i--)
		{
			for (int j = 0; j < 3; j++)
				curSegMatrix[i, j] = curSegMatrix[i - 1, j];
		}
		centerSegY--;
		for (int i = 0; i < 3; i++)
			curSegMatrix[0, i] = new SegmentContent(centerSegX + i - 1, centerSegY - 1, levelPointer);
	}
	public void updateAreaLeft()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 2; j > 0; j--)
				curSegMatrix[i, j] = curSegMatrix[i, j - 1];
		}
		centerSegX--;
		for (int i = 0; i < 3; i++)
			curSegMatrix[i, 0] = new SegmentContent(centerSegX - 1, centerSegY + i - 1, levelPointer);
	}

	public bool onField(int worldX, int worldY)
	{
		if (0 <= worldY && worldY < mapDesc.sizeN && 0 <= worldX && worldX < mapDesc.sizeM)
			return true;
		return false;
	}

	public List<BaseObject> getObjectsListInPoint(int worldX, int worldY)
	{
		if (!onField(worldX, worldY))
			return null;
		Adress adr = new Adress(worldX, worldY, levelPointer);
		return curSegMatrix[adr.segMatrixY, adr.segMatrixX].field[adr.y][adr.x];
	}

	public void addObjToMap(int worldX, int worldY, BaseObject obj)
	{
		Adress adr = new Adress(worldX, worldY, levelPointer);
		obj.adr = adr;
		obj.adr.idInField = curSegMatrix[adr.segMatrixY, adr.segMatrixX].field[adr.y][adr.x].Count;
		curSegMatrix[adr.segMatrixY, adr.segMatrixX].field[adr.y][adr.x].Add(obj);
	}

	public void removeObjFromMap(BaseObject obj)
	{
		List<BaseObject> list = curSegMatrix[obj.adr.segMatrixY, obj.adr.segMatrixX].field[obj.adr.y][obj.adr.x];
		list.RemoveAt(obj.adr.idInField);
		for (int i = obj.adr.idInField; i < list.Count; i++)
			list[i].adr.idInField = i;
	}

	public void moveObj(int toX, int toY, BaseObject obj)
	{
		removeObjFromMap(obj);
		addObjToMap(toX, toY, obj);
		obj.updateGameObject();
	}
}

public class MapDescription
{
	public int segCountN, segCountM; //map size in segments
	public int segSizeN, segSizeM;
	public int startSegX, startSegY;
	public int startPointInSegX, startPointInSegY;

	public string mapName;
	public int sizeN, sizeM; //world size
	public int worldStartPointX, worldStartPointY;

	public MapDescription(string mapName)
	{
		string path = "Maps/" + mapName + "/description";
		this.mapName = mapName;
		string text = (Resources.Load(path) as TextAsset).text;
		text = text.Replace("\r", "");

		string[] parameters = text.Split(' ', '\n');
		for (int i = 0; i < parameters.Length; i += 2)
		{
			if (parameters[i] == "MapMatrixSize")
			{
				string[] pos = parameters[i + 1].Split('x');
				segCountN = Convert.ToInt32(pos[1]);
				segCountM = Convert.ToInt32(pos[0]);
			}
			if (parameters[i] == "SegmentSize")
			{
				string[] pos = parameters[i + 1].Split('x');
				segSizeN = Convert.ToInt32(pos[1]);
				segSizeM = Convert.ToInt32(pos[0]);
			}
			if (parameters[i] == "StartSegment")
			{
				string[] pos = parameters[i + 1].Split('x');
				startSegX = Convert.ToInt32(pos[0]);
				startSegY = Convert.ToInt32(pos[1]);
			}
			if (parameters[i] == "StartPointInSegment")
			{
				string[] pos = parameters[i + 1].Split('x');
				startPointInSegX = Convert.ToInt32(pos[0]);
				startPointInSegY = Convert.ToInt32(pos[1]);
			}
		}

		sizeN = segCountN * segSizeN;
		sizeM = segCountM * segSizeM;

		worldStartPointX = startSegX * segSizeM + startPointInSegX;
		worldStartPointY = startSegY * segSizeN + startPointInSegY;
	}
}

public class SegmentContent
{
	public List<List<List<BaseObject>>> field;

	public SegmentContent(int segmentX, int segmentY, Level levelPointer)
	{
		string path = "Maps/" + levelPointer.map.mapDesc.mapName + "/" + segmentX.ToString() + "x" + segmentY.ToString();
		UnityEngine.Object file = Resources.Load(path);
		if (file == null)
			return;

		string text = file.ToString();
		text = text.Replace("\r", "");
		string[] elemets = text.Split('\n');

		field = new List<List<List<BaseObject>>>();
		for (int i = 0; i < levelPointer.map.mapDesc.segSizeN; i++)
		{
			field.Add(new List<List<BaseObject>>());
			for (int j = 0; j < levelPointer.map.mapDesc.segSizeM; j++)
				field[i].Add(new List<BaseObject>());
		}

		for (int i = 0; i < elemets.Length; i++)
		{
			string[] ids = elemets[i].Split(' ');
			string[] pos = ids[0].Split('x');
			int x = Convert.ToInt32(pos[0]), y = Convert.ToInt32(pos[1]);
			for (int j = 1; j < ids.Length; j++)
			{
				int id = Convert.ToInt32(ids[j]);
				int worldX = segmentX * levelPointer.map.mapDesc.segSizeM + x;
				int worldY = segmentY * levelPointer.map.mapDesc.segSizeN + y;
				Adress adr = new Adress(worldX, worldY, levelPointer);
				BaseObject curObject = BaseObject.createNewObject(id);
				curObject.adr = adr;
				curObject.adr.idInField = field[y][x].Count;
				field[y][x].Add(curObject);
			}
		}

	}

}