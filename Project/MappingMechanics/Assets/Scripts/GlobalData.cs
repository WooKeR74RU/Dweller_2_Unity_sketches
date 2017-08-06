using System;
using System.Collections.Generic;
using UnityEngine;

public static partial class GlobalData
{
	public const int SPRITE_SIZE = 24;

	public const int ENTITIES_ID_FROM = 1;
	public const int ENTITIES_ID_TO = 1000;
	public const int UNITS_ID_FROM = 1001;
	public const int UNITS_ID_TO = 2000;
	public const int ITEMS_ID_FROM = 2001;
	public const int ITEMS_ID_TO = 3000;

	public static Dictionary<int, string> textureNameById = new Dictionary<int, string>();

	public static Dictionary<string, Texture2D> maskTextureByName = new Dictionary<string, Texture2D>();

	public static Dictionary<int, string> objectNameById = new Dictionary<int, string>();
	public static Dictionary<string, int> objectIdByName = new Dictionary<string, int>();
	public static Dictionary<int, Texture2D> objectTextureById = new Dictionary<int, Texture2D>();

	public const float minOrthographicSize = 108, maxOrthographicSize = 180;
	public static Pair<int, int> resolution;
	public const int viewportN = 15;
	public static int viewportM;
	public static GameObject cameraObj;
	public static Camera camera;

	public static GameObject controlObj;
	public static Control control;

	public static GameObject objectEventTriggerObj;
	public static ObjectEventTrigger objectEventTrigger;

	public static Game game;

	public const int POOL_GAME_OBJECTS_COUNT = 5000;
	public static GameObject[] poolGameObjects = new GameObject[POOL_GAME_OBJECTS_COUNT];
	public static Queue<int> idFreeGameObjects = new Queue<int>();

	public static void initialization()
	{
		loadObjectsList();
		initializationCamera();
		initializationObjectEventTrigger();
		initializationControl();
		initializationPoolGameObjects();
	}

	public static int freeGameObjectFromPool()
	{
		int curId = idFreeGameObjects.Dequeue();
		poolGameObjects[curId].SetActive(true);
		return curId;
	}
	public static void freeGameObjectToPool(ref int gameObjectId)
	{
		poolGameObjects[gameObjectId].SetActive(false);
		poolGameObjects[gameObjectId].GetComponent<LinearSpriteAnimation>().clear();
		idFreeGameObjects.Enqueue(gameObjectId);
		gameObjectId = -1;
	}

	public static int getObjectTypeById(int id)
	{
		if (ENTITIES_ID_FROM <= id && id <= ENTITIES_ID_TO)
			return 0;
		if (UNITS_ID_FROM <= id && id <= UNITS_ID_TO)
			return 1;
		if (ITEMS_ID_FROM <= id && id <= ITEMS_ID_TO)
			return 2;
		throw new Exception("Undefined type");
	}

	public static string getObjectNameById(int id)
	{
		return objectNameById[id];
	}

	public static int getObjectIdByName(string name)
	{
		return objectIdByName[name];
	}

	public static string getTextureNameById(int id)
	{
		return textureNameById[id];
	}

	public static Texture2D getObjectTextureById(int id)
	{
		if (!objectTextureById.ContainsKey(id))
		{
			//нахер это говно(нет) (это важно, НЕ удалять)
			int type = getObjectTypeById(id);
			if (type == 0)
			{
				string path = "Textures/Entities/" + textureNameById[id];
				UnityEngine.Object file = Resources.Load(path);
				objectTextureById.Add(id, file as Texture2D);
			}
			if (type == 1)
			{
				string path = "Textures/Units/" + textureNameById[id];
				UnityEngine.Object file = Resources.Load(path);
				objectTextureById.Add(id, file as Texture2D);
			}
			if (type == 2)
			{
				string path = "Textures/Items/" + textureNameById[id];
				UnityEngine.Object file = Resources.Load(path);
				objectTextureById.Add(id, file as Texture2D);
			}
		}
		return objectTextureById[id];
	}

	public static Texture2D getMaskTextureByName(string name)
	{
		if (!maskTextureByName.ContainsKey(name))
		{
			string path = "Textures/Masks/" + name;
			UnityEngine.Object file = Resources.Load(path);
			maskTextureByName.Add(name, file as Texture2D);
		}
		return maskTextureByName[name];
	}
}