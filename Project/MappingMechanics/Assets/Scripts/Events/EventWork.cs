using System;
using UnityEngine;

public partial class ObjectEvent
{
	public string eventName;
	public object[] arguments;

	public ObjectEvent(string eventName, object[] arguments)
	{
		this.eventName = eventName;
		this.arguments = arguments;
	}

	public void cast()
	{
		//string eventName = "8-800-555-35-35";
		GetType().GetMethod(eventName).Invoke(this, arguments);
	}
}

public class ObjectEventSequence
{
	public Vector<ObjectEvent> sequence = new Vector<ObjectEvent>();

	public void addEvent(string eventName, object[] arguments)
	{
		sequence.Add(new ObjectEvent(eventName, arguments));
	}

	public void make()
	{
		for (int i = 0; i < sequence.Count; i++)
		{
			Debug.Log(ObjectEventSystem.curTime + ". " + sequence[i].arguments[sequence[i].arguments.Length - 1] + ": " + sequence[i].eventName);

			sequence[i].cast();
		}
	}
}