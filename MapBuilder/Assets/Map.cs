using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using System.Windows.Forms;
using System.Threading;
using UniRx;
using UniRx.Triggers;

public struct item
{
	public int x0, y0;
	public int id;
	public item(int x0, int y0, int id)
	{
		this.x0 = x0;
		this.y0 = y0;
		this.id = id;
	}
}
public class Map : MonoBehaviour
{

	public static int segmentCountX = 2, segmentCountY = 2;
	public static int segmentHeight = 30, segmentWidth = 58;
	public static int SPRITE_SIZE = 24;
	public static Dictionary<string, Texture2D> mapTexture = new Dictionary<string, Texture2D>();
	public static GameObject map;
	public static Dictionary<string, List<List<List<item>>>> beginFullMap = new Dictionary<string, List<List<List<item>>>>();
	public static GameObject UI;
	//public static List<List<List<int>>> field = new List<List<List<int>>>();
	public void Start()
	{
		UI = GameObject.Find("UI");
		initMainMap(true);
	}

	public static void DrawTexture(int x1, int y1, int id)
	{
		if (id == 0)
		{
			DeleteBlock(x1, y1);
			return;
		}
		try
		{
			Texture2D t = Menu.texturesByName[Menu.textureNameById[id]];
			HashSet<string> segments = new HashSet<string>();
			for (int i = 0; i < t.height; i++)
			{
				for (int j = 0; j < t.width; j++)
				{

					int x_ = x1 * SPRITE_SIZE + j, y_ = y1 * SPRITE_SIZE + i;
					string name = ((x1 + j / SPRITE_SIZE) / segmentWidth) + "x" + ((y1 + i / SPRITE_SIZE) / segmentHeight);
					if (((y1 * SPRITE_SIZE + i)) % SPRITE_SIZE == 0 && ((x1 * SPRITE_SIZE + j)) % SPRITE_SIZE == 0)
					{
						beginFullMap[name][(y1 + i / SPRITE_SIZE) % segmentHeight][(x1 + j / SPRITE_SIZE) % segmentWidth].Add(new item(x1, y1, id));
					}
					x_ = x_ % (segmentWidth * SPRITE_SIZE);
					y_ = y_ % (segmentHeight * SPRITE_SIZE);
					Texture2D mainMap = mapTexture[name];
					segments.Add(name);
					var PixelColorFore = t.GetPixel(j, i) * t.GetPixel(j, i).a;
					var PixelColorBack = mainMap.GetPixel(x_, y_) * (1 - PixelColorFore.a);
					mainMap.SetPixel(x_, y_, PixelColorBack + PixelColorFore);

				}
			}
			foreach (string s in segments)
			{
				mapTexture[s].Apply();
				Texture2D mainMap = mapTexture[s];
				map.GetComponent<Image>().sprite = Sprite.Create(mainMap, new Rect(0, 0, mainMap.width, mainMap.height), new Vector2(0, 0), 1);
			}
			string seg = ((x1) / segmentWidth) + "x" + ((y1) / segmentHeight);
			//beginFullMap[seg][y1][x1].Add(new item(x1, y1, id));

		}
		catch
		{

		}
	}
	static void DeleteBlock(int x1, int y1)
	{
		string name = (x1 / segmentWidth) + "x" + (y1 / segmentHeight);
		int x_ = x1 % segmentWidth;
		int y_ = y1 % segmentHeight;
		for (int i = 0; i < beginFullMap[name][y_][x_].Count; i++)
		{
			if (beginFullMap[name][y_][x_][i].x0 == x1 && beginFullMap[name][y_][x_][i].y0 == y1)
			{
				int id = beginFullMap[name][y_][x_][i].id;
				//beginFullMap[name][y_][x_].RemoveAt(i);
				DeleteItem(x1, y1, id);
				i--;
			}
		}
	}
	static void DeleteItem(int x1, int y1, int id)
	{
		HashSet<string> segments = new HashSet<string>();
		Texture2D t = Menu.texturesByName[Menu.textureNameById[id]];
		int tWidth = t.width;
		int tHeight = t.height;
		int sX = x1, sY = y1;
		for (int i = sY; i < sY + Math.Ceiling((double)tHeight / SPRITE_SIZE); i++)
		{
			for (int j = sX; j < sX + Math.Ceiling((double)tWidth / SPRITE_SIZE); j++)
			{
				string name = ((j) / segmentWidth) + "x" + ((i) / segmentHeight);
				segments.Add(name);
				int curx = (j) % segmentWidth;
				int cury = (i) % segmentHeight;
				drawRect(Menu.texturesByName["del"], j, i, new Rect(0, 0, SPRITE_SIZE, SPRITE_SIZE));
				for (int k = 0; k < beginFullMap[name][cury][curx].Count; k++)
				{
					if (beginFullMap[name][cury][curx][k].x0 == x1 && beginFullMap[name][cury][curx][k].y0 == y1 && beginFullMap[name][cury][curx][k].id == id)
					{
						beginFullMap[name][cury][curx].RemoveAt(k);
						k--;
						continue;
					}
					int x0 = beginFullMap[name][cury][curx][k].x0, y0 = beginFullMap[name][cury][curx][k].y0;
					x0 = j - x0; y0 = i - y0;
					x0 *= SPRITE_SIZE;
					y0 *= SPRITE_SIZE;
					drawRect(Menu.texturesByName[Menu.textureNameById[beginFullMap[name][cury][curx][k].id]], j, i, new Rect(x0, y0, SPRITE_SIZE, SPRITE_SIZE));
				}
			}
		}
		foreach (string s in segments)
		{
			mapTexture[s].Apply();
			Texture2D mainMap = mapTexture[s];
			map.GetComponent<Image>().sprite = Sprite.Create(mainMap, new Rect(0, 0, mainMap.width, mainMap.height), new Vector2(0, 0), 1);
		}
	}
	static void drawRect(Texture2D t, int x1, int y1, Rect rect)
	{
		string name = ((x1) / segmentWidth) + "x" + ((y1) / segmentHeight);
		Texture2D mainMap = mapTexture[name];
		for (int i = 0; i < SPRITE_SIZE; i++)
		{
			for (int j = 0; j < SPRITE_SIZE; j++)
			{
				if ((int)rect.x + j >= t.width || (int)rect.y + i >= t.height)
					continue;
				int x_ = (x1 % segmentWidth) * SPRITE_SIZE + j;
				int y_ = (y1 % segmentHeight) * SPRITE_SIZE + i;
				var PixelColorFore = t.GetPixel((int)rect.x + j, (int)rect.y + i) * t.GetPixel((int)rect.x + j, (int)rect.y + i).a;
				var PixelColorBack = mainMap.GetPixel(x_, y_) * (1 - PixelColorFore.a);
				mainMap.SetPixel(x_, y_, PixelColorBack + PixelColorFore);
			}
		}
	}
	//public static void rebuildTextureByField()
	//{
	//	for(int i=0;i<y;i++)
	//	{
	//		for(int j=0;j<x;j++)
	//		{
	//			for(int k=0;k<field[i][j].Count;k++)
	//			{
	//				Texture2D t = Menu.texturesByName[Menu.textureNameById[field[i][j][k]]];
	//				int x_ = j * SPRITE_SIZE + j, y_ = y1 * SPRITE_SIZE + i;
	//				var PixelColorFore = t.GetPixel(j, i) * t.GetPixel(j, i).a;
	//				var PixelColorBack = mainMap.GetPixel(x_, y_) * (1 - PixelColorFore.a);
	//				mainMap.SetPixel(x_, y_, PixelColorBack + PixelColorFore);
	//			}
	//		}
	//	}
	//}

