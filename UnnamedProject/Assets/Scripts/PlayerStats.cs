//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///*
// *  player stats(XP,Class and etc).
// */
//public class PlayerStats : MonoBehaviour {

//	public static GameObject player;
//	public static new Camera camera;
//	public static List<Item> inventory = new List<Item>();
//	public static int Class, XP;
//	void Start () {
//		player = new GameObject("player");
//		player.AddComponent<SpriteRenderer>();
//		player.GetComponent<SpriteRenderer>().sortingOrder = 100;//!!! Magic Constant
//		player.AddComponent<PlayerContol>();
//		player.transform.position = new Vector2(0, 0);
//		camera = GameObject.Find("Camera").GetComponent<Camera>();
//		playerClassInit(new Database().playerClass);
//		playerXPInit(new Database().PlayerXP);
//	}
//	void playerClassInit(int class_)
//	{
//		Class = class_;
//		player.AddComponent<LinearSpriteAnimation>();
		
//		LinearSpriteAnimation sa = player.GetComponent<LinearSpriteAnimation>();
//		sa.item = player;
//		sa.delay = 0.5f;
//		Texture2D texture;
//		switch (class_)
//		{
//			//Init player Animation	
//			case 1:
//			texture = Resources.Load("Textures/UnitsTextures/1") as Texture2D;
//				for(int i = 0;i<2;i++)
//				{
//					sa.frames.Add(Sprite.Create(texture, new Rect(i*GlobalData.SPRITE_SIZE, 0f, GlobalData.SPRITE_SIZE, GlobalData.SPRITE_SIZE), new Vector2(0.5f, 0.5f), 1));
//				}
				
//				break;
//			case 2:
//				texture = Resources.Load("Textures/UnitsTextures/2") as Texture2D;
//				for (int i = 0; i < 2; i++)
//				{
//					sa.frames.Add(Sprite.Create(texture, new Rect(i * GlobalData.SPRITE_SIZE, 0f, GlobalData.SPRITE_SIZE, GlobalData.SPRITE_SIZE), new Vector2(0.5f, 0.5f), 1));
//				}
//				break;
//			case 3:
//				texture = Resources.Load("Textures/UnitsTextures/3") as Texture2D;
//				for (int i = 0;i<8;i++)
//				{
//					sa.frames.Add(Sprite.Create(texture, new Rect(i*GlobalData.SPRITE_SIZE, 0f, GlobalData.SPRITE_SIZE, GlobalData.SPRITE_SIZE), new Vector2(0.5f, 0.5f), 1));
//				}
			
//				break;
//		}
		
//	}
//	void playerXPInit(int xp_)
//	{
//		XP = xp_;
//	}
//}
