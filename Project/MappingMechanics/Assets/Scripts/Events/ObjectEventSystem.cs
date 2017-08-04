using System.Collections.Generic;
using System.Linq;

public class ObjectEventSystem
{
	public int curTime = 0;
	public SortedDictionary<int, ObjectEvent> events = new SortedDictionary<int, ObjectEvent>(); //time of completion, object event

	public ObjectEventSystem()
	{
		addEvent("behaviour", new object[]{ GlobalData.game.player}, 0);
	}

	public void addEvent(string eventName, object[] arguments, int castTime)
	{
		ObjectEvent objEvent = new ObjectEvent(eventName, arguments, castTime);
		events.Add(curTime + castTime, objEvent);
	}

	public void castEvent()
	{
		KeyValuePair<int, ObjectEvent> curElement = events.First();
		curElement.Value.make();
		events.Remove(curElement.Key);
	}
}