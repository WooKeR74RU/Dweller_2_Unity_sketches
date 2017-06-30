using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour {

	public static int cur_block = 0;
	// Use this for initialization
	public static Object[] textures;
	public GameObject parent;
	void Start () {
		textures = Resources.LoadAll("Textures", typeof(Texture2D));
		GameObject g; Texture2D texture;
		for (int i=0;i<textures.Length;i++)
		{
			g = new GameObject();
			texture = (Texture2D)textures[i];
			int i1 = i;
			g.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetCurBlock(i1); });
			g.AddComponent<Image>().sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f),1);
			g.transform.parent = parent.transform;
			g.GetComponent<RectTransform>().localScale = new Vector3(1,1, 1);
		}
		g = new GameObject();
		texture = Resources.Load("dell") as Texture2D;
		g.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { SetCurBlock(textures.Length); });
		g.AddComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
		g.transform.parent = parent.transform;
	}
	void SetCurBlock(int i)
	{
		cur_block = i;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
