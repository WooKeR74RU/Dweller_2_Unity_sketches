public abstract class Entity : BaseObject
{
	public bool collision;
	public bool movable;
	public bool trap;

	public override void initializeCommonGroupComponents()
	{
		layerRate = 0;
	}
}