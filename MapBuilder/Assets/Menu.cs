using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public static int cur_block = 0;
    public static GameObject InfoPanel;
	public static Dictionary<string, Texture2D> texturesByName = new Dictionary<string, Texture2D>();
    public static Dictionary<int, string> objectNameById = new Dictionary<int, string>();
    public static Dictionary<string, int> objectIdByName = new Dictionary<string, int>();
    public static Dictionary<int, string> textureNameById = new Dictionary<int, string>();
    public GameObject parent;
	void Start () {
        loadObjectsList();
        texturesByName["del"] = Resources.Load("Textures/del") as Texture2D;
        texturesByName["transparent"] = Resources.Load("Textures/transparent") as Texture2D;

    }
	void SetCurBlock(string name)
	{
		cur_block = objectIdByName[name];
	}
    public void initMenuComponent(string textureName,string objectName)
    {
        GameObject g; Texture2D texture;
        g = new GameObject();
        texture = Resources.Load("Textures/"+ textureName) as Texture2D;
        g.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetCurBlock(objectName); });
        g.AddComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
        g.transform.parent = parent.transform;
        g.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        texturesByName[textureName] = texture;
    }
    public void loadObjectsList()
    {
        string path = "ObjectsList";
        string text = (Resources.Load(path) as TextAsset).text;
        text = text.Replace("\r", "").Replace(".", "").Replace("-", "").Replace("  ", " ");
        string[] parameters = text.Split(' ', '\n');
        for (int i = 0; i < parameters.Length; i += 3)
        {
            int id = Convert.ToInt32(parameters[i]);
            string objectName = parameters[i + 1];
            string textureName = parameters[i + 2];

            try
            {
                initMenuComponent(textureName, objectName);
            }
            catch(Exception)
            {
                Debug.Log("Not define object "+id+ " "+objectName);
            }
            objectNameById[id] = objectName;
            textureNameById[id] = textureName;
            objectIdByName[objectName] = id;
        }
    }

	public string OpenFileDialog(string itemPath)
	{
		System.Windows.Forms.FolderBrowserDialog openFileDialog1 = new System.Windows.Forms.FolderBrowserDialog();

		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			return openFileDialog1.SelectedPath;
		}
		return "";
	}


}
