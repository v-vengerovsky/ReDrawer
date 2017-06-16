using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class Utils
	{
		public static void GetMaxMinXY(List<Vector3> points, out float minX, out float maxX, out float minY, out float maxY)
		{
			minX = float.MaxValue;
			maxX = float.MinValue;
			minY = float.MaxValue;
			maxY = float.MinValue;

			foreach (var item in points)
			{
				if (item.x < minX)
				{
					minX = item.x;
				}

				if (item.x > maxX)
				{
					maxX = item.x;
				}

				if (item.y < minY)
				{
					minY = item.y;
				}

				if (item.y > maxY)
				{
					maxY = item.y;
				}
			}
		}

		public static float GetPerimeter(List<Vector3> points)
		{
			float perimeter = 0f;

			for (int i = 0; i < points.Count; i++)
			{
				Vector3 segment;

				if (i < points.Count - 1)
				{
					segment = points[i + 1] - points[i];
				}
				else
				{
					segment = points[0] - points[i];
				}

				perimeter += segment.magnitude;
			}

			return perimeter;
		}

		public static List<Vector3> GetStretchedPointsXY(List<Vector2> points2d, float width, float height)
		{
			return GetNormalizedPointsXY(points2d, width, height, true);
		}

		public static List<Vector3> GetStretchedPointsXY(List<Vector3> points, float width, float height)
		{
			return GetNormalizedPointsXY(points, width, height, true);
		}

		public static List<Vector3> GetFittedRatioPointsXY(List<Vector2> points2d, float width, float height)
		{			
			return GetNormalizedPointsXY(points2d, width, height, false);
		}

		public static List<Vector3> GetFittedRatioPointsXY(List<Vector3> points, float width, float height)
		{
			return GetNormalizedPointsXY(points, width, height, false);
		}

		public static List<Vector3> GetNormalizedPointsXY(List<Vector2> points2d, float width, float height, bool stretch)
		{
			List<Vector3> points = new List<Vector3>();

			foreach (var item in points2d)
			{
				points.Add(item);
			}

			return GetNormalizedPointsXY(points, width, height, stretch);
		}

		public static List<Vector3> GetNormalizedPointsXY(List<Vector3> points, float width, float height, bool stretch)
		{
			List<Vector3> result = new List<Vector3>();

			float minX;
			float maxX;
			float minY;
			float maxY;

			GetMaxMinXY(points, out minX, out maxX, out minY, out maxY);

			float halfWidth = width / 2;
			float halfHeight = height / 2;
			float rangeX = maxX - minX;
			float rangeY = maxY - minY;
			float figureRatio = rangeX / rangeY;
			float screenRatio = width / height;

			float xCoef = width / rangeX;
			float yCoef = height / rangeY;
			float xOffset = minX;
			float yOffset = minY;

			if (!stretch)
			{
				if (figureRatio > screenRatio)
				{
					yCoef *= screenRatio / figureRatio;
					halfHeight *= screenRatio / figureRatio;
				}
				else
				{
					xCoef *= figureRatio / screenRatio;
					halfWidth *= figureRatio / screenRatio;
				}
			}

			Vector3 temp;
			for (int i = 0; i < points.Count; i++)
			{
				temp = points[i];
				temp.x = ((temp.x - xOffset) * xCoef) - halfWidth;
				temp.y = ((temp.y - yOffset) * yCoef) - halfHeight;
				result.Add(temp);
			}

			return result;
		}
	}
}
