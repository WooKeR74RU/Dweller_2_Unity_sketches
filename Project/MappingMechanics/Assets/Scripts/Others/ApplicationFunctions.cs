using UnityEngine;
using UnityEngine.SceneManagement;

//TODO: Ограничить курсор мыши внутри окна
public class ApplicationFunctions : MonoBehaviour
{
	public static bool onceInit = false;
	public void Awake()
	{
		if (!onceInit)
		{
			GlobalData.initialization(); //occurs once
			onceInit = true;
		}
	}

	public void loadSceneByName(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void exit()
	{
		Application.Quit();
	}

	public void loadGame(string gameName)
	{
		loadSceneByName("Game");
		GlobalData.game = new Game();
		GlobalData.game.initialize(gameName);
		GlobalData.objectEventTriggerObj.SetActive(true);
	}

	public void resetGame()
	{
		GlobalData.objectEventTriggerObj.SetActive(false);
		GlobalData.game.reset();
		GlobalData.game = null;
		loadSceneByName("Menu");
	}
}