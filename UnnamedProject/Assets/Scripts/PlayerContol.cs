//using System.IO;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Reflection;
//public  class PlayerContol : MonoBehaviour {
//	//Current position in array map
//	public static int mapx = 1, mapy = 1;

//	GameObject player = PlayerStats.player;
		
//    void Start () {

	
//		player.transform.position = new Vector2 (mapx* GlobalData.SPRITE_SIZE, -mapy* GlobalData.SPRITE_SIZE);
//		PlayerStats.camera.transform.position = new Vector3 (mapx* GlobalData.SPRITE_SIZE, -mapy* GlobalData.SPRITE_SIZE,-60);
//	}

//	void Update () {
//		bool KeyDown = false;
//		float x, y;
//		int Mapx_ = mapx, Mapy_ = mapy;
//		x = player.transform.position.x;
//		y = player.transform.position.y;
//		if (Input.GetKeyDown (KeyCode.D)) {
//			x += GlobalData.SPRITE_SIZE;
//			Mapx_++;
//			KeyDown = true;
//		}
//		if (Input.GetKeyDown (KeyCode.A)) {
//			x -= GlobalData.SPRITE_SIZE;
//			Mapx_--;
//			KeyDown = true;
//		}
//		if (Input.GetKeyDown (KeyCode.W)) {
//			y += GlobalData.SPRITE_SIZE;
//			Mapy_--;
//			KeyDown = true;
//		}
//		if (Input.GetKeyDown (KeyCode.S)) {
//			y -= GlobalData.SPRITE_SIZE;
//			Mapy_++;
//			KeyDown = true;
//		}
//		if (!KeyDown)
//			return;
		
//		Vector2 move_vector = new Vector2(Mapx_ - mapx, mapy - Mapy_);
//		if (!isPathEnable(Mapx_, Mapy_, move_vector))
//			return;
	



//	}
//	public static void movePlayer(Vector2 move_vector)
//	{
//		PlayerContol.mapx += (int)move_vector.x;
//		PlayerContol.mapy -= (int)move_vector.y;

//		PlayerStats.camera.transform.position = new Vector3(PlayerStats.camera.transform.position.x + move_vector.x * GlobalData.SPRITE_SIZE, PlayerStats.camera.transform.position.y + move_vector.y * GlobalData.SPRITE_SIZE,-60);
//		PlayerStats.player.transform.position = (Vector2)PlayerStats.camera.transform.position;
//	}

//	public void newAIStep()
//	{
//		for (int i = 0; i < GlobalData.units.Count; i++)
//		{
//			Unit Cur = GlobalData.units[i];
//			Cur.NextStep();
//		}
//	}
//	//Interaction logic
//	bool isPathEnable(int x, int y,Vector2 move_vector)
//	{
//		bool isEnable = true;
//		if (y >= GlobalData.map.Count || x >= GlobalData.map[y].Count)
//			isEnable = false;
//			/*
//			for(int i=0;i< GlobalData.Objects.Count;i++)
//			{
//				Object_ Cur = GlobalData.Objects[i];
//				int id = Cur.id;

//				if (Cur.x == x && Cur.y == y)
//				{
//					Cur.InteractMechanic(i,move_vector);

//					if (Cur.collision)
//					{
//						isEnable = false;
//					}
//				}
//			}

//			for (int i = 0; i < GlobalData.Items.Count; i++)
//			{
//				Item Cur = GlobalData.Items[i];
//				int id = Cur.id;
//				if (Cur.x == x && Cur.y == y)
//				{
//					if (!isEnable)
//						break;

//					Cur.InteractMechanic(i);
//					if (Cur.isDestroyable)
//						i--;//!!!! I remove element from Items, so i must increase -1 to save actual position(OR I miss several element)
//				}

//			}
//			if(isEnable)
//				MovePlayer(move_vector);
//			for (int i = 0; i < GlobalData.Units.Count; i++)
//			{
//				Unit Cur = GlobalData.Units[i];
//				int id = Cur.id;
//				if (Cur.x == x && Cur.y == y)
//				{
//					Cur.InteractMechanic(i,move_vector);
//					if (Cur.collision)
//					{
//						//isEnable = false;
//					}

//				}
//				Cur.NextStep();
//			}*/
//			//List<KeyValuePair<int, int>> cc = GlobalData.map[y][x];

//			for (int i=0;i< GlobalData.map[y][x].Count;i++)
//		{
//			switch (GlobalData.map[y][x][i].Key)
//			{
//				case 0:
//					Entity Cur0 = GlobalData.entities[GlobalData.map[y][x][i].Value];
//					Cur0.interactMechanic(move_vector);
//					if (Cur0.collision)
//					{
//						isEnable = false;
//					}
//					break;

//				case 1:
				
//					Unit Cur1 = GlobalData.units[GlobalData.map[y][x][i].Value];
//					if (Cur1.collision)
//					{
//						//isEnable = false;
//					}
					
//					break;

//				case 2:
//					Item Cur2 = GlobalData.items[GlobalData.map[y][x][i].Value];
//					if (!isEnable)
//							continue;
//					//if (Cur2.isDestroyable)
//					//	i--;//It's important. When we delete object from this position, we must add -1 for don't miss the element
//					Cur2.interactMechanic();
					
//					break;
//			}
//		}
//		if (isEnable)
//			movePlayer(move_vector);
//		newAIStep();
//		return isEnable;

//	}
//}
