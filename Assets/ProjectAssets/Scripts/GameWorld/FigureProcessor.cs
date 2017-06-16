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
			_screenSettings = new ScreenSettings(Camera.main, _gamedata);
		}

		public void ShowFigureToDraw(Figure figure)
		{
			_gamedata.OriginalPoints = GetScaledFigurePoints(figure,_screenSettings);
		}

		private List<Vector3> GetScaledFigurePoints(Figure figure, ScreenSettings screenSettings)
		{
			float width = screenSettings.ScreenWidth - 2 * screenSettings.ScreenOffset;
			float height = screenSettings.ScreenHeight - 2 * screenSettings.ScreenOffset;

			List<Vector3> points = Utils.GetFittedRatioPointsXY(figure.Points,width,height);

			return points;
		}
	}
}
