using System;
using System.Collections.Generic;
using UnityEngine;

public class Display
{
	public bool active;
	public Map mapPointer;

	public int camX, camY;
	public int cameraX //current camera position X
	{
		get
		{
			return camX;
		}
		set
		{
			camX = value;
			if (active)
			{
				float camX = this.camX + GlobalData.viewportM / 2 + 0.5f;
				float camY = this.camY + GlobalData.viewportN / 2 + 0.5f;
				GlobalData.cameraObj.transform.position = new Vector2(camX * GlobalData.SPRITE_SIZE, camY * GlobalData.SPRITE_SIZE);
			}
		}
	}
	public int cameraY //current camera position Y
	{
		get
		{
			return camY;
		}
		set
		{
			camY = value;
			if (active)
			{
				float camX = this.camX + GlobalData.viewportM / 2 + 0.5f;
				float camY = this.camY + GlobalData.viewportN / 2 + 0.5f;
				GlobalData.cameraObj.transform.position = new Vector2(camX * GlobalData.SPRITE_SIZE, camY * GlobalData.SPRITE_SIZE);
			}
		}
	}

	public Display(Map mapPointer)
	{
		this.mapPointer = mapPointer;
		active = false;
		cameraX = mapPointer.mapDesc.worldStartPointX - GlobalData.viewportM / 2;
		cameraY = mapPointer.mapDesc.worldStartPointY - GlobalData.viewportN / 2;
	}

	public void initializationVisibleGameObjects()
	{
		if (!active)
			return;
		for (int i = cameraY; i < cameraY + GlobalData.viewportN; i++)
		{
			for (int j = cameraX; j < cameraX + GlobalData.viewportM; j++)
			{
				List<BaseObject> list = mapPointer.getObjectsListInPoint(j, i);
				setGameObjectsList(list);
			}
		}
	}

	public void clearVisibleGameObjects()
	{
		if (!active)
			return;
		for (int i = cameraY; i < cameraY + GlobalData.viewportN; i++)
		{
			for (int j = cameraX; j < cameraX + GlobalData.viewportM; j++)
			{
				List<BaseObject> list = mapPointer.getObjectsListInPoint(j, i);
				resetGameObjectsList(list);
			}
		}
	}

	public void resetGameObjectsList(List<BaseObject> list)
	{
		if (list == null)
			return;
		for (int j = 0; j < list.Count; j++)
			GlobalData.freeGameObjectToPool(ref list[j].gameObjId);
	}

	public void setGameObjectsList(List<BaseObject> list)
	{
		if (list == null)
			return;
		for (int j = 0; j < list.Count; j++)
			list[j].setGameObject(GlobalData.freeGameObjectFromPool());
	}

	private void moveCameraUp()
	{
		if (active)
		{
			for (int i = cameraX; i < cameraX + GlobalData.viewportM; i++)
			{
				List<BaseObject> tmp = mapPointer.getObjectsListInPoint(i, cameraY);
				resetGameObjectsList(tmp);
				tmp = mapPointer.getObjectsListInPoint(i, cameraY + GlobalData.viewportN);
				setGameObjectsList(tmp);
			}
		}
		cameraY++;
	}
	private void moveCameraDown()
	{
		cameraY--;
		if (active)
		{
			for (int i = cameraX; i < cameraX + GlobalData.viewportM; i++)
			{
				List<BaseObject> tmp = mapPointer.getObjectsListInPoint(i, cameraY + GlobalData.viewportN);
				resetGameObjectsList(tmp);
				tmp = mapPointer.getObjectsListInPoint(i, cameraY);
				setGameObjectsList(tmp);
			}
		}
	}
	private void moveCameraLeft()
	{
		cameraX--;
		if (active)
		{
			for (int i = cameraY; i < cameraY + GlobalData.viewportN; i++)
			{
				List<BaseObject> tmp = mapPointer.getObjectsListInPoint(cameraX + GlobalData.viewportM, i);
				resetGameObjectsList(tmp);
				tmp = mapPointer.getObjectsListInPoint(cameraX, i);
				setGameObjectsList(tmp);
			}
		}
	}
	private void moveCameraRight()
	{
		if (active)
		{
			for (int i = cameraY; i < cameraY + GlobalData.viewportN; i++)
			{
				List<BaseObject> tmp = mapPointer.getObjectsListInPoint(cameraX, i);
				resetGameObjectsList(tmp);
				tmp = mapPointer.getObjectsListInPoint(cameraX + GlobalData.viewportM, i);
				setGameObjectsList(tmp);
			}
		}
		cameraX++;
	}

	public void moveCamera(Pair<int, int> moveVector)
	{
		if (moveVector.second == 1)
			moveCameraUp();
		if (moveVector.second == -1)
			moveCameraDown();
		if (moveVector.first == -1)
			moveCameraLeft();
		if (moveVector.first == 1)
			moveCameraRight();

		int cameraCenterX = cameraX + GlobalData.viewportM / 2;
		int cameraCenterY = cameraY + GlobalData.viewportN / 2;
		int ht = (int)((mapPointer.centerSegY + 1.5f) * mapPointer.mapDesc.segSizeN);
		int hd = (int)((mapPointer.centerSegY - 0.5f) * mapPointer.mapDesc.segSizeN);
		int wl = (int)((mapPointer.centerSegX - 0.5f) * mapPointer.mapDesc.segSizeM);
		int wr = (int)((mapPointer.centerSegX + 1.5f) * mapPointer.mapDesc.segSizeM);

		if (cameraCenterY == ht || cameraCenterY == hd || cameraCenterX == wl || cameraCenterX == wr)
			Debug.Log("New area was loaded.");

		if (cameraCenterY == ht)
			mapPointer.updateAreaUp();
		if (cameraCenterY == hd)
			mapPointer.updateAreaDown();
		if (cameraCenterX == wl)
			mapPointer.updateAreaLeft();
		if (cameraCenterX == wr)
			mapPointer.updateAreaRight();
	}

}