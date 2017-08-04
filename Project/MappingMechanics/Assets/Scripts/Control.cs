using UnityEngine;
public class Control : MonoBehaviour
{
	private string[] dirButtons = new string[] { "Up", "Down", "Left", "Right" };
	private bool[] arrow = new bool[4];
	private int[] remainder = new int[4];

	private Unit curUnitPointer;
	private const int delay = 150;

	public static GameObject arrowObject;
	public static Texture2D arrowEnable;
	public static Texture2D arrowDisable;
	public bool enable;

	public void setEnable(bool status)
	{
		if (enable != status)
		{
			enable = status;
			if (enable)
			{
				arrowObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(arrowEnable, new Rect(0, 0, arrowEnable.width, arrowEnable.height), new Vector2(0, 0), 1);
			}
			else
			{
				arrowObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(arrowDisable, new Rect(0, 0, arrowDisable.width, arrowDisable.height), new Vector2(0, 0), 1);
			}
		}
	}

	private void Start()
	{
		gameObject.SetActive(false);
		arrowObject.SetActive(false);
		enable = false;
		setEnable(true);
	}

	private void Update()
	{
		for (int i = 0; i < dirButtons.Length; i++)
		{
			if (Input.GetButtonDown(dirButtons[i]))
				arrow[i] = true;
		}
		for (int i = 0; i < dirButtons.Length; i++)
		{
			if (Input.GetButtonUp(dirButtons[i]))
				remainder[i] = delay;
		}
		int dt = (int)(Time.deltaTime * 1000);
		for (int i = 0; i < dirButtons.Length; i++)
		{
			if (remainder[i] > 0)
			{
				remainder[i] -= dt;
				if (remainder[i] <= 0)
					arrow[i] = false;
			}
		}

		Pair<int, int> moveVector = new Pair<int, int>();
		if (arrow[0])
			moveVector.second = 1;
		if (arrow[1])
			moveVector.second -= 1;
		if (arrow[3])
			moveVector.first = 1;
		if (arrow[2])
			moveVector.first -= 1;

		bool way = !moveVector.Equals(new Pair<int, int>(0, 0)) && curUnitPointer.isPathPossible(moveVector);

		setEnable(way);

		int curArrowX = (curUnitPointer.adr.worldX + moveVector.first) * 24;
		int curArrowY = (curUnitPointer.adr.worldY + moveVector.second) * 24;
		arrowObject.transform.position = new Vector2(curArrowX, curArrowY);

		for (int i = 0; i < dirButtons.Length; i++)
		{
			if (Input.GetButton(dirButtons[i]))
				return;
		}

		for (int i = 0; i < dirButtons.Length; i++)
		{
			arrow[i] = false;
			remainder[i] = 0;
		}

		if (way)
		{
			gameObject.SetActive(false);
			arrowObject.SetActive(false);
			curUnitPointer.adr.levelPointer.eventSystem.addEvent("step", new object[] { moveVector, curUnitPointer }, 1);
			curUnitPointer.adr.levelPointer.eventSystem.addEvent("behaviour", new object[] { curUnitPointer }, 2);
		}
	}

	public void waitCommand(Unit curUnitPointer)
	{
		this.curUnitPointer = curUnitPointer;
		gameObject.SetActive(true);
		arrowObject.SetActive(true);
	}
}