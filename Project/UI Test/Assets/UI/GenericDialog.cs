using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class GenericDialog : MonoBehaviour
{

    GameObject panel;
    Canvas canvas;
    RectTransform transform;
    public GenericDialog()
    {
        panel = new GameObject("GenericDialog");
        transform =  panel.AddComponent<RectTransform>();
    }
    public GenericDialog(int x, int y, Canvas canvas) : this()
    {
        this.canvas = canvas;
       
        panel.transform.parent = canvas.transform;
    }
    public void setPosition(int x,int y)
    {
        transform.position = new Vector2(x, y);
    }
    void LayoutPreferences()
    {
        panel.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
        panel.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        panel.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
        panel.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
        panel.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
    }
    public void SetChildAligment(TextAnchor p)
    {
        panel.GetComponent<VerticalLayoutGroup>().childAlignment = p;
    }
    public void SetChildForceExpandWidth(bool p)
    {
        panel.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = p;
    }
    public void SetBackground(Texture2D t)
    {
        panel.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
    }
    public void SetBackground(Sprite sprite)
    {
        panel.AddComponent<Image>().sprite = sprite;
    }
    public void SetPaddings(int left, int right, int top, int bottom)
    {
        panel.GetComponent<VerticalLayoutGroup>().padding.bottom = bottom;
        panel.GetComponent<VerticalLayoutGroup>().padding.top = top;
        panel.GetComponent<VerticalLayoutGroup>().padding.right = right;
        panel.GetComponent<VerticalLayoutGroup>().padding.left = left;
    }
    public void SetChildControlWidth(bool p)
    {
        panel.GetComponent<VerticalLayoutGroup>().childControlWidth = p;
    }
    public void AddButton(string text, Texture2D background, int width, int height)
    {

      //  Button btn = new Button();
     
        //button.GetComponent<Button>().onClick.AddListener(method);
    }
    public void AddButton(string text, Texture2D background)
    {

     
        //button.GetComponent<Button>().onClick.AddListener(method);
    }
    public void AddButton(Button bt)
    {
        //button.GetComponent<Button>().onClick.AddListener(method);
    }
    public void AddText(string text)
    {

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
    public Image GetDialogBackgroundImage()
    {
        return panel.GetComponent<Image>();
    }
}