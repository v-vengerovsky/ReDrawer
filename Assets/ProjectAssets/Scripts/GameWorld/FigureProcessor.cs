using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class FigureProcessor
	{
		public List<Vector3> GetScaledFigurePoints(Figure figure, float screenWidth, float screenHeight, float screenOffset)
		{
			List<Vector3> points = new List<Vector3>();

			float minX = float.MaxValue;
			float maxX = float.MinValue;
			float minY = float.MaxValue;
			float maxY = float.MinValue;

			foreach (var item in figure.Points)
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

				if (item.y > minY)
				{
					maxY = item.y;
				}
			}


			float width = screenWidth - 2 * screenOffset;
			float height = screenHeight - 2 * screenOffset;
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

			if (figureRatio > screenRatio)
			{

			}
			else
			{
				//xCoef = 1f / screenRatio;
			}

			Vector3 temp;
			for (int i = 0; i < figure.Points.Count; i++)
			{
				temp = figure.Points[i];
				//temp.x = (temp.x - xOffset) * xCoef;
				//temp.y = (temp.y - xOffset) * yCoef;
				temp.x = ((temp.x - xOffset) * xCoef) - halfWidth;
				temp.y = ((temp.y - yOffset) * yCoef) - halfHeight;
				//temp.x = (temp.x ) * xCoef - (screenWidth / 2 - screenOffset);
				//temp.y = (temp.y ) * yCoef - (screenHeight / 2 - screenOffset);
				points.Add(temp);
			}

			return points;
		}
	}
}
