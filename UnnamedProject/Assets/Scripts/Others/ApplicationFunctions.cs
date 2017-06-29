using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationFunctions : MonoBehaviour
{
	public void exit()
	{
		Application.Quit();
	}
	public void loadSceneByName(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}
}