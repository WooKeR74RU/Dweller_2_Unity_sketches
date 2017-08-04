public abstract class Entity : BaseObject
{
	public bool collision;
	public bool movable;
	public bool trap;

	public abstract override BaseObject fullCopy();
	public abstract override void setView();
}