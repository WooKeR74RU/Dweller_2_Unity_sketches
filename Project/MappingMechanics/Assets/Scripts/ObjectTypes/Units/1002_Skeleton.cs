using UnityEngine;

public class Skeleton : Unit
{
	public override BaseObject fullCopy()
	{
		Skeleton other = MemberwiseClone() as Skeleton;
		return other;
	}

	public override void setView()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, id, 2, 1);
		tmp.GetComponent<SpriteRenderer>().sortingOrder = 3;
		offsetX = 0;
		offsetY = 0;
	}

	public override void AIBehavior()
	{
		baseAI();
	}

	public Skeleton()
	{
		id = GlobalData.getObjectIdByName(ToString());
		opacity = false;

		range = 7;
		obstaclePassCount = 1;
	}
}