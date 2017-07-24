using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class Entity
{
	public int id;
	public int worldX;
	public int worldY;
	public int x, y, z;
	public bool collision, movable, trap;
	public GameObject gameObj;
	public bool opacity;

	public static Entity createEntity(int x, int y, int id)
	{
		int sprSize = GlobalData.SPRITE_SIZE;

		Entity obj = new Entity();
		obj.x = x;
		obj.y = y;
		obj.worldX = x * sprSize;
		obj.worldY = y * sprSize;
		obj.id = id;
		Texture2D tex = GlobalData.getTexureById[id];

		GameObject tmpGameObj = new GameObject("Entity " + id.ToString());

		LinearSpriteAnimation animation = tmpGameObj.AddComponent<LinearSpriteAnimation>();
		animation.delay = 0.5f;
		int frameCount = tex.width / sprSize;
		for (int i = 0; i < frameCount; i++)
			animation.frames.Add(Sprite.Create(tex, new Rect(i * sprSize, 0, sprSize, sprSize), new Vector2(0.5f, 0.5f), 1));
		animation.gameObj = tmpGameObj;

		tmpGameObj.transform.position = new Vector2(obj.worldX, obj.worldY);
		obj.gameObj = tmpGameObj;
		obj.initialization();
		return obj;
	}

	public void initialization()
	{
		try
		{
			//string methodName = "8-800-555-35-35";
			string methodName = "id_" + id.ToString() + "_initialization";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, null);
		}
		catch
		{
			Debug.Log("Initialization " + id.ToString() + " not defined");
		}
	}
	void id_1_initialization()
	{
		collision = false;
		movable = false;
		trap = false;
		opacity = false;
	}
	void id_3_initialization()
	{
		collision = true;
		movable = false;
		trap = false;
		opacity = true;
	}
	void id_7_initialization()
	{
		collision = true;
		movable = true;
		trap = false;
		opacity = false;
	}
}