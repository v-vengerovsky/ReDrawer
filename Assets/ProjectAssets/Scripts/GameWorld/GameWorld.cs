using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReDrawer
{
	public class GameWorld
	{
		private GameData _data;
		private FigureLoader _figureLoader;
		private InputProcessor _inputProcessor;
		private FigureProcessor _figureProcessor;
		private ScreenSettings _screenSettings;
		private List<Figure> _figures;

		public GameWorld(GameData data)
		{
			_data = data;
			_figureLoader = new FigureLoader();
			_figures = _figureLoader.LoadFigures();
			_screenSettings = new ScreenSettings(Camera.main, _data.OriginalFigureGO);
			_figureProcessor = new FigureProcessor();
			ShowRandomFigure();
		}

		public void Input(BaseEventData data)
		{
			_inputProcessor.ProcessInput(data);
		}

		public void Update()
		{
			_inputProcessor.Update();
		}

		private void ShowRandomFigure()
		{
			if (_figures.Count == 0)
			{
				return;
			}

			int index = Random.Range(0, _figures.Count - 1);
			_data.OriginalPoints = _figureProcessor.GetScaledFigurePoints(_figures[index],_screenSettings.ScreenWidth,_screenSettings.ScreenHeight,_screenSettings.ScreenOffset);
		}
	}
}