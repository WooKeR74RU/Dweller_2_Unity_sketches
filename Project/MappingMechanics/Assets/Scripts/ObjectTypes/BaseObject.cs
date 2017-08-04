using System;
using System.Reflection;
using UnityEngine;

public abstract partial class BaseObject
{
	public int id;
	public Adress adr;

	public bool opacity;
	
	public int offsetX, offsetY;

	public int gameObjId;
	public void setGameObject(int gameObjId)
	{
		this.gameObjId = gameObjId;
		setView();
		updateGameObject();
	}

	public void updateGameObject()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.transform.position = new Vector2(adr.worldX * GlobalData.SPRITE_SIZE + offsetX, adr.worldY * GlobalData.SPRITE_SIZE + offsetY);
	}

	public static BaseObject createNewObject(int id)
	{
		Type objectType = Type.GetType(GlobalData.getObjectNameById(id));
		ConstructorInfo ci = objectType.GetConstructor(Type.EmptyTypes);
		return ci.Invoke(new object[] { }) as BaseObject;
	}

	public abstract BaseObject fullCopy();
	public abstract void setView();
}