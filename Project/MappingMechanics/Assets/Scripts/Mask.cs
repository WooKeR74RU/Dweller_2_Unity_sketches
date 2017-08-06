using UnityEngine;

public class Mask
{
	public string maskName;
	public int worldX;
	public int worldY;

	public int gameObjId;
	public int offsetX = 0;
	public int offsetY = 0;

	public Material material;

	//public stasic delay))00)))))0
	public Mask(string maskName, int worldX, int worldY, int frameCount, float delay)
	{
		this.maskName = maskName;
		this.worldX = worldX;
		this.worldY = worldY;

		gameObjId = GlobalData.freeGameObjectFromPool();
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.transform.position = new Vector2(worldX * GlobalData.SPRITE_SIZE, worldY * GlobalData.SPRITE_SIZE);
		tmp.GetComponent<LinearSpriteAnimation>().initialize(tmp, maskName, frameCount, delay);
	}

	public void setLayerOrder(int layer)
	{
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<SpriteRenderer>().sortingOrder = layer;
	}

	public void setOffset(int offsetX, int offsetY)
	{
		this.offsetX = offsetX;
		this.offsetY = offsetY;
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.transform.position = new Vector2(worldX * GlobalData.SPRITE_SIZE + offsetX, worldY * GlobalData.SPRITE_SIZE + offsetY);
	}

	public void setMaterial(Material material)
	{
		this.material = material;
		GameObject tmp = GlobalData.poolGameObjects[gameObjId];
		tmp.GetComponent<SpriteRenderer>().material = material;
	}

}