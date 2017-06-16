using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReDrawer
{
	public class GameWorld
	{
		private GameData _gameData;
		private FigureLoader _figureLoader;
		private InputProcessor _inputProcessor;
		private FigureProcessor _figureProcessor;
		private List<Figure> _figures;

		public GameWorld(GameData gameData)
		{
			_gameData = gameData;
			_figureLoader = new FigureLoader();
			_figures = _figureLoader.LoadFigures();
			_figureProcessor = new FigureProcessor(_gameData);
			_inputProcessor = new InputProcessor(_gameData);
			_inputProcessor.OnScored += ScoredFigure;
			_inputProcessor.OnFailed += FailedToScoreFigure;
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

		private void ScoredFigure()
		{
			ShowRandomFigure();
			Debug.LogWarning("ScoreFigure");
		}

		private void FailedToScoreFigure()
		{
			Debug.LogWarning("FailedToScoreFigure");
		}

		private void ShowRandomFigure()
		{
			if (_figures.Count == 0)
			{
				return;
			}

			int index = Random.Range(0, _figures.Count - 1);
			_figureProcessor.ShowFigureToDraw(_figures[index]);
		}
	}
}