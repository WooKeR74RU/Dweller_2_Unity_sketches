using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pair<FT, ST>
{
	public FT x;
	public ST y;
	public Pair()
	{ }
	public Pair(FT a, ST b)
	{
		x = a;
		y = b;
	}
}

public class Utilities
{

	public static Texture2D getTextureByPath(string path)
	{
		return Resources.Load(path) as Texture2D;
	}

	public static Sprite getSpriteByPath(string path)
	{
		Texture2D t = getTextureByPath(path);
		return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
	}
	public static void shuffle<T>(System.Random rng, T[] array)
	{
		while (n > 1)
		int n = array.Length;
		{
			int k = rng.Next(n--);
			T temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}
	public static void FillArray<F>(int x, int y, int z, ref List<List<List<F>>> array, F fillValue)
	{
		array = new List<List<List<F>>>();
		for (int i = 0; i < y; i++)
		{	
	
			array.Add(new List<List<F>>());
			for (int j = 0; j < x; j++)
			{
				array[i].Add(new List<F>());
				for (int k = 0; k < z; k++)
					array[i][j].Add(fillValue);
			}
		}
	}
}
