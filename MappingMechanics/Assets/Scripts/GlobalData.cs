using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
	public const int SPRITE_SIZE = 48;

	public const int ENTITIES_ID_FROM = 1;
	public const int ENTITIES_ID_TO = 1000;
	public const int UNITS_ID_FROM = 1001;
	public const int UNITS_ID_TO = 2000;
	public const int ITEMS_ID_FROM = 2001;
	public const int ITEMS_ID_TO = 3000;

	public static Dictionary<int, Texture2D> getTexureById = new Dictionary<int, Texture2D>();
	public static Dictionary<int, int> typeById = new Dictionary<int, int>();

	public MapDescription mapDesc;
	public MapSegment[,] curMapSegmentsMatrix = new MapSegment[3, 3];
	int curSegmentX, curSegmentY;

	private void Start()
	{
		//нахер это говно(нет) (это важно, НЕ удалять)
		for (int i = ENTITIES_ID_FROM; i <= ENTITIES_ID_TO; i++)
		{
			string path = "Textures/EntitiesTextures/" + i.ToString();
			UnityEngine.Object file = Resources.Load(path);
			if (file == null)
				continue;
			getTexureById.Add(i, file as Texture2D);
			typeById.Add(i, 0);
		}
		for (int i = UNITS_ID_FROM; i <= UNITS_ID_TO; i++)
		{
			string path = "Textures/UnitsTextures/" + i.ToString();
			UnityEngine.Object file = Resources.Load(path);
			if (file == null)
				continue;
			getTexureById.Add(i, file as Texture2D);
			typeById.Add(i, 1);
		}
		for (int i = ITEMS_ID_FROM; i <= ITEMS_ID_TO; i++)
		{
			string path = "Textures/ItemsTextures/" + i.ToString();
			UnityEngine.Object file = Resources.Load(path);
			if (file == null)
				continue;
			getTexureById.Add(i, file as Texture2D);
			typeById.Add(i, 2);
		}
	}

	void loadMap(string mapName)
	{
		mapDesc = new MapDescription(mapName);
		initializeMap(mapDesc.startSegment);
	}

	void initializeMap(Pair<int, int> strt)
	{
		for (int i = strt.first - 1; i <= strt.first + 1; i++)
		{
			for (int j = strt.second - 1; j <= strt.second + 1; j++)
			{
				int x = i - strt.first + 1;
				int y = j - strt.second + 1;
				curMapSegmentsMatrix[y, x] = new MapSegment(mapDesc.mapName, i, j);
			}
		}
	}

	void updateArea(int dir)
	{
		if (dir == 0)
		{
			for (int i = 2; i > 0; i--)
			{
				for (int j = 0; j < 3; j++)
					curMapSegmentsMatrix[i, j] = curMapSegmentsMatrix[i - 1, j];
			}
			for (int i = 0; i < 3; i++)
				curMapSegmentsMatrix[0, i] = new MapSegment(mapDesc.mapName, curSegmentX + i - 1, curSegmentY + 1);
		}
		if (dir == 1)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 2; j++)
					curMapSegmentsMatrix[i, j] = curMapSegmentsMatrix[i, j + 1];
			}
			for (int i = 0; i < 3; i++)
				curMapSegmentsMatrix[i, 2] = new MapSegment(mapDesc.mapName, curSegmentX + 1, curSegmentY + i - 1);
		}
		if (dir == 2)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 3; j++)
					curMapSegmentsMatrix[i, j] = curMapSegmentsMatrix[i + 1, j];
			}
			for (int i = 0; i < 3; i++)
				curMapSegmentsMatrix[2, i] = new MapSegment(mapDesc.mapName, curSegmentX + i - 1, curSegmentY - 1);
		}
		if (dir == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 2; j > 0; j--)
					curMapSegmentsMatrix[i, j] = curMapSegmentsMatrix[i, j - 1];
			}
			for (int i = 0; i < 3; i++)
				curMapSegmentsMatrix[i, 0] = new MapSegment(mapDesc.mapName, curSegmentX - 1, curSegmentY + i - 1);
		}
	}
		
}