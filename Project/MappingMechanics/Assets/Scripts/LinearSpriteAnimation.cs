using System.Collections.Generic;
using UnityEngine;

/*
	Create animation from Sprite array.
*/

[RequireComponent(typeof(SpriteRenderer))]
public class LinearSpriteAnimation : MonoBehaviour
{
	public GameObject gameObj; //Object for animation
	public List<Sprite> frames = new List<Sprite>(); //Animation frames
	public int framesCount; //Frames count
	private int curFrame; //Current frame
	public float delay; //Time between frame

	public void initialize(GameObject gameObj, int id, int framesCount, float delay)
	{
		this.gameObj = gameObj;
		this.framesCount = framesCount;
		this.delay = delay;
		Texture2D texture = GlobalData.getObjectTextureById(id);
		int frameWidth = texture.width / framesCount;
		for (int i = 0; i < framesCount; i++)
			frames.Add(Sprite.Create(texture, new Rect(i * frameWidth, 0, frameWidth, texture.height), new Vector2(0, 0), 1));
		if (framesCount == 1)
			changeFrame();
		else
			InvokeRepeating("changeFrame", 0, delay);
	}

	public void initialize(GameObject gameObj, string maskName, int framesCount, float delay)
	{
		this.gameObj = gameObj;
		this.framesCount = framesCount;
		this.delay = delay;
		Texture2D texture = GlobalData.getMaskTextureByName(maskName);
		int frameWidth = texture.width / framesCount;
		for (int i = 0; i < framesCount; i++)
			frames.Add(Sprite.Create(texture, new Rect(i * frameWidth, 0, frameWidth, texture.height), new Vector2(0, 0), 1));
		if (framesCount == 1)
			changeFrame();
		else
			InvokeRepeating("changeFrame", 0, delay);
	}

	public void changeFrame()
	{
		if (curFrame == framesCount)
			curFrame = 0;
		SpriteRenderer sprRenderer = gameObj.GetComponent<SpriteRenderer>();
		sprRenderer.sprite = frames[curFrame];
		curFrame++;
	}

	public void clear()
	{
		CancelInvoke();
		gameObj = null;
		frames.Clear();
		framesCount = 0;
		curFrame = 0;
		delay = 0;
	}
}
