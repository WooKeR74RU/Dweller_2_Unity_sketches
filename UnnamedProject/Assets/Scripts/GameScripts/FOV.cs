//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;

//public class FOV
//{
//	private static double dist(int x, int y, int toX, int toY)
//	{
//		double dst = Math.Sqrt((x - toX) * (x - toX) + (y - toY) * (y - toY));
//		return dst;
//	}

//	public static bool isVisible(int range, int x, int y, int toX, int toY)
//	{
//		if (dist(x, y, toX, toY) >= range)
//			return false;

//		int lenX = x - toX;
//		int lenY = y - toY;

//		int k = lenX;
//		int len = lenY / k;

//		int maxLen = 

//		for (int i = 0; i < k; i++)
//		{
//			for (int j = 0; j < length; j++)
//			{

//			}
//		}

//	}

//}

////public class UnitVisibleDistanceCalculated : MonoBehaviour
////{
////	//mapa[(int)mapY][(int)mapX] true or false. true if object in this cell not available for vision(it's opaque)
////	public bool isVisible(int visionDistance, int x, int y, int unitPositionX, int unitPositionY)
////	{
////		int w = Mathf.Abs(x - unitPositionX);
////		int h = Mathf.Abs(y - unitPositionY);
////		int distanceBetweenCell = (int)Mathf.Ceil(Mathf.Sqrt((float)(w * w + h * h)));
////		if (distanceBetweenCell / 48 <= visionDistance)
////		{
////			return isEnable(x, y, unitPositionX, unitPositionY);
////		}
////		return false;
////	}
////	bool isEnable(float x, float y, float unitPositionX, float unitPositionY)
////	{
////		if (x < 0 || y < 0)
////			return false;
////		int mapX = (int)x / 48, mapY = (int)y / 48;
////		if (mapY < GenerateMap.mapa.Count && mapX < GenerateMap.mapa[mapY].Count &&
////		GenerateMap.mapa[mapY][mapX] == true)
////			return false;

////	}
////}

//public class UnitVisibleDistanceCalculated
//{
//	public bool isVisible(int range, int x, int y, int toX, int toY)
//	{
//		ArrayList<ArrayList<int>> arr = GlobalData.field;

//	}
//}

