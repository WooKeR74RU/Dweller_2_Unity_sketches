using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameClickEvent : MonoBehaviour {

	public void StartNewGame()
	{
		SceneManager.LoadScene("Level1", LoadSceneMode.Single);
	}
}
