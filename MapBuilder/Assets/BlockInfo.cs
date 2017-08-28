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
	//public int x, y;
	//public void Update()
	//{
	//    if (Input.GetKeyUp(KeyCode.Escape))
	//    {
	//        BlockInfoPanel.hidePanel();
	//    }
	//}
	//public void OnPointerClick(PointerEventData eventData)
	//{
	//    if (eventData.button == PointerEventData.InputButton.Left)
	//        Debug.Log("Left click");
	//    if (eventData.button == PointerEventData.InputButton.Middle)
	//        Debug.Log("Middle click");
	//    if (eventData.button == PointerEventData.InputButton.Right)
	//        BlockInfoPanel.showLayerInfo(x, y);
	//}
	public void OnPointerClick(PointerEventData eventData)
	{
		throw new NotImplementedException();
	}
}