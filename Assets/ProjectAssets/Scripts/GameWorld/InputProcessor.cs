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
		private const float _minDistance = 100f;
		private const float _maxCloseDistance = 20f;

		private GameData _gamedata;
		private List<Vector2> _points;
		private float _lastInputTime;
		private float _totalDistance;

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
				Reset();
			}

			_lastInputTime = Time.time;
			float distance = 0f;

			if (_points.Count == 0)
			{
				_points.Add(pointerData.position);
			}
			else
			{
				distance = Vector2.Distance(_points[_points.Count - 1], pointerData.position);

				if (distance > Constants.InputAddPointDistanceTreshold)
				{
					_points.Add(pointerData.position);
					_totalDistance += distance;

					float distanceToFirstPoint = Vector2.Distance(_points[0], _points[_points.Count - 1]);
					if (distanceToFirstPoint < _maxCloseDistance 
						&& 
						_totalDistance > _minDistance)
					{
						//scored
						if (ScoredFigure())
						{

						}//failed
						else
						{
							Debug.LogWarning("figure closed");
						}
						Reset();
					}
				}
			}

			Debug.LogFormat("points :{0} distance:{1}",_points.Count,_totalDistance);
		}

		private bool ScoredFigure()
		{
			List<Vector3> originalPoints = _gamedata.OriginalPoints;
			float angle;
			float paralelTransferX;
			float paralelTransferY;
			bool scored = false;

			float originalMinX;
			float originalMaxX;
			float originalMinY;
			float originalMaxY;

			Utils.GetMaxMinXY(originalPoints, out originalMinX,out originalMaxX,out originalMinY,out originalMaxY);

			float originalRangeX = originalMaxX - originalMinX;
			float originalRangeY = originalMaxY - originalMinY;
			List<Vector3> normalizedInputPoints = Utils.GetNormalizedPointsXY(_points, originalRangeX, originalRangeY);

			foreach (var item in _points)
			{
				for (int i = 0; i < originalPoints.Count - 1; i++)
				{

				}
			}
			return scored;
		}

		private void Reset()
		{
			_points.Clear();
			_totalDistance = 0f;
		}

		public void Update()
		{

		}
	}
}
