using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectEventTrigger : MonoBehaviour
{
	int delay;
	private void Update()
	{
		foreach (KeyValuePair<string, Level> level in GlobalData.game.levels)
		{
			if (level.Value.eventSystem.isExecutionAvailable)
			{
				level.Value.eventSystem.isExecutionAvailable = false;
				level.Value.eventSystem.castEvent();
			}
		}
	}
}