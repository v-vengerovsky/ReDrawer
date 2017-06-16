using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReDrawer
{
	public class Constants
	{
		//paths
		public const string FiguresResourcesSubpath = "Figures";
		public const string FiguresSubpath = "Assets/ProjectAssets/Resources/Figures";
		public const string JsonExt = ".json";

		//input processor
		public const float InputAddPointDistanceTreshold = 10f;
		public const float InputMaxDistanceToFigure = 1f;
		public const float MinDistance = 100f;
		public const float MaxCloseDistance = 50f;

		//score system
		//public const float StartFigureTime = 10f;
		public const float StartFigureTime = 100000f;
		public const float FigureTimeDecrement = 0.5f;

		//formats
		public const string ScoreFormat = "Score: {0}";
	}
}
