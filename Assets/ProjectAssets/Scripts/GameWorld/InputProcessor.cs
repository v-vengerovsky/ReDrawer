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
		private float _totalDistance;
		private bool _ignore;

		//events
		private event Action _onScored;
		private event Action _onFailed;

		public event Action OnScored
		{
			add { _onScored += value; }
			remove { _onScored -= value; }
		}

		public event Action OnFailed
		{
			add { _onFailed += value; }
			remove { _onFailed -= value; }
		}

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

			if (pointerData.clickTime > _lastInputTime)
			{
				Reset();
			}

			_lastInputTime = pointerData.clickTime;

			if (_ignore)
			{
				return;
			}

			Debug.LogFormat("clickTime:{0}", pointerData.clickTime);

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
					Debug.LogFormat("points :{0} distance:{1} distance to first point:{2}", _points.Count, _totalDistance, distanceToFirstPoint);

					if (distanceToFirstPoint < Constants.MaxCloseDistance
						&&
						_totalDistance > Constants.MinDistance)
					{
						//scored
						if (ScoredFigure())
						{
							if (_onScored != null)
							{
								_onScored.Invoke();
							}
						}//failed
						else
						{
							if (_onFailed != null)
							{
								_onFailed.Invoke();
							}
						}

						Reset();
						_ignore = true;
					}
				}
			}
		}

		private bool ScoredFigure()
		{
			List<Vector3> originalPoints = _gamedata.OriginalPoints;
			float perimeter = Utils.GetPerimeter(originalPoints);
			float inputPerimeter = 0f;

			float angle;

			//paralell transfer
			float pTX;
			float pTY;

			//trigonometric func
			float cos;
			float sin;

			//rotation transfer
			float rTX;
			float rTY;

			//rebased segment x coord
			float rX;

			bool scored = false;

			float originalMinX;
			float originalMaxX;
			float originalMinY;
			float originalMaxY;

			Utils.GetMaxMinXY(originalPoints, out originalMinX,out originalMaxX,out originalMinY,out originalMaxY);

			float originalRangeX = originalMaxX - originalMinX;
			float originalRangeY = originalMaxY - originalMinY;
			List<Vector3> normalizedInputPoints = Utils.GetStretchedPointsXY(_points, originalRangeX, originalRangeY);

			for (int j = 0; j < normalizedInputPoints.Count; j++)
			{
				Vector3 segment;
				bool scoredPoint = false;

				if (j < normalizedInputPoints.Count - 1)
				{
					segment = normalizedInputPoints[j + 1] - normalizedInputPoints[j];
				}
				else
				{
					segment = normalizedInputPoints[0] - normalizedInputPoints[j];
				}

				var item = normalizedInputPoints[j];
				//point check and projection count
				for (int i = 0; i < originalPoints.Count; i++)
				{
					Vector3 originalSegment;

					if (i < originalPoints.Count - 1)
					{
						originalSegment = originalPoints[i + 1] - originalPoints[i];
					}
					else
					{
						originalSegment = originalPoints[0] - originalPoints[i];
					}

					angle = Vector3.Angle(Vector3.right, originalSegment);

					if (originalSegment.y < 0)
					{
						angle = 360 - angle;
					}

					//parallel transfer
					pTX = item.x - originalPoints[i].x;
					pTY = item.y - originalPoints[i].y;

					//trigonometric func
					cos = Mathf.Cos(angle / 180 * Mathf.PI);
					sin = Mathf.Sin(angle / 180 * Mathf.PI);

					//rotation transfer
					rTX = pTX * cos + pTY * sin;
					rTY = -pTX * sin + pTY * cos;

					rX = originalSegment.x * cos + originalSegment.y * sin;

					//base check for proximity to figure segment
					if (Mathf.Abs(rTY) < Constants.InputMaxDistanceToFigure && rTX > -Constants.InputMaxDistanceToFigure && rTX < rX + Constants.InputMaxDistanceToFigure)
					{
						scoredPoint = true;

						//projection on original figure counting
						//Vector3.Dot(segment,originalSegment)/
						inputPerimeter += Vector3.Project(segment, originalSegment).magnitude * Mathf.Sign(Vector3.Dot(segment, originalSegment));
						break;
					}
				}

				if (!scoredPoint)
				{
					return false;
				}
			}

			return Mathf.Abs(inputPerimeter) > perimeter / 2;
		}

		private void Reset()
		{
			_points.Clear();
			_totalDistance = 0f;
			_ignore = false;
		}

		public void Update()
		{

		}
	}
}
