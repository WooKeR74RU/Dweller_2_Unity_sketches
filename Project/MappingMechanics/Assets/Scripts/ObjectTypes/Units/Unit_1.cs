using System.Collections.Generic;

public abstract class Unit : BaseObject
{
	public static Pair<int, int>[] dir8 = new Pair<int, int>[] { new Pair<int, int>(0, 1), new Pair<int, int>(1, 1), new Pair<int, int>(1, 0), new Pair<int, int>(1, -1), new Pair<int, int>(0, -1), new Pair<int, int>(-1, -1), new Pair<int, int>(-1, 0), new Pair<int, int>(-1, 1) };
	public static Pair<int, int>[] dir4 = new Pair<int, int>[] { new Pair<int, int>(0, 1), new Pair<int, int>(1, 0), new Pair<int, int>(0, -1), new Pair<int, int>(-1, 0) };

	public FOV fov;
	public int range;
	public int obstaclePassCount;

	public bool isPlayerControl;

	public abstract override BaseObject fullCopy();
	public abstract override void setView();
	public abstract void AIBehavior();

	public void behavior()
	{
		if (isPlayerControl)
			GlobalData.control.waitCommand(this);
		else
			AIBehavior();
	}

	private void baseAI() //temporarily
	{
		for (int i = 0; i < 100; i++) //temporarily
		{
			int dir = (new System.Random()).Next(8);
			if (isPathPossible(dir8[dir]))
			{
				adr.levelPointer.eventSystem.addEvent("step", new object[] { dir8[dir], this }, 1);
				break;
			}
		}
	}

	private bool getCollisionInPoint(int worldX, int worldY)
	{
		List<BaseObject> list = adr.levelPointer.map.getObjectsListInPoint(worldX, worldY);
		for (int i = 0; i < list.Count; i++)
		{
			int type = GlobalData.getObjectTypeById(list[i].id);
			if (type == 0)
			{
				Entity curEntity = list[i] as Entity;
				if (curEntity.collision)
					return true;
			}
			if (type == 1)
			{
				return true;
			}
			if (type == 2)
			{ }
		}
		return false;
	}

	public bool	isPathPossible(Pair<int, int> moveVector)
	{
		int toX = adr.worldX + moveVector.first;
		int toY = adr.worldY + moveVector.second;
		if (getCollisionInPoint(toX, toY))
			return false;
		return true;
	}






	////TODO: Если юнит попал в обзор камеры, дать ему gameObject
	//public bool move(Pair<int, int> moveVector)
	//{
	//	int toX = adr.worldX + moveVector.first;
	//	int toY = adr.worldY + moveVector.second;

	//	if (/*getCollisionInPoint(toX, toY) || */toX == adr.worldX && toY == adr.worldY)
	//		return false;

	//	adr.mapPointer.moveObj(toX, toY, this);
	//	if (isPlayerControl)
	//		GlobalData.game.levels[GlobalData.game.curLevelName].display.moveCamera(moveVector);

	//	return true;
	//}
}