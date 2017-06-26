using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;

using UnityEngine.UI;
public class Map : MonoBehaviour {

	public static int x=18,y=10;
	public static List<List<GameObject>> mapa = new List<List<GameObject>>();
	public static GameObject grid ;
	public InputField SS;
	public static  List<List<List<KeyValuePair<string, Texture2D>>>> m = new List<List<List<KeyValuePair<string, Texture2D>>>>();
	public void Start()
	{
		
		Init();
	}
	public void BASE()
	{
		grid.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Convert.ToInt32(SS.text),Convert.ToInt32(SS.text));
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
		//grid.GetComponent<GridLayoutGroup>()
		List<List<Sprite>> last = new List<List<Sprite>>();
		List<List<List<KeyValuePair<string, Texture2D>>>> last2 = new List<List<List<KeyValuePair<string, Texture2D>>>>();
		for (int i = 0; i < mapa.Count; i++)
		{
			last.Add(new List<Sprite>());
			for (int j = 0; j < mapa[i].Count; j++)
			{
				last[i].Add(mapa[i][j].GetComponent<Image>().sprite);
			}
		
		}
		for(int i=0;i<m.Count;i++)
		{
			last2.Add(new List<List<KeyValuePair<string, Texture2D>>>());
			for (int j = 0; j < m[i].Count; j++)
			{
				last2[i].Add(new List<KeyValuePair<string, Texture2D>>());
				for (int k = 0; k < m[i][j].Count; k++)
				{
					last2[i][j].Add(new KeyValuePair<string, Texture2D>(m[i][j][k].Key, m[i][j][k].Value));
				}
			}
		}
		m = new List<List<List<KeyValuePair<string, Texture2D>>>>();
		mapa = new List<List<GameObject>>();
		for (int i = 0; i < y; i++)
		{
			mapa.Add(new List<GameObject>());
			m.Add(new List<List<KeyValuePair<string, Texture2D>>>());
			for (int j = 0; j < x; j++)
			{
				
				GameObject g = new GameObject();
				int i1 = i,j1 = j;
				g.AddComponent<Image>();
				g.AddComponent<Button>().onClick.AddListener(delegate { SetBlock(i1,j1); });
				g.transform.parent = grid.transform;
				g.transform.localScale = new Vector3(1, 1, 1);
				mapa[i].Add(g);
				m[i].Add(new List<KeyValuePair<string, Texture2D>>());
				if (last.Count > i && last[i].Count > j)
				{
					mapa[i][j].GetComponent<Image>().sprite = last[i][j];
					m[i][j].AddRange(last2[i][j]);
				}

			}
			
		}
	}
	public  void WriteString()
	{
		string path = Application.dataPath+"/test.txt";
		File.WriteAllBytes(path, new byte[0]);
		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter(path, true);
		for (int i=0;i<m.Count;i++)
		{
			for(int j=0;j<m[i].Count;j++)
			{
				string p = "{";
				for(int k=0;k<m[i][j].Count;k++)
				{

					p += m[i][j][k].Key;
					
					if (k + 1 != m[i][j].Count)
						p += ",";
				}
				if (m[i][j].Count == 0)
					p += "Null";
				p += "}";
				if (j + 1 != m[i].Count)
					p += "#";
				writer.Write(p);
			}
			writer.Write("\r\n");
		}
		//writer.WriteLine("Test");
		writer.Close();

		//Re-import the file to update the reference in the editor
		//AssetDatabase.ImportAsset(path);
		//TextAsset asset = Resources.Load("test") as TextAsset;

		//Print the text from the file
		//Debug.Log(asset.text);
	}
	static void SetBlock(int i,int j)
	{
		if(Menu.cur_block == Menu.textures.Length)
		{
			mapa[i][j].GetComponent<Image>().sprite = null;
			m[i][j].Clear();
			return;
		}
		if(m[i][j].Count == 0)
		{
			Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
			mapa[i][j].GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
			m[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture));
		}
		else
		{
			Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
			Texture2D texture2 = null;
			texture2 = CombineTextures(m[i][j][m[i][j].Count - 1].Value, texture);
			mapa[i][j].GetComponent<Image>().sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f), 1);
			m[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture2));

		}
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