	public static void createSegment(string name)
	{
		beginFullMap[name] = new List<List<List<item>>>();
		Texture2D tmp = null;

		Texture2D curT = null;
		if (mapTexture.ContainsKey(name))
			curT = mapTexture[name];
		if (curT != null)
			tmp = Instantiate(curT);
		curT = new Texture2D(segmentWidth * SPRITE_SIZE, segmentHeight * SPRITE_SIZE, TextureFormat.ARGB32, false);
		curT.filterMode = FilterMode.Point;
		if (tmp != null)
		{
			for (int i = 0; i < Math.Min(tmp.height, segmentHeight * SPRITE_SIZE); i++)
			{
				beginFullMap[name].Add(new List<List<item>>());


				for (int j = 0; j < Math.Min(tmp.width, segmentWidth * SPRITE_SIZE); j++)
				{
					beginFullMap[name][i].Add(new List<item>());
					var color = tmp.GetPixel(j, i);
					curT.SetPixel(j, i, color);
				}
			}
		}
		curT.Apply();
		for (int i = beginFullMap[name].Count; i < segmentHeight; i++)
		{
			beginFullMap[name].Add(new List<List<item>>());
			for (int j = beginFullMap[name][i].Count; j < segmentWidth; j++)
			{
				beginFullMap[name][i].Add(new List<item>());
			}
		}
		mapTexture[name] = curT;
	}
	public static void initMainMap(bool saveChanges)
	{
		map = GameObject.Find("MapTexure");
		int childs = map.transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(map.transform.GetChild(i).gameObject);
		}

