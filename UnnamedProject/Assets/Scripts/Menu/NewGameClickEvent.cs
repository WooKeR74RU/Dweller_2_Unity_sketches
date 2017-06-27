using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameClickEvent : MonoBehaviour
{
	public void StartNewGame()
	{
		SceneManager.LoadScene("level_1", LoadSceneMode.Single);
	}
}
