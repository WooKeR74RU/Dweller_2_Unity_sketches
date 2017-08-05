using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectEventSystem
{
	public double curTime = 0;
	public SortedDictionary<double, ObjectEvent> events = new SortedDictionary<double, ObjectEvent>(); //time of completion, object event

	public System.Random random = new System.Random();
	public const double eps = 1e6;

	public bool isExecutionAvailable = true;

	public Level curLevelPointer;
	public ObjectEventSystem(Level curLevelPointer)
	{
		this.curLevelPointer = curLevelPointer;

		///TODO: temporally
		SegmentContent[,] matrix = curLevelPointer.map.curSegMatrix;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (matrix[i, j].field == null)
					continue;
				for (int y = 0; y < matrix[i, j].field.Count; y++)
				{
					for (int x = 0; x < matrix[i, j].field[y].Count; x++)
					{
						List<BaseObject> list = matrix[i, j].field[y][x];
						for (int k = 0; k < list.Count; k++)
						{
							if (GlobalData.getObjectTypeById(list[k].id) == 1)
								addEvent("behaviour", new object[] { list[k] }, 0);
						}
					}
				}
			}
		}
		///

	}

	public void addEvent(string eventName, object[] arguments, double castTime)
	{
		ObjectEvent objEvent = new ObjectEvent(eventName, arguments);
		double completionTime = curTime + castTime;
		while (events.ContainsKey(completionTime))
			completionTime += random.NextDouble() % eps;
		events.Add(completionTime, objEvent);
	}

	static int counter = 1;
	public void castEvent()
	{
		KeyValuePair<double, ObjectEvent> curElement = events.First();
		curElement.Value.make();
		events.Remove(curElement.Key);
		curTime = curElement.Key;

		Debug.Log(counter + ". " + curElement.Value.arguments[curElement.Value.arguments.Length - 1] + ": " + curElement.Value.eventName);
		counter++;
	}
}