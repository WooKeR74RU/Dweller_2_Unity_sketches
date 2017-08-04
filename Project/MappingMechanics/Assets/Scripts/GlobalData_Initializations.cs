using System;
using UnityEngine;

/*
 * Initializations and some resources.
 */

public static partial class GlobalData
{
	public static void initializationCamera()
	{
		resolution = new Pair<int, int>(Screen.width, Screen.height);
		viewportM = viewportN * resolution.first / resolution.second;
		if (viewportM % 2 == 0)
			viewportM++;

		cameraObj = new GameObject("Camera");
		camera = cameraObj.AddComponent<Camera>();
		UnityEngine.Object.DontDestroyOnLoad(cameraObj);

		camera.clearFlags = CameraClearFlags.SolidColor;
		camera.backgroundColor = Color.white;
		camera.orthographic = true;
		camera.orthographicSize = maxOrthographicSize;
		camera.nearClipPlane = 0;
		camera.farClipPlane = 1;
	}

	public static void initializationObjectEventTrigger()
	{
		objectEventTriggerObj = new GameObject("ObjectEventTrigger");
		objectEventTrigger = objectEventTriggerObj.AddComponent<ObjectEventTrigger>();
		UnityEngine.Object.DontDestroyOnLoad(objectEventTriggerObj);

		objectEventTriggerObj.SetActive(false);
	}

	public static void initializationControl()
	{
		controlObj = new GameObject("Control");
		control = controlObj.AddComponent<Control>();
		UnityEngine.Object.DontDestroyOnLoad(controlObj);

		Control.arrowObject = new GameObject("Arrow");
		Control.arrowEnable = Resources.Load("Textures/UI/arrow_enable") as Texture2D;
		Control.arrowDisable = Resources.Load("Textures/UI/arrow_disable") as Texture2D;
		Control.arrowObject.AddComponent<SpriteRenderer>().sortingOrder = 2;
		UnityEngine.Object.DontDestroyOnLoad(Control.arrowObject);
	}

	public static void initializationPoolGameObjects()
	{
		for (int i = 0; i < POOL_GAME_OBJECTS_COUNT; i++)
		{
			poolGameObjects[i] = new GameObject("Object #" + i);
			poolGameObjects[i].SetActive(false);
			poolGameObjects[i].AddComponent<LinearSpriteAnimation>();
			idFreeGameObjects.Enqueue(i);
			UnityEngine.Object.DontDestroyOnLoad(poolGameObjects[i]);
		}
	}

	public static void loadObjectsList()
	{
		string path = "ObjectsList";
		string text = (Resources.Load(path) as TextAsset).text;
		text = text.Replace("\r", "").Replace(".", "").Replace("-", "").Replace("  ", " ");
		string[] parameters = text.Split(' ', '\n');
		for (int i = 0; i < parameters.Length; i += 3)
		{
			int id = Convert.ToInt32(parameters[i]);
			string objectName = parameters[i + 1];
			string textureName = parameters[i + 2];

			objectNameById[id] = objectName;
			textureNameById[id] = textureName;
			objectIdByName[objectName] = id;
		}
	}
}