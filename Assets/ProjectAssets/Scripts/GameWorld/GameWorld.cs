using System;
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
		private ScoreSystem _scoreSysytem;
		private List<Figure> _figures;

		public event Action<int> OnScore
		{
			add { _scoreSysytem.OnScore += value; }
			remove { _scoreSysytem.OnScore -= value; }
		}

		public GameWorld(GameData gameData)
		{
			_gameData = gameData;
			_figureLoader = new FigureLoader();
			_figures = _figureLoader.LoadFigures();
			_figureProcessor = new FigureProcessor(_gameData);
			_scoreSysytem = new ScoreSystem();
			_scoreSysytem.OnLoose += Lost;
			_inputProcessor = new InputProcessor(_gameData);
			_inputProcessor.OnScored += ScoredFigure;
			_inputProcessor.OnScored += _scoreSysytem.ScoreFigure;
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
			_scoreSysytem.Update();
		}

		private void ScoredFigure()
		{
			ShowRandomFigure();
		}

		private void FailedToScoreFigure()
		{

		}

		private void Lost()
		{
			Approot.Instance.SetState(new GameOverState());
		}

		private void ShowRandomFigure()
		{
			if (_figures.Count == 0)
			{
				return;
			}

			int index = UnityEngine.Random.Range(0, _figures.Count);
			_figureProcessor.ShowFigureToDraw(_figures[index]);
		}
	}
}