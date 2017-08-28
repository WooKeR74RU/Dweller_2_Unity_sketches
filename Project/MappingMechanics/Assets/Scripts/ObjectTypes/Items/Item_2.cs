public abstract class Item : BaseObject
{
	public int count;
	public bool isDestroyable;

	public override void initializeCommonGroupComponents()
	{
		layerRate = 0;
	}
}