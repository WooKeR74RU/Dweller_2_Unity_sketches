using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Create animation from Sprite array.
*/

public class LinearSpriteAnimation : MonoBehaviour
{
	public GameObject gameObj; //Object for animation
	public List<Sprite> frames = new List<Sprite>(); //Animation frames
	private int curFrame = 0; //Current frame
	public float delay = 0; //Time between frame
	void Start()
	{
		InvokeRepeating("changeFrame", 0.0f, delay);
	}
	public void changeFrame()
	{
		if (curFrame == frames.Count)
			curFrame = 0;
		SpriteRenderer sprRenderer = gameObj.GetComponent<SpriteRenderer>();
		sprRenderer.sprite = frames[curFrame];
		curFrame++;
	}
}
