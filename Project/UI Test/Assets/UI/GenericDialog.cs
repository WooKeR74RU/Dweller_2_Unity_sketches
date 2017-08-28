using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class GenericDialog : MonoBehaviour, BaseUI
{

   public GameObject panel;
	string name;
    Canvas canvas;
	List<System.Object> objects = new List<System.Object>();
	float dialogWidth, dialogHeight;
    public GenericDialog(Canvas canvas, string name)
    {
		this.name = name;
		this.canvas = canvas;
    }
	public void create()
	{
		panel = new GameObject(name);
		panel.AddComponent<VerticalLayoutGroup>();
		panel.transform.parent = canvas.transform;
		layoutPreferences();
		show();
		dialogHeight = panel.GetComponent<RectTransform>().sizeDelta.y;
		dialogWidth = panel.GetComponent<RectTransform>().sizeDelta.x;
		panel.AddComponent<LayoutElement>();
	}
	public void setPosition(int x,int y)
    {
        transform.position = new Vector2(x, y);
    }

    void layoutPreferences()
    {
        panel.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
        panel.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        panel.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
        panel.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
        panel.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
		panel.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		panel.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
	}
    public void setChildAligment(TextAnchor textAnchor)
    {
        panel.GetComponent<VerticalLayoutGroup>().childAlignment = textAnchor;
    }
    public void setChildForceExpandWidth(bool p)
    {
		panel.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = p;
    }
    public void setBackground(Texture2D texture)
    {
        panel.AddComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1);
    }
    public void setBackground(Sprite sprite)
    {
        panel.AddComponent<Image>().sprite = sprite;
    }
    public void setPadding(float left, float right, float top, float bottom)
    {
        panel.GetComponent<VerticalLayoutGroup>().padding.bottom = (int)bottom;
        panel.GetComponent<VerticalLayoutGroup>().padding.top = (int)top;
        panel.GetComponent<VerticalLayoutGroup>().padding.right = (int)right;
        panel.GetComponent<VerticalLayoutGroup>().padding.left = (int)left;
    }

    public void updateChildSize()
	{
		//float width = panel.GetComponent<RectTransform>().sizeDelta.x;
		for (int i = 0; i < objects.Count; i++)
		{
			((BaseUI)objects[i]).setWidth(dialogWidth);
		}
	}
    public void addView(Button button)
    {
		button.buttonGameObject.transform.parent = panel.transform;
		objects.Add(button);
		//button.GetComponent<Button>().onClick.AddListener(method);
	}
    public void addView(TextView textView)
    {
		textView.textGameObject.transform.parent = panel.transform;
		objects.Add(textView);
	}
    public void show()
    {
        panel.SetActive(true);
    }
    public void hide()
    {
        panel.SetActive(false);
    }
    public void cancel()
    {
        Destroy(panel);
    }

	public void setSize(float width, float height)
	{
		setWidth(width);
		setHeight(height);
	}

	public void setWidth(float width)
	{
		dialogWidth = width;
		panel.GetComponent<LayoutElement>().preferredWidth = dialogWidth;
		updateChildSize();
	}

	public void setHeight(float height)
	{
		dialogHeight = height;
		panel.GetComponent<LayoutElement>().preferredHeight = dialogHeight;
	}

}