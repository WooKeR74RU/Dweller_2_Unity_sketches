using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;

using UnityEngine.UI;
public class Map : MonoBehaviour {

	public static int x=18,y=10;
	public static List<List<GameObject>> mapa = new List<List<GameObject>>();
	public static GameObject grid ;
	public InputField SS;
	public static  List<List<List<KeyValuePair<string, Texture2D>>>> field = new List<List<List<KeyValuePair<string, Texture2D>>>>();
	
	public void Start()
	{
		
		Init();
	}
	public void BASE()
	{
		grid.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Convert.ToInt32(SS.text), Convert.ToInt32(SS.text));
	}
	static GenericDialog dialog;
	public static void ShowDialog(int x_,int y_)
	{
		
		int x = (int)Input.mousePosition.x;
		int y = (int)Input.mousePosition.y;
		dialog = new GenericDialog(x, y, 0, 0, GameObject.Find("UI"));
		TextView tv = new TextView(x, y, 0, 0, GameObject.Find("UI"));
		tv.panel.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		tv.panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		tv.text.GetComponent<UnityEngine.UI.Text>().color = Color.white;
		dialog.panel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		dialog.panel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
		dialog.panel.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		
		string text = "";
		for(int i=0;i<field[y_][x_].Count;i++){
			text += field[y_][x_][i].Key+",";
		}
		tv.SetText(text);
		dialog.AddText(tv);
	
		dialog.SetBackground(Color.black);
		dialog.panel.GetComponent<Image>().type = Image.Type.Sliced;
		if(text!="")
			dialog.SetPaddings(5, 5, 5, 5);
		
		dialog.Show();
		dialog.panel.transform.position = new Vector2((int)Input.mousePosition.x+5, (int)Input.mousePosition.y+5);
	}
	public static void CancelDialog()
	{
		dialog.Cancel();
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
		for(int i=0;i<field.Count;i++)
		{
			last2.Add(new List<List<KeyValuePair<string, Texture2D>>>());
			for (int j = 0; j < field[i].Count; j++)
			{
				last2[i].Add(new List<KeyValuePair<string, Texture2D>>());
				for (int k = 0; k < field[i][j].Count; k++)
				{
					last2[i][j].Add(new KeyValuePair<string, Texture2D>(field[i][j][k].Key, field[i][j][k].Value));
				}
			}
		}
		field = new List<List<List<KeyValuePair<string, Texture2D>>>>();
		mapa = new List<List<GameObject>>();
		
		for (int i = 0; i < y; i++)
		{
			mapa.Add(new List<GameObject>());
			field.Add(new List<List<KeyValuePair<string, Texture2D>>>());
			for (int j = 0; j < x; j++)
			{
				
				GameObject g = new GameObject();
				int i1 = i,j1 = j;
				g.AddComponent<Image>();

				EventTrigger trigger = g.AddComponent<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerEnter;
				entry.callback.AddListener((eventData) => { ShowDialog(j1,i1); });
				trigger.triggers.Add(entry);

				entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerExit;
				entry.callback.AddListener((eventData) => { CancelDialog(); });
				trigger.triggers.Add(entry);

				g.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetBlock(i1,j1); });
				g.transform.parent = grid.transform;
				g.transform.localScale = new Vector3(1, 1, 1);
				mapa[i].Add(g);
				field[i].Add(new List<KeyValuePair<string, Texture2D>>());
				if (last.Count > i && last[i].Count > j)
				{
					mapa[i][j].GetComponent<Image>().sprite = last[i][j];
					field[i][j].AddRange(last2[i][j]);
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
		for (int i=0;i<field.Count;i++)
		{
			bool check = false;
			for(int j=0;j<field[i].Count;j++)
			{
				string p = "{";
				if (field[i][j].Count == 0)
					continue;
				check = true;
				p += i + "," + j + ",";
				for(int k=0;k<field[i][j].Count;k++)
				{

					p += field[i][j][k].Key;
					
					if (k + 1 != field[i][j].Count)
						p += ",";
				}
				p += "}#";
				writer.Write(p);
			}
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
			field[i][j].Clear();
			return;
		}
		if(field[i][j].Count == 0)
		{
			Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
			mapa[i][j].GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
			field[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture));
		}
		else
		{
			Texture2D texture = Menu.textures[Menu.cur_block] as Texture2D;
			Texture2D texture2 = null;
			texture2 = CombineTextures(field[i][j][field[i][j].Count - 1].Value, texture);
			mapa[i][j].GetComponent<Image>().sprite = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f), 1);
			field[i][j].Add(new KeyValuePair<string, Texture2D>(Menu.textures[Menu.cur_block].name, texture2));

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
