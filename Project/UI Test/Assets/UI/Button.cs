using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class Button : UnityEngine.UI.Button, BaseUI
{
    public GameObject buttonGameObject, textGameObject;
    Canvas canvas;
	string buttonName;
	float paddingl, paddingr, paddingt, paddingd;
	public Button() : base()
    {
		
	}
    public Button(Canvas canvas,string buttonName) : base()
    {
		this.canvas = canvas;
		this.buttonName = buttonName;
	}
	public void create()
	{
		buttonGameObject = new GameObject(buttonName);
		buttonGameObject.transform.parent = canvas.transform;
		buttonGameObject.AddComponent<Button>();
		AddText();
	}
    void AddText()
    {
		if (textGameObject != null)
			return;
		textGameObject = new GameObject("Text");
		TextPreferences(textGameObject.AddComponent<Text>());
        textGameObject.transform.parent = buttonGameObject.transform;
    }

	void TextPreferences(Text text)
	{
		text.resizeTextForBestFit = true;
		text.resizeTextMaxSize = 100;
		text.resizeTextMinSize = 5;

		text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		text.alignment = TextAnchor.MiddleCenter;

		text.color = Color.black;
	}
	public void SetParent(GameObject parent)
    {
        buttonGameObject.transform.parent = parent.transform;
    }

	public void setText(string text)
	{
		textGameObject.GetComponent<Text>().text = text;
	}
	public void setPivot(Vector2 pivot)
	{
		buttonGameObject.GetComponent<RectTransform>().pivot = pivot;
	}
	public void setPosition(Vector2 pos)
	{
		buttonGameObject.transform.position = pos;
	}
	public void setSize(float width, float height)
	{
		buttonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width,height);
		textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		setPadding(paddingl, paddingr, paddingt, paddingd);
	}
	public void setPadding(float l,float r,float t,float d)
	{
		paddingl = l;
		paddingr = r;
		paddingt = t;
		paddingd = d;
		float h = buttonGameObject.GetComponent<RectTransform>().sizeDelta.y;
		float w = buttonGameObject.GetComponent<RectTransform>().sizeDelta.x;
		textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(w - (l + r), h - (t + d));
	}
	public void setBackground(Texture2D background)
	{
		buttonGameObject.GetComponent<Image>().sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0, 0), 1);
	}

	public void setWidth(float width)
	{
		buttonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, buttonGameObject.GetComponent<RectTransform>().sizeDelta.y);
		textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, textGameObject.GetComponent<RectTransform>().sizeDelta.y);
		setPadding(paddingl, paddingr, paddingt, paddingd);

	}

	public void setHeight(float height)
	{
		setPadding(paddingl, paddingr, paddingt, paddingd);
		buttonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonGameObject.GetComponent<RectTransform>().sizeDelta.x, height);
		textGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textGameObject.GetComponent<RectTransform>().sizeDelta.x, height);
	}
}

//using UnityEngine.Events;
//public class Button 
//{
//    public GameObject canvas;
//    public GameObject button;
//    public GameObject text;

//    /*
//	 * Create button on new canvas with below parameters
//	*/
//    public Button(int x, int y, int width, int height, GameObject canvas)
//    {
//        button = new GameObject("Button");
//        this.canvas = canvas;
//        text = new GameObject("Text");

//        UnityEngine.UI.Button btn = button.AddComponent<UnityEngine.UI.Button>();

//        RectTransform buttonDimensions = button.AddComponent<RectTransform>();

//        button.AddComponent<CanvasRenderer>();
//        button.AddComponent<Image>();

//        text.AddComponent<RectTransform>();
//        text.AddComponent<CanvasRenderer>();
//        VerticalLayoutGroup button_layout = button.AddComponent<VerticalLayoutGroup>();
//        Text text_ = text.AddComponent<Text>();
//        text_.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
//        text_.resizeTextForBestFit = true;
//        text_.resizeTextMinSize = 1;
//        text_.resizeTextMaxSize = 300;
//        text_.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
//        text_.alignment = TextAnchor.MiddleCenter;
//        text.transform.parent = button.transform;

//        Canvas canvas_ = canvas.GetComponent<Canvas>();
//        CanvasScaler canvas_scaler = canvas.GetComponent<CanvasScaler>();
//        button.transform.parent = canvas.transform;

//        SetPosition(new Vector2(x, y));
//        SetButtonSize(width, height);
//    }
//    void LayoutPreferences()
//    {
//        button.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
//        button.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
//        button.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = true;
//        button.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = true;
//    }
//    void CanvasPreference()
//    {
//        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
//    }
//    public void SetBackground(Sprite background)
//    {
//        button.GetComponent<Image>().sprite = background;
//        button.GetComponent<UnityEngine.UI.Button>().targetGraphic = button.GetComponent<Image>();
//    }
//    public void SetBackground(Texture2D background)
//    {
//        button.GetComponent<Image>().sprite = Sprite.Create(background, new Rect(0, 0, background.width, background.height), new Vector2(0.5f, 0.5f), 1);
//        button.GetComponent<UnityEngine.UI.Button>().targetGraphic = button.GetComponent<Image>();
//    }
//    public void SetText(string text_)
//    {
//        text.GetComponent<Text>().text = text_;
//    }
//    public void SetButtonSize(int width, int height)
//    {
//        button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
//    }
//    public void SetPosition(Vector2 pos)
//    {
//        button.GetComponent<RectTransform>().transform.position = pos;
//    }
//    public void SetOnClickListener(UnityAction listener)
//    {
//        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(listener);
//    }
//    public Image GetButtonImageComponent()
//    {
//        return button.GetComponent<Image>();
//    }
//    public Button GetButtonComponent()
//    {
//        return button.GetComponent<Button>();
//    }
//    public Canvas GetCurrentCanvas()
//    {
//        return canvas.GetComponent<Canvas>();
//    }
//}