using System;
using System.Collections.Generic;

public abstract class Unit : BaseObject
{
	public static Pair<int, int>[] dir8 = new Pair<int, int>[] { new Pair<int, int>(0, 1), new Pair<int, int>(1, 1), new Pair<int, int>(1, 0), new Pair<int, int>(1, -1), new Pair<int, int>(0, -1), new Pair<int, int>(-1, -1), new Pair<int, int>(-1, 0), new Pair<int, int>(-1, 1) };
	public static Pair<int, int>[] dir4 = new Pair<int, int>[] { new Pair<int, int>(0, 1), new Pair<int, int>(1, 0), new Pair<int, int>(0, -1), new Pair<int, int>(-1, 0) };

	public bool isPlayerControl;

	public FOV fov;
	public int range;
	public int obstaclePassCount;
	
	public static Random random = new Random();

	public abstract void AIBehavior();

	public override void initializeCommonGroupComponents()
	{
		
	}

	public void behavior()
	{
		if (isPlayerControl)
			GlobalData.control.waitCommand(this);
		else
			AIBehavior();
	}

	public void baseAI() //temporarily
	{
		ObjectEventSequence sequence = new ObjectEventSequence();
		for (int i = 0; i < 100; i++) //temporarily
		{
			int dir = random.Next(8);
			if (isPathPossible(dir8[dir]))
			{
				sequence.addEvent("step", new object[] { dir8[dir], this });
				break;
			}
		}
		sequence.addEvent("behaviour", new object[] { this });
		adr.levelPointer.eventSystem.addSequence(sequence, 1);
		adr.levelPointer.eventSystem.isExecutionAvailable = true;
	}

	public bool getCollisionInPoint(int worldX, int worldY)
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
}