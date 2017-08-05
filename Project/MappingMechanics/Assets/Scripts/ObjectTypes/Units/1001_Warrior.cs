using UnityEngine;

public class Warrior : Unit
{
	public override BaseObject fullCopy()
	{
		Warrior other = MemberwiseClone() as Warrior;
		return other;
	}

	public override void setView()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, id, 1, 0);
		//TODO: Переделать обработку слоев
		tmp.GetComponent<SpriteRenderer>().sortingOrder = 3;
		offsetX = -12;
		offsetY = 0;
	}

	public override void AIBehavior()
	{
		baseAI();
	}

	public Warrior()
	{
		id = GlobalData.getObjectIdByName(ToString());
		opacity = false;

		range = 7;
		obstaclePassCount = 1;
	}
}