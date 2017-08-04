using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;

using UnityEngine.UI;
public class Map : MonoBehaviour
{

	public static int x=18,y=10;
	public static List<List<List<GameObject>>> mapa = new List<List<List<GameObject>>>();
	public static GameObject grid ;
	public InputField cellSize;
	public static  List<List<List<KeyValuePair<string, Texture2D>>>> field = new List<List<List<KeyValuePair<string, Texture2D>>>>();
	
	public void Start()
	{
		
		Init();
		BASE();
	}

	public void BASE()
	{
		grid.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Convert.ToInt32(cellSize.text), Convert.ToInt32(cellSize.text));
	}


	public static void Init()
	{
		
		grid = GameObject.Find("map");
		grid.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.GetComponent<GridLayoutGroup>().constraintCount = x;
		foreach (Transform child in grid.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		List<List<List<GameObject>>> lastMapa = new List<List<List<GameObject>>>(mapa);
		List<List<List<KeyValuePair<string, Texture2D>>>> lastField = new List<List<List<KeyValuePair<string, Texture2D>>>>(field);
		field = new List<List<List<KeyValuePair<string, Texture2D>>>>();
		mapa = new List<List<List<GameObject>>>();
		
		for (int i = 0; i < y; i++)
		{
			mapa.Add(new List<List<GameObject>>());
			field.Add(new List<List<KeyValuePair<string, Texture2D>>>());
			for (int j = 0; j < x; j++)
			{
				mapa[i].Add(new List<GameObject>());
				field[i].Add(new List<KeyValuePair<string, Texture2D>>());
				if (lastField.Count > i && lastField[i].Count > j && lastField[i][j].Count > 0)
				{

					field[i][j] = new List<KeyValuePair<string,Texture2D>>(lastField[i][j]);
					//mapa[i][j] = new List<GameObject>(mapa[i][j]);
					GameObject g = getBlockGameObject(j, i, "del");
					g.GetComponent<Image>().sprite = CreateSprite(i, j);
					mapa[i][j].Add(g);
				}
				else
				{
					GameObject g = getBlockGameObject(j, i, "del");
					mapa[i][j].Add(g);
					field[i][j].Add(new KeyValuePair<string, Texture2D>("del",Menu.texturesByName["del"]));



				}
			}
			
		}
	}
	static GameObject getBlockGameObject(int x,int y,string name)
	{
		GameObject g = new GameObject();
		Texture2D t = Menu.texturesByName["del"];
		g.AddComponent<Image>().sprite = Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0.5f,0.5f),1);
		int x1 = x, y1 = y;
		g.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetBlock(y1, x1); });
		g.AddComponent<BlockInfo>().x = x;
		g.GetComponent<BlockInfo>().y = y;
		g.transform.parent = grid.transform;
		g.transform.localScale = new Vector3(1, 1, 1);
		return g;
	}
	public void WriteString()
	{
		string path = Application.dataPath + "/test.txt";
		StreamWriter writer = new StreamWriter(path, false);
		bool flag = false;
		for (int i = 0; i < field.Count; i++)
		{
			for (int j = 0; j < field[i].Count; j++)
			{
				if (field[i][j].Count == 0)
					continue;
				string curDesc = null;
				if (!flag)
					flag = true;
				else
					curDesc += "\n";
				curDesc += j + "x" + (field.Count - 1 - i) + " ";
				for (int k = 0; k < field[i][j].Count; k++)
				{
					curDesc += field[i][j][k].Key;
					if (k + 1 != field[i][j].Count)
						curDesc += " ";
				}
				writer.Write(curDesc);
			}
		}
		writer.Close();
	}

	static void SetBlock(int i,int j)
	{
		string name = Menu.cur_block.ToString();
		if (name.Equals("del"))
		{
			for(int k=0;k<mapa[i][j].Count;k++)
				mapa[i][j][k].GetComponent<Image>().sprite = null;
			field[i][j].Clear();
			return;
		}
		Texture2D texture = Menu.texturesByName[name];
		field[i][j].Add(new KeyValuePair<string, Texture2D>(name, texture));
		//mapa[i][j].Add(getBlockGameObject(j,i, mapa[i][j].Count));
		mapa[i][j][0].GetComponent<Image>().sprite = CreateSprite(i, j);

		//if (field[i][j].Count == 0)
		//{
		//	Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
		//	mapa[i][j].GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
		//	field[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture));
		//}
		//else
		//{
		//	Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
		//	Texture2D texture2 = null;
		//	texture2 = CombineTextures(field[i][j][field[i][j].Count - 1].Value, texture);
		//	mapa[i][j].GetComponent<Image>().sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f), 1);
		//	field[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture2));

		//}
	}
	public static Sprite CreateSprite(int i,int j)
	{
		
		Texture2D texture2 = Menu.texturesByName["0"] as Texture2D; ;
		for (int k=0;k<field[i][j].Count;k++)
		{
			Texture2D texture = field[i][j][k].Value;
			texture2 = CombineTextures(texture2, texture);
		}
		return Sprite.Create(texture2,new Rect(0,0,texture2.width,texture2.height),new Vector2(0.5f,0.5f),1);
	}
	public static Texture2D CombineTextures( Texture2D Background, Texture2D Overlay)
	{
		Texture2D newTexture;
		
		newTexture = new Texture2D(Background.width, Background.height, TextureFormat.ARGB32, false);
		newTexture.filterMode = FilterMode.Point;
		var offset = new Vector2(((newTexture.width - Overlay.width) / 2), ((newTexture.height - Overlay.height) / 2));
		
		newTexture.SetPixels(Background.GetPixels());

		for (int y = 0; y < Overlay.height; y++){
			for (int x = 0; x < Overlay.width; x++){
				var PixelColorFore = Overlay.GetPixel(x, y) * Overlay.GetPixel(x, y).a;
				var PixelColorBack = newTexture.GetPixel(x + (int)offset.x, y + (int)offset.y) * (1 - PixelColorFore.a);
				newTexture.SetPixel(x + (int)offset.x, y + (int)offset.y, PixelColorBack + PixelColorFore);
			}
		}

		newTexture.Apply();
		return newTexture;
	}
}
