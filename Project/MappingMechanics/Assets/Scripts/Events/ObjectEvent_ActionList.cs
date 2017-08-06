/*
 * Objects action list.
 */

public partial class ObjectEvent
{
	public void behaviour(Unit curUnit)
	{
		curUnit.behavior();
	}

	//Funny comment
	public void move(int toX, int toY, BaseObject obj)
	{
		obj.adr.levelPointer.map.moveObj(toX, toY, obj);
		if (obj is Unit && (obj as Unit).isPlayerControl)
		{ 
			obj.adr.levelPointer.display.clearVisibleGameObjects();
			obj.adr.levelPointer.display.initializationVisibleGameObjects();
		}
	}

	public void step(Pair<int, int> moveVector, BaseObject obj)
	{
		bool prevInCamera = obj.isObjectInCamera();

		int toX = obj.adr.worldX + moveVector.first;
		int toY = obj.adr.worldY + moveVector.second;
		obj.adr.levelPointer.map.moveObj(toX, toY, obj);
		if (obj is Unit && (obj as Unit).isPlayerControl)
			obj.adr.levelPointer.display.stepCamera(moveVector);
		
		bool curInCamera = obj.isObjectInCamera();
		if (!prevInCamera && curInCamera)
			obj.setGameObject(GlobalData.freeGameObjectFromPool());
		if (prevInCamera && !curInCamera)
			GlobalData.freeGameObjectToPool(ref obj.gameObjId);

		obj.updateGameObject();
	}
}