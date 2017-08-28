using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextView : Text, BaseUI
{

	Canvas canvas;
	public GameObject textGameObject;
	string name;
	float paddingl, paddingr, paddingt, paddingd;
	public TextView(Canvas canvas,string name)
    {
		this.canvas = canvas;
		this.name = name;
    }
	public void create()
	{
		textGameObject = new GameObject(name);
		textGameObject.transform.parent = canvas.transform;
		textGameObject.AddComponent<Text>();
		textPreference();

	}
	void textPreference()
	{
		textGameObject.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		textGameObject.GetComponent<Text>().resizeTextForBestFit = true;
		textGameObject.GetComponent<Text>().resizeTextMinSize = 10;
		textGameObject.GetComponent<Text>().resizeTextMaxSize = 300;
		textGameObject.GetComponent<Text>().alignByGeometry = true;
		textGameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		textGameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		textGameObject.AddComponent<LayoutElement>();

	}
	public void setVerticalOverflow(VerticalWrapMode mode)
    {
        textGameObject.GetComponent<Text>().verticalOverflow = mode;
    }
    public void setMinTextSize(int size)
    {
        textGameObject.GetComponent<Text>().resizeTextMinSize = size;
    }
    public void setMaxTextSize(int size)
    {
        textGameObject.GetComponent<Text>().resizeTextMaxSize = size;
    }
    public void setBackground(Texture2D t)
    {
		textGameObject.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
    }

    public void setText(string text_)
    {
        textGameObject.GetComponent<Text>().text = text_;
	}
	public void setFontSize(int size)
	{
		textGameObject.GetComponent<Text>().fontSize = size;
	}
	public void setWidth(float width)
	{
		textGameObject.GetComponent<LayoutElement>().preferredWidth = width;
	}
	public void setHeight(float height)
	{
		textGameObject.GetComponent<LayoutElement>().preferredHeight = height;
	}
	public void setSize(float width, float height)
	{
		setHeight(height);
		setWidth(width);
	}

	public void setPadding(float l, float r, float t, float d)
	{
		paddingl = l;
		paddingr = r;
		paddingt = t;
		paddingd = d;
		float h = textGameObject.GetComponent<RectTransform>().sizeDelta.y;
		float w = textGameObject.GetComponent<RectTransform>().sizeDelta.x;
		textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(w - (l + r), h - (t + d));
	}
}
