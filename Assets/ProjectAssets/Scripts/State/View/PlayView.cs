using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReDrawer
{
	public class PlayView : ViewBase
	{
		[SerializeField]
		private Text _scoreText;
		private int _score;

		public int Score
		{
			get
			{
				return _score;
			}

			set
			{
				_score = value;
				UpdateView();
			}
		}

		private void Awake()
		{
			Score = 0;
		}

		private void UpdateView()
		{
			_scoreText.text = _score.ToString();
		}
	}
}