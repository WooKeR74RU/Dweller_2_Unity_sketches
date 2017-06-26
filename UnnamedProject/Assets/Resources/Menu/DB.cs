using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour {

	public int PlayerClass
	{
		get { return PlayerPrefs.GetInt("PlayerClass"); }
		set { PlayerPrefs.SetInt("PlayerClass", value); }
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
