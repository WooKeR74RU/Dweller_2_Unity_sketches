using System.Collections.Generic;
using UnityEngine;

public class Game
{
	public GameDescription gameDesc;
	public Dictionary<string, Level> levels = new Dictionary<string, Level>();
	public string curLevelName;
	public Unit player; //temporally

	public void initialize(string gameName)
	{
		gameDesc = new GameDescription(gameName);
		player = new Warrior();
		player.isPlayerControl = true;
		startLevel(gameDesc.startLevelName);
		selectLevel(curLevelName);
	}

	public void reset()
	{
		foreach (KeyValuePair<string, Level> level in levels)
		{
			if (level.Value.display.active)
			{
				level.Value.display.clearVisibleGameObjects();
				level.Value.display.active = false;
				break;
			}
		}
		levels = null;
		gameDesc = null;
		curLevelName = null;
	}

	public void startLevel(string levelName)
	{
		levels[levelName] = new Level();
		levels[levelName].initialize(levelName);
		curLevelName = levelName;
	}

	public void selectLevel(string levelName)
	{
		levels[curLevelName].display.clearVisibleGameObjects();
		levels[curLevelName].display.active = false;
		curLevelName = levelName;
		levels[curLevelName].display.active = true;
		levels[curLevelName].display.initializationVisibleGameObjects();
	}
}

public class GameDescription
{
	public string gameName;
	public string startLevelName;
	//graph
	public GameDescription(string gameName)
	{
		string path = "GamesDescription/" + gameName;
		this.gameName = gameName;
		string text = (Resources.Load(path) as TextAsset).text;
		text = text.Replace("\r", "");

		string[] parametres = text.Split(' ', '\n');
		for (int i = 0; i < parametres.Length; i += 2)
		{
			if (parametres[i] == "StartLevelName")
			{
				startLevelName = parametres[i + 1];
				break;
			}
		}

		//graph
	}
}