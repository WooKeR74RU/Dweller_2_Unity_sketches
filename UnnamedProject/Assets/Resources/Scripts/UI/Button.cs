using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Button : MonoBehaviour {


	public GameObject canvas;
	public GameObject button;
	public GameObject text;

	Canvas canvas_;
	CanvasScaler canvas_scaler;
	UnityEngine.UI.Button btn;
	Text text_;
	RectTransform button_dimensions;
	VerticalLayoutGroup button_layout;
	public Button(int x, int y, int width, int height)
	{
		button = new GameObject("Button");
		canvas = new GameObject("Canvas");
		text = new GameObject("Text");
		//main_.SetActive(false);
		button_dimensions = button.AddComponent<RectTransform>();
		button.AddComponent<CanvasRenderer>();
		btn = button.AddComponent<UnityEngine.UI.Button>();
		button.AddComponent<Image>();

		text.AddComponent<RectTransform>();
		text.AddComponent<CanvasRenderer>();
		button_layout = button.AddComponent<VerticalLayoutGroup>();
		text_ = text.AddComponent<Text>();
		text_.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text_.resizeTextForBestFit = true;
		text_.resizeTextMinSize = 1;
		text_.resizeTextMaxSize = 300;
		text.transform.parent = button.transform;
		text_.alignment = TextAnchor.MiddleCenter;
		canvas.AddComponent<RectTransform>();
		canvas_ = canvas.AddComponent<Canvas>();
		canvas_scaler = canvas.AddComponent<CanvasScaler>();
		canvas.AddComponent<GraphicRaycaster>();
		button.transform.parent = canvas.transform;


		SetPosition(new Vector2(x, y));
		SetSize(width, height);

		CanvasPreference();
	}
	public Button(int x, int y, int width, int height,GameObject canvas__)
	{
		button = new GameObject("Button");
		canvas = canvas__;
		text = new GameObject("Text");

		button_dimensions = button.AddComponent<RectTransform>();

		button.AddComponent<CanvasRenderer>();
		btn = button.AddComponent<UnityEngine.UI.Button>();
		button.AddComponent<Image>();

		text.AddComponent<RectTransform>();
		text.AddComponent<CanvasRenderer>();
		button_layout = button.AddComponent<VerticalLayoutGroup>();
		text_ = text.AddComponent<Text>();
		text_.font = Resources.GetBuiltinResource(typeof(Font),"Arial.ttf") as Font;
		text_.resizeTextForBestFit = true;
		text_.resizeTextMinSize = 1;
		text_.resizeTextMaxSize = 300;
		text_.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		text_.alignment = TextAnchor.MiddleCenter;
		text.transform.parent = button.transform;

		canvas_ = canvas.GetComponent<Canvas>();
		canvas_scaler = canvas.GetComponent<CanvasScaler>();
		button.transform.parent = canvas.transform;

		SetPosition(new Vector2(x, y));
		SetSize(width,height);
	}
	void LayoutPreferences()
	{
		button_layout.childControlWidth = true;
		button_layout.childControlHeight = true;
		button_layout.childForceExpandWidth = true;
		button_layout.childForceExpandHeight = true;
	}
	void CanvasPreference()
	{
		canvas_.renderMode = RenderMode.ScreenSpaceOverlay;
	}

	void SetUIScale(CanvasScaler.ScaleMode mode, Vector2 resolution,float Match)
	{
		canvas_scaler.uiScaleMode = mode;
		if (mode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
		{
			canvas_scaler.referenceResolution = resolution;
			canvas_scaler.matchWidthOrHeight = Match;
		}
	}
	
	public void SetBackground(Sprite background)
	{
		button.GetComponent<Image>().sprite = background;
		btn.targetGraphic = button.GetComponent<Image>();
	}
	public void SetBackground(Texture2D background)
	{
		button.GetComponent<Image>().sprite = Sprite.Create(background,new Rect(0,0,background.width,background.height),new Vector2(0.5f,0.5f),1);
		btn.targetGraphic = button.GetComponent<Image>();
	}
	public void SetText(string text_)
	{
		text.GetComponent<Text>().text = text_;
	}
	public void SetSize(int width,int height)
	{
		button_dimensions.sizeDelta = new Vector2(width, height);
	}
	public void SetPosition(Vector2 pos)
	{
		button_dimensions.transform.position = pos;
	}
	public void SetOnClickListener(UnityAction listener)
	{
		btn.onClick.AddListener(listener);
	}
}
