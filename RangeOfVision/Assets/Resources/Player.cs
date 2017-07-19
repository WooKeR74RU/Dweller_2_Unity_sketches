using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Player : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	public InputField inp;
	int range = 5;
	// Update is called once per frame
	void Update () {
		range = Convert.ToInt32(inp.text);
		
		if (Input.GetKeyDown(KeyCode.W))
		{
			if(GenerateMap.mapa[(int)(gameObject.transform.position.y+48)/48][(int)(gameObject.transform.position.x)/48])
				return;
			try
			{
				foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Range"))
				{
					if (fooObj.name == "Range")
					{
						GameObject.Destroy(fooObj);
					}
				}
			}
			catch { }
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 48);
			FOV f = new FOV();
			f.updateView(range, (int)(this.gameObject.transform.position.x / 48), (int)(this.gameObject.transform.position.y / 48));

			f.getView();
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (GenerateMap.mapa[(int)gameObject.transform.position.y / 48][(int)(gameObject.transform.position.x - 48) / 48])
				return;
			try
			{
				foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Range"))
				{
					if (fooObj.name == "Range")
					{
						GameObject.Destroy(fooObj);
					}
				}
			}
			catch { }
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - 48, this.gameObject.transform.position.y);
			FOV f = new FOV();
			f.updateView(range, (int)(this.gameObject.transform.position.x / 48), (int)(this.gameObject.transform.position.y / 48));

			f.getView();
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			if (GenerateMap.mapa[(int)(gameObject.transform.position.y ) / 48][(int)(gameObject.transform.position.x + 48) / 48])
				return;
			try
			{
				foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Range"))
				{
					if (fooObj.name == "Range")
					{
						GameObject.Destroy(fooObj);
					}
				}
			}
			catch { }
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + 48, this.gameObject.transform.position.y);
			FOV f = new FOV();
			f.updateView(range, (int)(this.gameObject.transform.position.x / 48), (int)(this.gameObject.transform.position.y / 48));

			f.getView();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			if (GenerateMap.mapa[(int)(gameObject.transform.position.y - 48) / 48][(int)(gameObject.transform.position.x) / 48])
				return;
			try
			{
				foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Range"))
				{
					if (fooObj.name == "Range")
					{
						GameObject.Destroy(fooObj);
					}
				}
			}
			catch { }
			this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 48);
			FOV f = new FOV();
			f.updateView(range, (int)(this.gameObject.transform.position.x / 48), (int)(this.gameObject.transform.position.y / 48));

			f.getView();
		}
	}
}