		float scaleFactor = UI.GetComponent<RectTransform>().localScale.x;
		beginFullMap.Clear();
		map.GetComponent<RectTransform>().sizeDelta = new Vector2(SPRITE_SIZE * segmentCountX * segmentWidth, SPRITE_SIZE * segmentCountY * segmentHeight);
		for (int i = 0; i < segmentCountY; i++)
		{
			for (int j = 0; j < segmentCountX; j++)
			{
				string name = j + "x" + i;
				if(saveChanges)
					createSegment(name);
					else
				{
					beginFullMap[name] = new List<List<List<item>>>();
					Texture2D curT = new Texture2D(segmentWidth * SPRITE_SIZE, segmentHeight * SPRITE_SIZE, TextureFormat.ARGB32, false);
					curT.filterMode = FilterMode.Point;
					mapTexture[name] = curT;
					for (int p = 0; p <segmentHeight; p++)
					{
						beginFullMap[name].Add(new List<List<item>>());


						for (int d = 0; d < segmentWidth ; d++)
						{
							beginFullMap[name][p].Add(new List<item>());
						}
					}
				}
				GameObject segment = new GameObject(name);
				segment.transform.parent = map.transform;
				segment.AddComponent<RectTransform>().pivot = new Vector2(0, 0);
				segment.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
				segment.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

				segment.GetComponent<RectTransform>().transform.localPosition = new Vector2(j * segmentWidth * SPRITE_SIZE, i * segmentHeight * SPRITE_SIZE);
				segment.GetComponent<RectTransform>().sizeDelta = new Vector2(segmentWidth * SPRITE_SIZE, segmentHeight * SPRITE_SIZE);
				Texture2D t = mapTexture[name];
				segment.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0), 1);
				//segment.transform.position = new Vector2(j * segmentWidth * SPRITE_SIZE, i * segmentHeight * SPRITE_SIZE + 120 * scaleFactor);
				segment.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);

			}
		}

	}
	public void createMap()
	{
		int countX = (int)Math.Ceiling((double)segmentCountX / segmentWidth), countY = (int)Math.Ceiling((double)segmentCountY / segmentHeight);
		for (int i = 0; i < segmentCountY; i++)
		{
			for (int j = 0; j < segmentCountX; j++)
			{
				WriteString(j + "x" + i, j * segmentWidth, i * segmentHeight, segmentHeight, segmentWidth);
			}
		}
	}
	public void WriteString(string name, int x1, int y1, int h, int w)
	{
		List<List<List<item>>> field = beginFullMap[name];
		string path = UnityEngine.Application.dataPath + "/map/" + name + ".txt";

		if (!Directory.Exists(UnityEngine.Application.dataPath + "/map"))
		{
			DirectoryInfo di = Directory.CreateDirectory(UnityEngine.Application.dataPath + "/map");

		}
		StreamWriter writer = new StreamWriter(path, false);
		bool flag = false;
		for (int i = y1; i < y1 + h; i++)
		{
			for (int j = x1; j < x1 + w; j++)
			{
				int curx = j % segmentWidth;
				int cury = i % segmentHeight;
				if (cury >= field.Count || curx >= field[cury].Count)
					continue;
				if (field[cury][curx].Count == 0)
					continue;
				string curDesc = null;
				if (!flag)
					flag = true;
				else
					curDesc += "\n";
				bool add = false;
				curDesc += (curx) + "x" + (h - cury - 1) + " ";
				for (int k = 0; k < field[cury][curx].Count; k++)
				{
					if (field[cury][curx][k].x0 != j || field[cury][curx][k].y0 != i)
						continue;
					curDesc += field[cury][curx][k].id;
					add = true;
					if (k + 1 != field[cury][curx].Count)
						curDesc += " ";
				}
				if (add)
					writer.Write(curDesc);
			}
		}
		writer.Close();
	}
	//   public void CreateTexure()
	//   {
	//	Texture2D t = new Texture2D(x * SPRITE_SIZE, y * SPRITE_SIZE);

	//	for (int i=0;i<y;i++)
	//	{
	//		for(int j=0;j<x;j++)
	//		{
	//			for(int k=0;k< SPRITE_SIZE; k++)
	//			{
	//				for(int l=0;l< SPRITE_SIZE; l++)
	//				{
	//					for (int h = 0; h < field[i][j].Count; h++)
	//					{

	//						var color = field[i][j][h].Value.GetPixel(k, l) * field[i][j][h].Value.GetPixel(k, l).a;
	//						t.SetPixel(j * SPRITE_SIZE + l, i * SPRITE_SIZE + k, color);
	//					}
	//				}
	//			}
	//		}
	//	}
	//	SaveTextureToFile(t,"dedede.png");
	//	Debug.Log("DONE");
	//	//string path = Application.dataPath + "/" + "image" + ".png";
	//	//StreamWriter writer = new StreamWriter(path, false);
	//	//writer.Write(t);
	//	//writer.Close();
	//}
	public IEnumerator DrawMap(string folderPath)
	{
		for (int i = 0; i < segmentCountY; i++)
		{
			for (int j = 0; j < segmentCountX; j++)
			{
				String file;
				using (StreamReader sr = new StreamReader(folderPath + "\\" + j + "x" + i + ".txt"))
				{
					file = sr.ReadToEnd().Replace("\r", "");
				
				}
				string[] cells = file.Split('\n');
				for (int k = cells.Length - 1; k >= 0; k--)
					{
						string cell = cells[k];
						string[] el = cell.Split(' ');
						int x_ = Convert.ToInt32(el[0].Split('x')[0]);
						int y_ = Convert.ToInt32(el[0].Split('x')[1]);
						for (int l = 1; l < el.Length; l++)
						{
							int id = Convert.ToInt32(el[l]);
							DrawTexture(x_, y_, id);
							yield return null;
						}
					}
			}
		}
		yield return null;
	}
	bool isloadMapEnable = false;
	string selectedFolder;
	public void Update()
	{
		if(isloadMapEnable)
		{
			StartCoroutine(DrawMap(selectedFolder));
			isloadMapEnable = false;
		}
	}
	IEnumerator step(int x_,int y_,int id)
	{
		DrawTexture(x_, y_, id);
		yield return null;
	}
	public void loadMap()
	{

		System.Windows.Forms.FolderBrowserDialog openFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			selectedFolder = openFileDialog1.SelectedPath;
		}
		using (StreamReader sr = new StreamReader(selectedFolder + "\\description.txt"))
		{
			String file = sr.ReadToEnd().Replace("\r", "");
			string[] el = file.Split('\n');
			string matrixSize = el[0].Split(' ')[1];
			string segmentSize = el[1].Split(' ')[1];
			segmentHeight = Convert.ToInt32(segmentSize.Split('x')[1]);
			segmentWidth = Convert.ToInt32(segmentSize.Split('x')[0]);
			segmentCountX = Convert.ToInt32(matrixSize.Split('x')[0]);
			segmentCountY = Convert.ToInt32(matrixSize.Split('x')[1]);

		}
		initMainMap(false);
		isloadMapEnable = true;
	}

	static void SaveTextureToFile(Texture2D texture, string fileName)
	{
		var bytes = texture.EncodeToPNG();
		var file = File.Open(UnityEngine.Application.dataPath + "/" + fileName, FileMode.Create);
		var binary = new BinaryWriter(file);
		binary.Write(bytes);
		file.Close();
	}
}