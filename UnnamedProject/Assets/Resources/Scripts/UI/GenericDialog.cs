using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class GenericDialog : MonoBehaviour
{

	GameObject main_;
	GameObject panel;
	VerticalLayoutGroup layout;
	Canvas canvas;
	public GenericDialog(int x,int y,int width, int height)
	{
		main_ = new GameObject("DialogCanvas");
		
		canvas = main_.AddComponent<Canvas>();
			canvas.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		main_.AddComponent<CanvasScaler>();
		main_.AddComponent<GraphicRaycaster>();
		panel = new GameObject("DialogPanel");
		panel.AddComponent<CanvasRenderer>();
	
		panel.AddComponent<RectTransform>().transform.position = new Vector2(x,y);
		panel.transform.SetParent(canvas.transform, false);
		panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		layout = panel.AddComponent<VerticalLayoutGroup>();
		panel.AddComponent<ContentSizeFitter>();
		panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		LayoutPreferences();
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, panel.GetComponent<RectTransform>().sizeDelta.y);
		panel.SetActive(false);
	}
	public GenericDialog(int x, int y, int width, int height, GameObject canvas_)
	{
		main_ = canvas_;
		
		canvas = main_.GetComponent<Canvas>();
		canvas.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);////MAGICCCC
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		panel = new GameObject("DialogPanel");
		panel.AddComponent<CanvasRenderer>();
		
		panel.AddComponent<RectTransform>().transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
		panel.transform.SetParent(canvas.transform, false);
		panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
	
		layout = panel.AddComponent<VerticalLayoutGroup>();
		panel.AddComponent<ContentSizeFitter>();
		panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		LayoutPreferences();
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, panel.GetComponent<RectTransform>().sizeDelta.y);
		panel.SetActive(false);
	}
	void LayoutPreferences()
	{
		layout.childControlWidth = false;
		layout.childControlHeight = false;
		layout.childForceExpandWidth = false;
		layout.childForceExpandHeight = false;
		layout.childAlignment = TextAnchor.UpperCenter;
	}
	public void SetChildAligment(TextAnchor p)
	{
		layout.childAlignment = p;
	}
	public void SetChildForceExpandWidth(bool p)
	{
		layout.childForceExpandWidth = p;
	}
	public void SetBackground(Texture2D t)
	{
		panel.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
	}

	public void SetPaddings(int left,int right,int top,int bottom)
	{
		panel.GetComponent<VerticalLayoutGroup>().padding.bottom = bottom;
		panel.GetComponent<VerticalLayoutGroup>().padding.top = top;
		panel.GetComponent<VerticalLayoutGroup>().padding.right = right;
		panel.GetComponent<VerticalLayoutGroup>().padding.left = left;
	}
	public void SetChildControlWidth(bool p)
	{
		layout.childControlWidth = p;
	}
	public void AddButton(string text,Texture2D background,int width, int height)
	{

		Button btn = new Button(0, 0, width, height,main_);
		btn.button.transform.parent = panel.transform;
		btn.SetBackground(background);
		btn.SetText(text);
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
	public void AddButton(string text, Texture2D background)
	{

		Button btn = new Button(0, 0, background.width, background.height, main_);
		btn.button.transform.parent = panel.transform;
		btn.SetBackground(background);
		btn.SetText(text);
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
	public void AddButton(Button bt)
	{
		bt.button.transform.parent = panel.transform;
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
	public void AddText(string text)
	{

		TextView tt = new TextView(0, 0, (int)panel.GetComponent<RectTransform>().sizeDelta.x, 0, main_);
		tt.SetVerticalOverflow(VerticalWrapMode.Truncate);
		tt.panel.transform.parent = panel.transform;
		tt.SetText(text);
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
	public void AddText(TextView tv)
	{

		tv.panel.transform.parent = panel.transform;
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
	public void Show()
	{
		panel.SetActive(true);
	}
	public void Hide()
	{
		panel.SetActive(false);
	}
	public void Cancel()
	{
		Destroy(panel);
	}
}