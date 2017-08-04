using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour
{

	public GameObject canvas, text, panel;
	VerticalLayoutGroup layout;
	public TextView(int x, int y, int width, int height, GameObject canvas__)
	{
		panel = new GameObject("Content");
		canvas = canvas__;
		text = new GameObject("Text");

		panel.AddComponent<RectTransform>();

		panel.AddComponent<CanvasRenderer>();
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		text.AddComponent<RectTransform>();
		text.AddComponent<CanvasRenderer>();
		text.AddComponent<Text>();
		text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text.GetComponent<Text>().resizeTextForBestFit = true;
		text.GetComponent<Text>().resizeTextMinSize = 10;
		text.GetComponent<Text>().resizeTextMaxSize = 300;
		text.GetComponent<Text>().transform.parent = panel.transform;
		layout = panel.AddComponent<VerticalLayoutGroup>();
		LayoutPreferences();
		panel.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		text.GetComponent<Text>().alignByGeometry = true;

	}
	void LayoutPreferences()
	{
		layout.childControlWidth = true;
		layout.childControlHeight = true;
		layout.childForceExpandWidth = true;
		layout.childForceExpandHeight = true;
	}
	public void SetVerticalOverflow(VerticalWrapMode mode)
	{
		text.GetComponent<Text>().verticalOverflow = mode;
	}
	public void SetMinTextSize(int size)
	{
		text.GetComponent<Text>().resizeTextMinSize = size;
	}
	public void SetMaxTextSize(int size)
	{
		text.GetComponent<Text>().resizeTextMaxSize = size;
	}
	public void SetBackground(Texture2D t)
	{
		panel.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
	}

	public void SetText(string text_)
	{
		text.GetComponent<Text>().text = text_;
		panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
	}

}
