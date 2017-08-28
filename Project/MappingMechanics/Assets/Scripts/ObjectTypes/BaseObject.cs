using System;
using System.Reflection;
using UnityEngine;

public abstract partial class BaseObject
{
	public int id;
	public Adress adr;

	public int gameObjId;
	public int offsetX, offsetY;

	public int layerRate = 0;
	public int layer
	{
		get
		{
			return adr.worldY * layerRate;
		}
	}
	
	public bool opacity;

	public void setGameObject(int gameObjId)
	{
		Debug.Log(layerRate);

		this.gameObjId = gameObjId;
		setView();
		updateGameObject();
	}

	public bool isObjectInCamera()
	{
		if (!adr.levelPointer.display.active)
			return false;
		if (adr.levelPointer.display.cameraX <= adr.worldX && adr.worldX < adr.levelPointer.display.cameraX + GlobalData.viewportM && adr.levelPointer.display.cameraY <= adr.worldY && adr.worldY < adr.levelPointer.display.cameraY + GlobalData.viewportN)
		{
			return true;
		}
		return false;
	}

	public void updateGameObject()
	{
		if (gameObjId == -1)
			return;
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.transform.position = new Vector2(adr.worldX * GlobalData.SPRITE_SIZE + offsetX, adr.worldY * GlobalData.SPRITE_SIZE + offsetY);
	}

	public static BaseObject createNewObject(int id)
	{
		Type objectType = Type.GetType(GlobalData.getObjectNameById(id));
		ConstructorInfo ci = objectType.GetConstructor(Type.EmptyTypes);
		return ci.Invoke(new object[] { }) as BaseObject;
	}

	public abstract void initializeCommonGroupComponents();
	public abstract BaseObject fullCopy();
	public abstract void setView();
}