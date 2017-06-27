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
	public int x, y, z = -1;
	public int layerOrder = 0, speed = 1;
	public bool collision;
	public GameObject obj;

	public static Unit createUnit(int x, int y, int id, int layer)
	{
		Unit o = new Unit();
		o.x = x;
		o.y = y;
		o.worldX = x * GlobalData.SPRITE_SIZE;
		o.worldY = -y * GlobalData.SPRITE_SIZE;
		o.id = id;
		o.layerOrder = layer;
		
		GameObject g = new GameObject("Unit " + id.ToString());
		SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
		g.transform.position = new Vector2(o.worldX, o.worldY);
		o.obj = g;
		o.initMechanic();
		renderer.sortingOrder = o.layerOrder;
		return o;
	}

	void setCollision(bool collision)
	{
		this.collision = collision;
	}
	void transform(Unit u, Vector2 move_vector)
	{
		int nx = u.x + (int)move_vector.x, ny = u.y - (int)move_vector.y;
		int val = GlobalData.map[u.y][u.x][u.z].Value;

		GlobalData.removeFromMap(u.x, u.y, u.z);
		GlobalData.addToMap(1, val, nx, ny);
		u.x = nx;
		u.y = ny;
		u.worldX = u.worldX + (int)move_vector.x * GlobalData.SPRITE_SIZE;
		u.worldY = u.worldY + (int)move_vector.y * GlobalData.SPRITE_SIZE;
		u.obj.transform.position = new Vector2(u.worldX, u.worldY);
	}
	
	//Call when object create
	public void initMechanic()
	{
		try
		{
			string methodName = "id_" + id.ToString() + "_initialize";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, null);
		}
		catch (Exception e)
		{
			Debug.Log("Initialize mechanic with " + id.ToString() + " not defined. " + e.ToString());
		}
	}
	//

	public void interactMechanic(int pos, Vector2 move_vector)
	{
		int id = GlobalData.units[pos].id;
		try
		{
			string methodName = "ID" + id.ToString() + "Interact";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, new object[] { move_vector });
		}
		catch (Exception e)
		{
			Debug.Log("Interract mechanic with " + id.ToString() + " not defined. " + e.ToString());
		}
	}

	Vector2[] possiblePath = new Vector2[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
	public void NextStep()
	{
		while (true)
		{
			int dir = new System.Random().Next(4);
			if (!EnablePaths(x, y, possiblePath[dir]))
				continue;
			transform(this, possiblePath[dir]);
			break;
		}
	}

	bool EnablePaths(int x_, int y_, Vector2 move_vector)
	{
		x_ += (int)move_vector.x;
		y_ -= (int)move_vector.y;
		bool isEnable = true;
		if (PlayerContol.mapx == x_ && PlayerContol.mapy == y_)
			isEnable = false;
		if (y_ >= GlobalData.map.Count || x_ >= GlobalData.map[y_].Count)
			return false;
		for (int i = 0; i < GlobalData.map[y_][x_].Count; i++)
		{
			switch (GlobalData.map[y_][x_][i].Key)
			{
				case 0:
					Entity Cur0 = GlobalData.entities[GlobalData.map[y_][x_][i].Value];
					//Cur0.InteractMechanic(move_vector);
					if (Cur0.collision)
					{
						isEnable = false;
					}
					break;

				case 1:
					break;

				case 2:
					break;
			}
		}
		return isEnable;
	}

	//Units initialization mechanic by id
	public void id_13_initialize()
	{
		setCollision(true);
		LinearSpriteAnimation anim = obj.AddComponent<LinearSpriteAnimation>();
		anim.delay = 0.5f;
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat1"));
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat2"));
		anim.item = obj;
	}
	//Units interact mechanic by id
	public void id_13_ai_step()
	{
		setCollision(true);
		LinearSpriteAnimation anim = obj.AddComponent<LinearSpriteAnimation>();
		anim.delay = 0.5f;
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat1"));
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat2"));
		anim.item = obj;
	}



}
