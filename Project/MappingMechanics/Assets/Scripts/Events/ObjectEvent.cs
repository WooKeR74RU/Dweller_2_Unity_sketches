public partial class ObjectEvent
{
	public string eventName;
	public object[] arguments;

	public ObjectEvent(string eventName, object[] arguments)
	{
		this.eventName = eventName;
		this.arguments = arguments;
	}

	public void make()
	{
		//string eventName = "8-800-555-35-35";
		GetType().GetMethod(eventName).Invoke(this, arguments);
	}
}