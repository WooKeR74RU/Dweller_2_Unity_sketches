﻿using UnityEngine;

public class Coins : Item
{
	public override BaseObject fullCopy()
	{
		Coins other = MemberwiseClone() as Coins;
		return other;
	}

	public override void setView()
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, id, 1, 0);
		tmp.GetComponent<SpriteRenderer>().sortingOrder = 500;
	}

	public Coins()
	{
		initializeCommonGroupComponents();
		id = GlobalData.getObjectIdByName(ToString());
		opacity = false;

		isDestroyable = true;
		count = 1;
	}
}