using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalData : MonoBehaviour
{
	public const int SPRITE_SIZE = 48;

	const int ENTITIES_ID_FROM = 1;
	const int ENTITIES_ID_TO = 1000;
	const int UNITS_ID_FROM = 1001;
	const int UNITS_ID_TO = 2000;
	const int ITEMS_ID_FROM = 2001;
	const int ITEMS_ID_TO = 3000;

	//0 - enity, 1 - unit, 2 - item
	public static Dictionary<int, Texture2D> getTexureById = new Dictionary<int, Texture2D>();
	public static Dictionary<int, int> typeById = new Dictionary<int, int>();

	public static List<Entity> entities = new List<Entity>();
	public static List<Item> items = new List<Item>();
	public static List<Unit> units = new List<Unit>();
	public static List<List<List<Pair<int, int>>>> field = new List<List<List<Pair<int, int>>>>(); //type, id in array of type

	private void Start()
	{
		//нахер это говно(нет) (это важно, НЕ удалять)

		for (int i = ENTITIES_ID_FROM; i <= ENTITIES_ID_TO; i++)
		{
			try
			{
				typeById.Add(i, 0);
				getTexureById.Add(i, Resources.Load("Textures/EntitiesTextures/" + i.ToString()) as Texture2D);
			}
			catch
			{
				Debug.Log(i.ToString() + " file doesn't exist");
			}
		}
		for (int i = UNITS_ID_FROM; i <= UNITS_ID_TO; i++)
		{
			try
			{
				typeById.Add(i, 1);
				getTexureById.Add(i, Resources.Load("Textures/UnitsTextures/" + i.ToString()) as Texture2D);
			}
			catch
			{
				Debug.Log(i.ToString() + " file doesn't exist");
			}
		}
		for (int i = ITEMS_ID_FROM; i <= ITEMS_ID_TO; i++)
		{
			try
			{
				typeById.Add(i, 2);
				getTexureById.Add(i, Resources.Load("Textures/ItemsTextures/" + i.ToString()) as Texture2D);
			}
			catch
			{
				Debug.Log(i.ToString() + " file doesn't exist");
			}
		}
	}

	public static void addNewObject(Entity entity)
	{
		for (int i = 0; i < entities.Count; i++)
		{
			if (entities[i].gameObj == null)
			{
				entities[i] = entity;
				return;
			}
		}
		entities.Add(entity);
		return;
	}
	public static void addNewObject(Unit unit)
	{
		for (int i = 0; i < units.Count; i++)
		{
			if (units[i].gameObj == null)
			{
				units[i] = unit;
				return;
			}
		}
		units.Add(unit);
		return;
	}
	public static void addNewObject(Item item)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].gameObj == null)
			{
				items[i] = item;
				return;
			}
		}
		items.Add(item);
		return;
	}

	public static void addToField(int type, int idInArrOfType, int x, int y)
	{
		field[y][x].Add(new Pair<int, int>(type, idInArrOfType));
	}
	public static void removeFromField(int x, int y, int z)
	{
		field[y][x].RemoveAt(z);
		for (int i = 0; i < field[y][x].Count; i++)
			changeZ(field[y][x][i], i);
	}
	public static void changeZ(Pair<int, int> a, int z)
	{
		if (a.x == 0)
			entities[a.y].z = z;
		if (a.x == 1)
			units[a.y].z = z;
		if (a.x == 2)
			items[a.y].z = z;
	}
}