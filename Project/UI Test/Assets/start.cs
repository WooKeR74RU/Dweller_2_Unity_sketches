using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{

	public Canvas canvas;
	Button btn;
	//Use this for initialization
	GenericDialog gd;
	TextView tv;
	void Start()
	{

		btn = new Button(canvas, "button");
		btn.create();
		btn.setText("defuygtrgyheesidw");
		//btn.setSize(500, 200);
		btn.setPadding(10, 10, 10, 10);
		btn.buttonGameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
		tv = new TextView(canvas, "penis bobra");
		tv.create();
		tv.setText("efrhgyuhfejklw," +
  "effef");
		gd = new GenericDialog(canvas, "ppppppe");
		gd.create();
		gd.addView(btn);
		gd.addView(tv);
		gd.setPadding(10, 10, 30, 30);
		Button bt = new Button(canvas, "button");
		bt.create();
		gd.addView(bt);
		bt.setText("andatra");
		bt.setWidth(50);
		gd.setWidth(300);
	
	}

	// Update is called once per frame
	void Update()
	{
		//tv.setHeight(tv.textGameObject.GetComponent<RectTransform>().sizeDelta.y);

	}
}
