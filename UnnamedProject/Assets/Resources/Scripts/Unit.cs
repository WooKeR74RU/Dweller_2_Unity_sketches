using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
public class Unit
{

	public int id;
	public int worldx;
	public int worldy;
	public int x, y, z = -1;
	public int layerOrder = 0, speed = 1;
	public bool collision;
	public GameObject obj;
	//Base mechanic
	public static Unit createUnit(int x, int y, int id, int layer)
	{
		Unit o = new Unit();
		o.x = x;
		o.y = y;
		o.worldx = x * GlobalData.SPRITE_SIZE;
		o.worldy = -y * GlobalData.SPRITE_SIZE;
		o.id = id;
		o.layerOrder = layer;
		//Texture2D t = GlobalData.texture_by_id[id];
		//o.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		//o.InitMechanic(id);

		GameObject g = new GameObject("Unit " + id.ToString());
		SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
		//renderer.sprite = o.sprite;
		renderer.sortingOrder = o.layerOrder;
		g.transform.position = new Vector2(o.worldx, o.worldy);
		o.obj = g;
		//GlobalData.Units.Add(o);
		o.initMechanic();
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
		u.worldx = u.worldx + (int)move_vector.x * GlobalData.SPRITE_SIZE;
		u.worldy = u.worldy + (int)move_vector.y * GlobalData.SPRITE_SIZE;
		u.obj.transform.position = new Vector2(u.worldx, u.worldy);
	}
	//Call when object create
	public void initMechanic()
	{
		int id_ = this.id;
		try
		{

			string methodName = "ID" + id_.ToString() + "Start";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, null);
		}
		catch (Exception e)
		{
			Debug.Log("Start mechanic with " + id_.ToString() + " not defined. " + e.ToString());
		}
	}
	//Call when interact
	public void interactMechanic(int pos, Vector2 move_vector)
	{
		int id_ = GlobalData.units[pos].id;
		try
		{
			string methodName = "ID" + id_.ToString() + "Interact";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, new object[] { move_vector });
		}
		catch (Exception e)
		{
			Debug.Log("Interract mechanic with " + id_.ToString() + " not defined. " + e.ToString());
		}
	}
	public void ID13Start()
	{
		setCollision(true);
		LinearSpriteAnimation anim = obj.AddComponent<LinearSpriteAnimation>();
		anim.delay = 0.5f;
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat1"));
		anim.frames.Add(Utility.getSpriteByPath("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat2"));
		anim.item = obj;
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

}
