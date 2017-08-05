using UnityEngine;

public class StoneFloor : Entity
{
	public override BaseObject fullCopy()
	{
		StoneFloor other = MemberwiseClone() as StoneFloor;
		return other;
	}
	
	public override void setView()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, id, 1, 0);
		tmp.GetComponent<SpriteRenderer>().sortingOrder = 0;
	}

	public StoneFloor()
	{
		id = GlobalData.getObjectIdByName(ToString());
		opacity = false;

		collision = false;
		movable = false;
		trap = false;
	}
}