using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class GameWorld
	{
		private GameData _data;
		private FigureLoader _figureLoader;
		private List<Figure> _figures;

		public GameWorld(GameData data)
		{
			_data = data;
			_figureLoader = new FigureLoader();
			_figures = _figureLoader.LoadFigures();
		}

		public void Update()
		{

		}
	}
}