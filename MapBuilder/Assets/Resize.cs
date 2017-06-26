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
	public GameObject xx, yy;
	public void RX()
	{
		Map.x = Convert.ToInt32(xx.GetComponent<InputField>().text);
		Map.Init();
	}
	public void RY()
	{
		Map.y = Convert.ToInt32(yy.GetComponent<InputField>().text);
		Map.Init();
	}
}
