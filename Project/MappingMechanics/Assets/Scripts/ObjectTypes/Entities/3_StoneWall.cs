using UnityEngine;

public class StoneWall : Entity
{
	public override BaseObject fullCopy()
	{
		StoneWall other = MemberwiseClone() as StoneWall;
		return other;
	}

	public override void setView()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, id, 1, 0);
		tmp.GetComponent<SpriteRenderer>().sortingOrder = 0;
	}

	public StoneWall()
	{
		initializeCommonGroupComponents();
		id = GlobalData.getObjectIdByName(ToString());
		opacity = true;

		collision = true;
		movable = false;
		trap = false;
	}
}