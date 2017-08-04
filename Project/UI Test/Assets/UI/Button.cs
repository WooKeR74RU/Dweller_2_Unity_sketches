using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Button : UnityEngine.UI.Button
{
    GameObject buttonGameObject, textGameObject;
    Canvas canvas;
    public Button() : base()
    {
      
    }
    protected override void Start()
    {
        buttonGameObject = gameObject;
        AddText();
    }
    public Button(Canvas canvas,string buttonName) : base()
    {
        this.canvas = canvas;
        buttonGameObject = new GameObject(buttonName);
        buttonGameObject.transform.parent = canvas.transform;
        buttonGameObject.AddComponent<Button>();
    }
    void AddText()
    {
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

		text.color = Color.black;
	}
	public void SetParent(GameObject parent)
    {
        buttonGameObject.transform.parent = parent.transform;
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