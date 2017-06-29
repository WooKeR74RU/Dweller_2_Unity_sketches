using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

public class Unit
{
	public int id;
	public int worldX;
	public int worldY;
	public int x, y, z;
	public GameObject gameObj;

	public static Unit createUnit(int x, int y, int id)
	{
		int sprSize = GlobalData.SPRITE_SIZE;
		
		Unit obj = new Unit();
		obj.x = x;
		obj.y = y;
		obj.worldX = x * sprSize;
		obj.worldY = y * sprSize;
		obj.id = id;
		Texture2D tex = GlobalData.getTexureById[id];

		GameObject tmpGameObj = new GameObject("Unit " + id.ToString());

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

	Vector2[] possiblePath = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1),
		new Vector2(0, -1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) };

	bool EnablePaths(int fromX, int fromY, Vector2 move_vector)
	{
		fromX += (int)move_vector.x;
		fromY += (int)move_vector.y;
		bool isEnable = true;
		if (fromY >= GlobalData.field.Count || fromX >= GlobalData.field[fromY].Count)
			return false;
		for (int i = 0; i < GlobalData.field[fromY][fromX].Count; i++)
		{
			int type = GlobalData.field[fromY][fromX][i].x;
			if (type == 0)
			{
				Entity curEnity = GlobalData.entities[GlobalData.field[fromY][fromX][i].y];
				if (curEnity.collision)
					isEnable = false;
			}
			if (type == 1)
			{}
			if (type == 2)
			{}
		}
		return isEnable;
	}

	void baseAI()
	{
		while (true)
		{
			int dir = new System.Random().Next(8);
			if (!EnablePaths(x, y, possiblePath[dir]))
				continue;
			move(this, possiblePath[dir]);
			break;
		}
	}
	void move(Unit u, Vector2 move_vector)
	{
		int nx = u.x + (int)move_vector.x, ny = u.y + (int)move_vector.y;
		int val = GlobalData.field[u.y][u.x][u.z].y;
		GlobalData.removeFromMap(u.x, u.y, u.z);
		GlobalData.addToMap(1, val, nx, ny);
		u.x = nx;
		u.y = ny;
		u.worldX = u.worldX + (int)move_vector.x * GlobalData.SPRITE_SIZE;
		u.worldY = u.worldY + (int)move_vector.y * GlobalData.SPRITE_SIZE;
		u.gameObj.transform.position = new Vector2(u.worldX, u.worldY);
	}

	public void nextAIStep()
	{
		try
		{
			string methodName = "id_" + id.ToString() + "_ai_step";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, null);
		}
		catch
		{
			Debug.Log("AI step " + id.ToString() + " not defined");
		}
	}

	void id_1_ai_step()
	{
		baseAI();
	}

}