using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
public class Entity {

	public int id;
	public int worldx;
	public int worldy;
	public int x, y, z = -1;
	//public Sprite sprite;
	public int layerOrder = 0;
	public bool collision, movable;
	public GameObject obj;
	//Base mechanic
	public static Entity createItem(int x, int y, int id, int layer)
	{
		Entity o = new Entity();
		o.x = x;
		o.y = y;
		o.worldx = x*GlobalData.SPRITE_SIZE;
		o.worldy = -y* GlobalData.SPRITE_SIZE;
		o.id = id;
		o.layerOrder = layer;
		Texture2D t = GlobalData.getTexureById[id];
		//o.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		//o.InitMechanic(id);

		GameObject g = new GameObject("Object " + id.ToString());
		SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
		renderer.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		renderer.sortingOrder = o.layerOrder;
		g.transform.position = new Vector2(o.worldx, o.worldy);
		o.obj = g;
		//GlobalData.Objects.Add(o);
		o.initMechanic();
		return o; //НЕ return 0;
	}

	void setCollision(bool collision)
	{
		this.collision = collision;
	}
	void setMovable(bool movable)
	{
		this.movable = movable;
	}

	bool transform(Entity cur, Vector2 move_vector)
	{
		int nx = cur.x + (int)move_vector.x, ny = cur.y - (int)move_vector.y;

		if (GlobalData.map.Count <= ny || GlobalData.map[ny].Count <= nx)
			return false;
		
		bool move = true;
		for (int i = 0; i < GlobalData.map[ny][nx].Count; i++)
		{
			int type = GlobalData.map[ny][nx][i].Key;
			if (type == 0)
			{
				Entity c = GlobalData.entities[GlobalData.map[ny][nx][i].Value];
				if (c.collision && !c.movable)
				{
					move = false;
				}
				if (c.collision && c.movable)
				{
					move = transform(c, move_vector);
				}
			}
			if (type == 1)
			{
				Unit c = GlobalData.units[GlobalData.map[ny][nx][i].Value];
				if (c.collision)
				{
					move = false;
				}
			}
			if (type == 2)
			{}
		}

		if (move)
		{
			//Object_ cur2 = (Object_)cur.MemberwiseClone();
			int lastx = cur.x, lasty = cur.y, lastz = cur.z;

			cur.x = nx;
			cur.y = ny;
			cur.worldx = cur.worldx + (int)move_vector.x * GlobalData.SPRITE_SIZE;
			cur.worldy = cur.worldy + (int)move_vector.y * GlobalData.SPRITE_SIZE;
			cur.obj.transform.position = new Vector2(cur.worldx, cur.worldy);
			GlobalData.addToMap(0, GlobalData.map[lasty][lastx][lastz].Value, nx, ny);
			GlobalData.removeFromMap(lastx, lasty, lastz);

		}
		return move;
	}
	//

	//Call when object create
	public void initMechanic()
	{
		try
		{
			string methodName = "ID" + id.ToString() + "Start";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, null);
		}
		catch (Exception e)
		{
			Debug.Log("Start mechanic with " + id.ToString() + " not defined. " + e.ToString());
		}
	}
	//Call when interact
	public void interactMechanic(Vector2 move_vector)
	{
		try
		{
			string methodName = "ID" + id.ToString() + "Interact";
			MethodInfo mi = this.GetType().GetMethod(methodName);
			mi.Invoke(this, new object[] { move_vector });
		}
		catch(Exception e)
		{
			Debug.Log("Interract mechanic with " + id.ToString() + " not defined. " + id.ToString());
		}
	}

	public void ID0Start()
	{
		setCollision(false);
		setMovable(false);
	}
	public void ID100Start()
	{
		setCollision(false);
		setMovable(false);
	}
	public void ID101Start()
	{
		setCollision(false);
		setMovable(false);
	}
	public void ID102Start()
	{
		setCollision(true);
		setMovable(true);
	}
	public void ID102Interact(Vector2 move_vector)
	{
		obj.GetComponent<SpriteRenderer>().color = Color.green;
		if(transform(this, move_vector))
		{
			PlayerContol.movePlayer(move_vector);
			Debug.Log(move_vector.ToString());
		}
	}
	

	public void ID103Start()
	{
		setCollision(true);
		setMovable(false);
		obj.GetComponent<SpriteRenderer>().sortingOrder = layerOrder;
	}
	
	public void ID103Interact(Vector2 move_vector)
	{
		obj.GetComponent<SpriteRenderer>().sprite = Sprite.Create(GlobalData.getTexureById[104], new Rect(0, 0, GlobalData.getTexureById[104].width, GlobalData.getTexureById[104].height), new Vector2(0.5f, 0.5f), 1);
		id = 104;
	
		GlobalData.addToMapNewObject(Item.createItem(PlayerContol.mapx,PlayerContol.mapy, 106, 2), PlayerContol.mapx, PlayerContol.mapy);

	
	}
	public void ID105Start()
	{
		setCollision(true);
		setMovable(false);
		obj.GetComponent<SpriteRenderer>().sortingOrder = layerOrder;
	}
}
