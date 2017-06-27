using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Create animation from Sprite array.
*/

public class LinearSpriteAnimation : MonoBehaviour {

	public GameObject item;//Object for animation
	public List<Sprite> frames = new List<Sprite>();//Animation frames
	private int current_state = 0;//Current frame
	public float delay = 0;//Time between frame
	void Start()
	{
		InvokeRepeating("ChangeFrame", 0.0f, delay);
	}
	public void ChangeFrame()
	{
		if (current_state == frames.Count)
			current_state = 0;
		SpriteRenderer render = item.GetComponent<SpriteRenderer>();
		render.sprite = frames[current_state];
		current_state++;
	}
}
