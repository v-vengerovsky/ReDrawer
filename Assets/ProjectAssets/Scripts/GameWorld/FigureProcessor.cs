using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class FigureProcessor
	{
		private IFigureDisplay _figureDisplay;
		private ScreenSettings _screenSettings;

		public FigureProcessor(IFigureDisplay figureDisplay)
		{
			_figureDisplay = figureDisplay;
			_screenSettings = new ScreenSettings(Camera.main, _figureDisplay,null);
		}

		public void ShowFigureToDraw(Figure figure)
		{
			_figureDisplay.FigurePoints = GetScaledFigurePoints(figure,_screenSettings);
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
