using System;
using System.Collections.Generic;
using System.Linq;

public class ObjectEventSystem
{
	//TODO: Reset time every 1e6
	public static double curTime = 0; //temporally static
	public SortedDictionary<double, ObjectEventSequence> eventsSequence = new SortedDictionary<double, ObjectEventSequence>(); //time of completion, object event sequence

	public Random random = new Random();
	public const double eps = 1e-6;

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
							{
								ObjectEventSequence sequence = new ObjectEventSequence();
								sequence.addEvent("behaviour", new object[] { list[k] });
								addSequence(sequence, 0);
							}
						}
					}
				}
			}
		}
		///

	}

	public void addSequence(ObjectEventSequence sequence, double castTime)
	{
		double completionTime = curTime + castTime;
		while (eventsSequence.ContainsKey(completionTime))
			completionTime += (random.NextDouble() - 0.5) % eps;
		eventsSequence.Add(completionTime, sequence);
	}

	public void makeSequence()
	{
		KeyValuePair<double, ObjectEventSequence> curElement = eventsSequence.First();
		curTime = curElement.Key;
		curElement.Value.make();
		eventsSequence.Remove(curElement.Key);
	}
}