using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Button : MonoBehaviour
{
	public GameObject canvas;
	public GameObject button;
	public GameObject text;

	/*
		Create button on new canvas with below parameters
	*/

	public Button(int x, int y, int width, int height)
	{
		button = new GameObject("Button");
		canvas = new GameObject("Canvas");
		text = new GameObject("Text");
		//main_.SetActive(false);
		//RectTransform button_dimensions = button.AddComponent<RectTransform>();
		button.AddComponent<CanvasRenderer>();
		//UnityEngine.UI.Button btn = button.AddComponent<UnityEngine.UI.Button>();
		button.AddComponent<Image>();

		text.AddComponent<RectTransform>();
		text.AddComponent<CanvasRenderer>();
		//VerticalLayoutGroup button_layout = button.AddComponent<VerticalLayoutGroup>();
		Text text_ = text.AddComponent<Text>();
		text_.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text_.resizeTextForBestFit = true;
		text_.resizeTextMinSize = 1;
		text_.resizeTextMaxSize = 300;
		text.transform.parent = button.transform;
		text_.alignment = TextAnchor.MiddleCenter;
		canvas.AddComponent<RectTransform>();
		//Canvas canvas_ = canvas.AddComponent<Canvas>();
		//CanvasScaler canvas_scaler = canvas.AddComponent<CanvasScaler>();
		canvas.AddComponent<GraphicRaycaster>();
		button.transform.parent = canvas.transform;


		SetPosition(new Vector2(x, y));
		SetButtonSize(width, height);

		CanvasPreference();
	}
	/*
	 * Create button on existing canvas with below parameters
	*/
	public Button(int x, int y, int width, int height, GameObject canvas__)
	{
		button = new GameObject("Button");
		canvas = canvas__;
		text = new GameObject("Text");

		//RectTransform button_dimensions = button.AddComponent<RectTransform>();

		button.AddComponent<CanvasRenderer>();
		//UnityEngine.UI.Button btn = button.AddComponent<UnityEngine.UI.Button>();
		button.AddComponent<Image>();

		text.AddComponent<RectTransform>();
		text.AddComponent<CanvasRenderer>();
		//VerticalLayoutGroup button_layout = button.AddComponent<VerticalLayoutGroup>();
		Text text_ = text.AddComponent<Text>();
		text_.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text_.resizeTextForBestFit = true;
		text_.resizeTextMinSize = 1;
		text_.resizeTextMaxSize = 300;
		text_.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		text_.alignment = TextAnchor.MiddleCenter;
		text.transform.parent = button.transform;

		//Canvas canvas_ = canvas.GetComponent<Canvas>();
		//CanvasScaler canvas_scaler = canvas.GetComponent<CanvasScaler>();
		button.transform.parent = canvas.transform;

		SetPosition(new Vector2(x, y));
		SetButtonSize(width, height);
	}
	void LayoutPreferences()
	{
		button.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
		button.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
		button.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = true;
		button.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = true;
	}
	void CanvasPreference()
	{
		canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}
	public void SetBackground(Sprite background)
	{
		button.GetComponent<Image>().sprite = background;
		button.GetComponent<UnityEngine.UI.Button>().targetGraphic = button.GetComponent<Image>();
	}
	public void SetBackground(Texture2D background)
	{
		button.GetComponent<Image>().sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), 1);
		button.GetComponent<UnityEngine.UI.Button>().targetGraphic = button.GetComponent<Image>();
	}
	public void SetText(string text_)
	{
		text.GetComponent<Text>().text = text_;
	}
	public void SetButtonSize(int width, int height)
	{
		button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
	}
	public void SetPosition(Vector2 pos)
	{
		button.GetComponent<RectTransform>().transform.position = pos;
	}
	public void SetOnClickListener(UnityAction listener)
	{
		button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(listener);
	}
	public Image GetButtonImageComponent()
	{
		return button.GetComponent<Image>();
	}
	public Button GetButtonComponent()
	{
		return button.GetComponent<Button>();
	}
	public Canvas GetCurrentCanvas()
	{
		return canvas.GetComponent<Canvas>();
	}
}
