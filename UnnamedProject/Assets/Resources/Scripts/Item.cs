using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Reflection;
public class Item
{

	public int id;
	public int Worldx;
	public int Worldy;
	public int x ,y, z = -1;

	public int LayerOrder = 0;
	public GameObject obj;
	public int count;
	public ItemDescription description;
	public bool isDestroyable = true;

	public static Item createItem(int x, int y,int id, int layer)
	{
		Item o = new Item();
		o.x = x;
		o.y = y;
		o.Worldx = x * GlobalData.SPRITE_SIZE;
		o.Worldy = -y * GlobalData.SPRITE_SIZE;
		o.id = id;
		o.LayerOrder = layer;
		o.count = 1;
		Texture2D t = GlobalData.getTexureById[id];
		//o.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		//o.InitMechanic(id);

		GameObject g = new GameObject("Item");
		SpriteRenderer renderer = g.AddComponent<SpriteRenderer>();
		renderer.sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
		o.obj = g;
		renderer.sortingOrder = o.LayerOrder;
		g.transform.position = new Vector2(o.Worldx, o.Worldy);
		return o;
		//GlobalData.Items.Add(o);
	}
	void addToInventory()
	{
		for(int i=0;i<PlayerStats.inventory.Count;i++)
		{
			if(PlayerStats.inventory[i].id == id)
			{
				PlayerStats.inventory[i].count += count;
				destroy();
				return;
			}
		}
		PlayerStats.inventory.Add(this);
		destroy();
	}
	GenericDialog chooseDialog;
	public void destroy()
	{
		if (this.isDestroyable)
		{
			UnityEngine.GameObject.Destroy(this.obj);
			obj = null;
			GlobalData.removeFromMap(x, y, z);
			chooseDialog.Hide();
			chooseDialog.Cancel();
		}
	}
	public void interactMechanic()
	{	try
		{
			showDialog();
		}
		catch
		{
			Debug.Log("KEK");
		}
	}
	void showDialog()
	{
		chooseDialog = new GenericDialog(Screen.width / 2, Screen.height / 2, 500, 0, GameObject.Find("UI"));

		TextView tv = new TextView(0, 0, 0, 0, GameObject.Find("UI"));
		tv.SetMinTextSize(1);
		tv.SetMaxTextSize(30);
		tv.SetText("Взять ID=" + id.ToString() + "?");
		chooseDialog.AddText(tv);
		//aaa.AddText("Добрый день! Я диалоговое окно при взаимодействии с сундуком. Просто так. Ничего не делаю. И на кнопки я реагировать не буду!");
		Button btn = new Button(0, 0, 1, 200, GameObject.Find("UI"));
		btn.SetOnClickListener(delegate { addToInventory(); });
		btn.SetBackground(GlobalData.getTexureById[101]);
		chooseDialog.AddButton(btn);
		chooseDialog.SetChildControlWidth(true);
		chooseDialog.SetChildForceExpandWidth(true);
		chooseDialog.SetBackground(GlobalData.getTexureById[100]);

		btn = new Button(0, 0, 100, 200, GameObject.Find("UI"));
		btn.SetOnClickListener(delegate { chooseDialog.Cancel(); });
		btn.SetBackground(GlobalData.getTexureById[100]);
		chooseDialog.AddButton(btn);
		chooseDialog.Show();
	}
	
}
