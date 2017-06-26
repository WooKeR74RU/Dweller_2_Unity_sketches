using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

	public const int SPRITE_SIZE = 48;
	public static Dictionary<int, Texture2D> getTexureById = new Dictionary<int, Texture2D>();
	public static Dictionary<int, int> typeById = new Dictionary<int, int>();//0 - object,1 - unit,2 - item

	public static List<Entity> entities = new List<Entity>();
	public static List<Item> items = new List<Item>();
	public static List<Unit> units = new List<Unit>();
	public static List<List<List<KeyValuePair<int, int>>>> map = new List<List<List<KeyValuePair<int, int>>>>();
	//KeyValuePair pos in array and type
	void Start()
	{
		typeById.Add(100, 0);
		typeById.Add(101, 0);
		typeById.Add(102, 0);
		typeById.Add(13, 1);
		typeById.Add(103, 0);
		typeById.Add(104, 0);
		typeById.Add(105, 0);
		typeById.Add(106, 2);
		typeById.Add(0, 0);

		//нахер это говно(нет) (это важно, НЕ удалять)
		getTexureById.Add(100, Resources.Load("Textures/100") as Texture2D);
		getTexureById.Add(101, Resources.Load("Textures/101") as Texture2D);
		getTexureById.Add(102, Resources.Load("Textures/102") as Texture2D);
		//texture_by_id.Add(13, Resources.Load("Textures/EnemyAnimationFrame/RatAnimationFrame(13)/Rat1") as Texture2D);
		getTexureById.Add(103, Resources.Load("Textures/103") as Texture2D);
		getTexureById.Add(104, Resources.Load("Textures/104") as Texture2D);
		getTexureById.Add(105, Resources.Load("Textures/105") as Texture2D);
		getTexureById.Add(106, Resources.Load("Textures/106") as Texture2D);
		getTexureById.Add(0, Resources.Load("Textures/0") as Texture2D);
	}


	public static void addToMapNewObject(Item i, int Mapx, int Mapy)
	{
		for (int j = 0; j < items.Count; j++)
		{
			if (items[j].obj == null)
			{
				i.z = map[Mapy][Mapx].Count;
				items[j] = i;
				map[Mapy][Mapx].Add(new KeyValuePair<int, int>(2, j));

				return;
			}
		}
		i.z = map[Mapy][Mapx].Count;
		map[Mapy][Mapx].Add(new KeyValuePair<int, int>(2, items.Count));
		items.Add(i);
		return;
	}
	public static void addToMapNewObject(Entity o, int Mapx, int Mapy)
	{
		for (int j = 0; j < entities.Count; j++)
		{
			if (entities[j].obj == null)
			{
				o.z = map[Mapy][Mapx].Count;
				entities[j] = o;
				map[Mapy][Mapx].Add(new KeyValuePair<int, int>(0, j));
				return;
			}
		}
		o.z = map[Mapy][Mapx].Count;
		map[Mapy][Mapx].Add(new KeyValuePair<int, int>(0, entities.Count));
		entities.Add(o);
		return;
	}
	public static void addToMapNewObject(Unit u, int Mapx, int Mapy)
	{
		for (int j = 0; j < units.Count; j++)
		{
			if (units[j].obj == null)
			{
				u.z = map[Mapy][Mapx].Count;
				units[j] = u;
				map[Mapy][Mapx].Add(new KeyValuePair<int, int>(1, j));
				return;
			}
		}
		u.z = map[Mapy][Mapx].Count;
		map[Mapy][Mapx].Add(new KeyValuePair<int, int>(1, units.Count));
		units.Add(u);
		return;
	}
	public static void addToMap(int type, int pos, int x, int y)
	{
		map[y][x].Add(new KeyValuePair<int, int>(type, pos));
	//	ChangeZ(map[y][x][map[y][x].Count - 1], map[y][x].Count - 1);
	}
	public static void removeFromMap(int x, int y, int z)
	{
		map[y][x].RemoveAt(z);
		for (int i = 0; i < map[y][x].Count; i++)
			changeZ(map[y][x][i], i);
	}

	public static void changeZ(KeyValuePair<int, int> a, int z)
	{
		switch (a.Key)
		{
			case 0:
				entities[a.Value].z = z;
				break;

			case 1:
				units[a.Value].z = z;
				break;

			case 2:
				items[a.Value].z = z;
				break;
		}
	}
}
