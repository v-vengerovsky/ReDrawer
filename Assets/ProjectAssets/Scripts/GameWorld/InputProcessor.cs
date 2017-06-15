using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReDrawer
{
	public class InputProcessor
	{
		private GameData _gamedata;
		private List<Vector2> _points;
		private float _lastInputTime;

		public InputProcessor(GameData gamedata)
		{
			_gamedata = gamedata;
			_points = new List<Vector2>();
		}

		public void ProcessInput(BaseEventData data)
		{
			PointerEventData pointerData = data as PointerEventData;

			if (pointerData == null)
			{
				return;
			}

			if (Time.time - _lastInputTime > Time.deltaTime * 2)
			{
				_points.Clear();
			}

			_lastInputTime = Time.time;

			if (_points.Count == 0 || Vector2.Distance(_points[_points.Count - 1], pointerData.position) > Constants.InputAddPointDistanceTreshold)
			{
				_points.Add(pointerData.position);
			}

			Debug.LogWarningFormat("points quantity:{0}",_points.Count);
		}

		public void Update()
		{

		}
	}
}
