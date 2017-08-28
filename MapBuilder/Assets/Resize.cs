using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Resize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject xx, yy, scaleSlider;

	public void RX()
	{
		Map.segmentCountX = Convert.ToInt32(xx.GetComponent<InputField>().text);
		Map.initMainMap(true);
	}
	public void RY()
	{
		Map.segmentCountY = Convert.ToInt32(yy.GetComponent<InputField>().text);
		Map.initMainMap(true);
	}
	public void setMainTextureScale()
	{
		Map.map.GetComponent<RectTransform>().localScale = new Vector2(scaleSlider.GetComponent<Slider>().value, scaleSlider.GetComponent<Slider>().value);
	}
}
