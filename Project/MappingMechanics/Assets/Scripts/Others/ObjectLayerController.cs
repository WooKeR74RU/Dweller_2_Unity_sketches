using UnityEngine;

public class ObjectLayerController : MonoBehaviour
{
	private int layerRate_;
	public int layerRate
	{
		get
		{
			return layerRate_;
		}
		set
		{
			layerRate_ = value;
			updateLayer();
		}
	}

	public int layer
	{ 
		get
		{
			int worldY = (int)gameObject.transform.position.y / GlobalData.SPRITE_SIZE;
			return worldY * layerRate;
		}
	}

	public void updateLayer()
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = layer;
	}
}