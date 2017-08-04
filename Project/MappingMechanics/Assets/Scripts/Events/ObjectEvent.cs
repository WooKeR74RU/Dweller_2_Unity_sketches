public partial class ObjectEvent
{
	public string eventName;
	public object[] arguments;

	public int castTime;

	public ObjectEvent(string eventName, object[] arguments, int castTime)
	{
		this.eventName = eventName;
		this.arguments = arguments;

		this.castTime = castTime;
	}

	public void make()
	{
		//string eventName = "8-800-555-35-35";
		try
		{
			GetType().GetMethod(eventName).Invoke(this, arguments);

		}
		catch (System.Exception)
		{

			throw;
		}
	}
}