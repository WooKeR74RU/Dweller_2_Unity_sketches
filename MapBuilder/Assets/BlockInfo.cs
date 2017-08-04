using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.IO;

using UnityEngine.UI;

public class BlockInfo : MonoBehaviour, IPointerClickHandler
{
	public int x, y;
	public void OnPointerClick(PointerEventData eventData)
	{
		
		if (eventData.button == PointerEventData.InputButton.Left)
			Debug.Log("Left click");
		else if (eventData.button == PointerEventData.InputButton.Middle)
			Debug.Log("Middle click");
		else if (eventData.button == PointerEventData.InputButton.Right)
			showLayerInfo();
	}
	void showLayerInfo()
	{
		
		GameObject panel = GameObject.Find("LayerPanel");
		int childs = panel.transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(panel.transform.GetChild(i).gameObject);
		}
		
	

		for (int i = 0; i < Map.field[y][x].Count; i++)
		{
			GameObject item = new GameObject();
			item.AddComponent<RectTransform>().pivot = new Vector2(0,1f);
			

			LayoutElement el1 = item.AddComponent<LayoutElement>();
			//el1.preferredWidth = 436;
			//el1.preferredHeight = 48;
			HorizontalLayoutGroup layout = item.AddComponent<HorizontalLayoutGroup>();
			layout.childControlHeight = true;
			layout.childControlWidth = true;
			layout.childForceExpandHeight = false;
			layout.childForceExpandWidth = false;
			ContentSizeFitter fitter = item.AddComponent<ContentSizeFitter>();
			fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			item.transform.parent = panel.transform;

			GameObject g = new GameObject();
			Texture2D t = Map.field[y][x][i].Value;
			g.AddComponent<RectTransform>();
			g.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
			g.transform.parent = item.transform;

			GameObject text = new GameObject();
			text.AddComponent<RectTransform>();
			text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
			LayoutElement el = text.AddComponent<LayoutElement>();
			el.minHeight = 48;
			el.preferredWidth = 472;
			string ttext = Map.field[y][x][i].Key;
			Text tt = text.AddComponent<Text>();
			tt.text = ttext;
			tt.resizeTextForBestFit = true;
			tt.fontSize = 40;
			tt.resizeTextMaxSize = 40;
			tt.resizeTextMinSize = 40;
			tt.font  =  Resources.Load<Font>("Fonts/Actor-Regular");
			ContentSizeFitter fitter2 = text.AddComponent<ContentSizeFitter>();
			fitter2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			text.transform.parent = item.transform;
			//item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			el1.preferredHeight = (int)((double)48 * item.GetComponent<RectTransform>().localScale.x);
		}
		//text2.GetComponent<Text>().text = text;
	}
}