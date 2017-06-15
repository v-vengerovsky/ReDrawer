using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class FigureProcessor
	{
		private GameData _gamedata;
		private ScreenSettings _screenSettings;

		public FigureProcessor(GameData gamedata)
		{
			_gamedata = gamedata;
			_screenSettings = new ScreenSettings(Camera.main, _gamedata.OriginalFigureGO);
		}

		public void ShowFigureToDraw(Figure figure)
		{
			_gamedata.OriginalPoints = GetScaledFigurePoints(figure,_screenSettings);
		}

		private List<Vector3> GetScaledFigurePoints(Figure figure, ScreenSettings screenSettings)
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

				if (item.y > maxY)
				{
					maxY = item.y;
				}
			}

			float width = screenSettings.ScreenWidth - 2 * screenSettings.ScreenOffset;
			float height = screenSettings.ScreenHeight - 2 * screenSettings.ScreenOffset;
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
				yCoef *= screenRatio / figureRatio;
				halfHeight *= screenRatio / figureRatio;
			}
			else
			{
				xCoef *= figureRatio / screenRatio;
				halfWidth *= figureRatio / screenRatio;
			}

			Vector3 temp;
			for (int i = 0; i < figure.Points.Count; i++)
			{
				temp = figure.Points[i];
				temp.x = ((temp.x - xOffset) * xCoef) - halfWidth;
				temp.y = ((temp.y - yOffset) * yCoef) - halfHeight;
				points.Add(temp);
			}

			return points;
		}
	}
}
