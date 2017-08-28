using UnityEngine;

using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using System.Collections.Generic;

public class BlockInfoPanel : MonoBehaviour,IPointerDownHandler
{
	static GameObject panel;
	public void Start()
	{
		panel = gameObject;
		hidePanel();
	}
	public static void hidePanel()
	{
		panel.SetActive(false);
	}
	public static void showLayerInfo(List<item> ids)
	{
		panel.SetActive(true);
		int childs = panel.transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(panel.transform.GetChild(i).gameObject);
		}

		for (int i = 0; i < ids.Count; i++)
		{
			string ttext = Menu.objectNameById[ids[i].id];
			GameObject item = new GameObject();
			item.AddComponent<RectTransform>().pivot = new Vector2(0, 1f);

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
			Texture2D t = Menu.texturesByName[Menu.textureNameById[ids[i].id]];
			g.AddComponent<RectTransform>();
			g.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			g.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			g.AddComponent<LayoutElement>().preferredHeight = 64;
			g.GetComponent<LayoutElement>().preferredWidth = 64;

			g.AddComponent<Image>().sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f), 1);
			g.transform.parent = item.transform;

			GameObject text = new GameObject();
			text.AddComponent<RectTransform>();
			text.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
			LayoutElement el = text.AddComponent<LayoutElement>();
			//el.minHeight = 64;
			el.preferredWidth = 472;

			Text tt = text.AddComponent<Text>();
			tt.text = ttext;
			tt.resizeTextForBestFit = true;
			tt.fontSize = 40;
			tt.resizeTextMaxSize = 40;
			tt.resizeTextMinSize = 40;
			tt.font = Resources.Load<Font>("Fonts/Actor-Regular");
			ContentSizeFitter fitter2 = text.AddComponent<ContentSizeFitter>();
			fitter2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			text.transform.parent = item.transform;
			item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			//	el1.preferredHeight = (int)((double)48 * item.GetComponent<RectTransform>().localScale.x);
		}
		//text2.GetComponent<Text>().text = text;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (panel.active)
			SelectBlock.isStayBlockEnable = false;
		else
			SelectBlock.isStayBlockEnable = true; ;
	}
}