public abstract class Item : BaseObject
{
	public int count;
	public bool isDestroyable;

	public abstract override BaseObject fullCopy();
	public abstract override void setView();
}