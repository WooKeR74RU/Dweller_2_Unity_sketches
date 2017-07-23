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
	public int worldX;
	public int worldY;
	public int x, y, z;
	public GameObject gameObj;
	public int count;
	public bool isDestroyable;
	public bool opacity;
	GenericDialog chooseDialog;

	public Item(int id)
	{

	}

	public static Item createItem(int x, int y, int id, int cnt)
	{
		int sprSize = GlobalData.SPRITE_SIZE;

		Item obj = new Item();
		obj.x = x;
		obj.y = y;
		obj.worldX = x * sprSize;
		obj.worldY = y * sprSize;
		obj.id = id;
		Texture2D tex = GlobalData.getTexureById[id];
		obj.count = cnt;

		GameObject tmpGameObj = new GameObject("Item " + id.ToString());

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

	public void destroy()
	{
		if (isDestroyable)
		{
			UnityEngine.GameObject.Destroy(gameObj);
			gameObj = null;
			GlobalData.removeFromField(x, y, z);
			chooseDialog.Cancel();
		}
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

	void id_2001_initialization()
	{
		isDestroyable = true;
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
		btn.SetOnClickListener(delegate { chooseDialog.Cancel(); });
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