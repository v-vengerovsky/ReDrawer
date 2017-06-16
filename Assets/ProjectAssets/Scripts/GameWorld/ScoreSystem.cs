using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class ScoreSystem
	{
		private int _score = 0;
		private float _figureTime = Constants.StartFigureTime;
		private float _time = 0;

		private event Action<int> _onScore;
		private event Action _onLoose;

		public event Action<int> OnScore
		{
			add { _onScore += value; }
			remove { _onScore -= value; }
		}

		public event Action OnLoose
		{
			add { _onLoose += value; }
			remove { _onLoose -= value; }
		}

		public int Score { get { return _score; } }

		public ScoreSystem()
		{

		}

		public void Update()
		{
			_time += Time.deltaTime;
			if (_time > _figureTime)
			{
				if (_onLoose != null)
				{
					_onLoose.Invoke();
				}
			}
		}

		public void ScoreFigure()
		{
			_score++;
			_time = 0;
			_figureTime -= Constants.FigureTimeDecrement;

			if (_onScore != null)
			{
				_onScore.Invoke(Score);
			}
		}
	}
}
