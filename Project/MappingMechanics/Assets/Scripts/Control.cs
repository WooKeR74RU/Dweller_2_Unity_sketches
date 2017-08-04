using UnityEngine;
public class Control : MonoBehaviour
{
	private string[] dirButtons = new string[] { "Up", "Down", "Left", "Right" };
	private bool[] arrow = new bool[4];
	private int[] remainder = new int[4];

	private Unit curUnitPointer;
	private int delay = 150;

	public static GameObject arrowObject;

	private void Start()
	{
		gameObject.SetActive(false);
		arrowObject.SetActive(false);
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

		if (way)
		{
			int curArrowX = (curUnitPointer.adr.worldX + moveVector.first) * 24;
			int curArrowY = (curUnitPointer.adr.worldY + moveVector.second) * 24;
			arrowObject.transform.position = new Vector2(curArrowX, curArrowY);
			arrowObject.SetActive(true);
		}
		else
		{
			arrowObject.SetActive(false);
		}

		for (int i = 0; i < dirButtons.Length; i++)
		{
			if (Input.GetButton(dirButtons[i]))
				return;
		}

		arrowObject.SetActive(false);
		for (int i = 0; i < dirButtons.Length; i++)
		{
			arrow[i] = false;
			remainder[i] = 0;
		}

		if (way)
		{
			gameObject.SetActive(false);
			curUnitPointer.adr.levelPointer.eventSystem.addEvent("step", new object[] { moveVector, curUnitPointer }, 1);
			curUnitPointer.adr.levelPointer.eventSystem.addEvent("behaviour", new object[] { curUnitPointer }, 2);

			Debug.Log("Step");
		}
	}

	public void waitCommand(Unit curUnitPointer)
	{
		this.curUnitPointer = curUnitPointer;
		gameObject.SetActive(true);
		arrowObject.SetActive(true);
	}
}