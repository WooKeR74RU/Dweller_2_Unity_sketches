using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour {

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
		int n = array.Length;
		while (n > 1)
		{
			int k = rng.Next(n--);
			T temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}

}
