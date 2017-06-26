using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  player stats(XP,Class and etc).
 */
public class PlayerStats : MonoBehaviour {

	public static GameObject player;
	public static new Camera camera;
	public static List<Item> inventory = new List<Item>();
	public static int Class, XP;
	void Start () {
		player = new GameObject("player");
		player.AddComponent<SpriteRenderer>();
		player.GetComponent<SpriteRenderer>().sortingOrder = 100;//!!! Magic Constant
		player.AddComponent<PlayerContol>();
		player.transform.position = new Vector2(0, 0);
		camera = GameObject.Find("Camera").GetComponent<Camera>();
		playerClassInit(new DB().PlayerClass);
		playerXPInit(new DB().PlayerXP);
	}
	void playerClassInit(int class_)
	{
		Class = class_;
		player.AddComponent<LinearSpriteAnimation>();
		
		LinearSpriteAnimation sa = player.GetComponent<LinearSpriteAnimation>();
		sa.item = player;
		sa.delay = 0.5f;
		Texture2D texture;
		switch (class_)
		{
			//Init player Animation	
			case 1:
				texture = Resources.Load("Textures/PlayerAnimationFrame/Warrior(1)/Warrior1") as Texture2D;
				sa.frames.Add(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1));
				texture = Resources.Load("Textures/PlayerAnimationFrame/Warrior(1)/Warrior2") as Texture2D;
				sa.frames.Add(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1));
				
				break;
			case 2:
				texture = Resources.Load("Textures/PlayerAnimationFrame/Ranger(2)/Ranger1") as Texture2D;
				sa.frames.Add(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1));
				texture = Resources.Load("Textures/PlayerAnimationFrame/Ranger(2)/Ranger2") as Texture2D;
				sa.frames.Add(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1));
				break;
			case 3:
				texture = Resources.Load("Textures/PlayerAnimationFrame/Wizard(3)/magAnimation") as Texture2D;
				for(int i = 0;i<8;i++)
				{
					sa.frames.Add(Sprite.Create(texture, new Rect(i*GlobalData.SPRITE_SIZE, 0f, GlobalData.SPRITE_SIZE, GlobalData.SPRITE_SIZE), new Vector2(0.5f, 0.5f), 1));
				}
			
				break;
		}
		
	}
	void playerXPInit(int xp_)
	{
		XP = xp_;
	}
}
