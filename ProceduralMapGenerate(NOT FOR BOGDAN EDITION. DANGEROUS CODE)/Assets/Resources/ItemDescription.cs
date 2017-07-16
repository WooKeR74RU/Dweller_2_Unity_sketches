using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription {

	public int id;
	public int x, y;

	public ItemDescription(int id,int x,int y)
	{
		this.x = x;
		this.y = y;
		this.id = id;
	}
}
