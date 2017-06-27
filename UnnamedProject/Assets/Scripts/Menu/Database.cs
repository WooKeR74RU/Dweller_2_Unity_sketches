using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {
	//playerClass
	public int playerClass
	{
		get { return PlayerPrefs.GetInt("playerClass"); }
		set { PlayerPrefs.SetInt("playerClass", value); }
	}
	public int PlayerXP
	{
		get { return PlayerPrefs.GetInt("PlayerXP"); }
		set { PlayerPrefs.SetInt("PlayerXP", value); }
	}
	public int PlayerMana
	{
		get { return PlayerPrefs.GetInt("PlayerMana"); }
		set { PlayerPrefs.SetInt("PlayerMana", value); }
	}
}
