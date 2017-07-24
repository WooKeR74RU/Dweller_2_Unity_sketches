using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Display : MonoBehaviour
{
	public int curX, curY;
	public Camera camera;
	public GameObject[] gameObjects = new GameObject[5000];
	Queue<int> idFreeGameObjects = new Queue<int>();
	int freeObjectFromPool()
	{
		return idFreeGameObjects.Dequeue();
	}
}