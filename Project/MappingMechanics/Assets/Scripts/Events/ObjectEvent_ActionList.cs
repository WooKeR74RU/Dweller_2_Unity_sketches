/*
 * Objects action list.
 */

public partial class ObjectEvent
{
	public void behaviour(Unit curUnit)
	{
		curUnit.behavior();
	}

	public void move(int toX, int toY, BaseObject obj)
	{
		if (obj is Unit && (obj as Unit).isPlayerControl)
			obj.adr.levelPointer.display.initializationVisibleGameObjects();
		obj.adr.levelPointer.map.moveObj(toX, toY, obj);
	}

	public void step(Pair<int, int> moveVector, BaseObject obj)
	{
		int toX = obj.adr.worldX + moveVector.first;
		int toY = obj.adr.worldY + moveVector.second;
		obj.adr.levelPointer.map.moveObj(toX, toY, obj);
		if (obj is Unit && (obj as Unit).isPlayerControl)
			obj.adr.levelPointer.display.moveCamera(moveVector);
	}
}