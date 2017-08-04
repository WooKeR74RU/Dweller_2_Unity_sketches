public class Level
{
	public Map map;
	public Display display;
	public ObjectEventSystem eventSystem;
	public string levelName;

	public void initialize(string levelName)
	{
		this.levelName = levelName;
		map = new Map();
		map.initialize(levelName, this);
		display = new Display(map);
		eventSystem = new ObjectEventSystem();
	}
}