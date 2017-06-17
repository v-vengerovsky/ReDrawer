using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class GameData : MonoBehaviour,IFigureDisplay
	{
		[SerializeField]
		private LineRenderer _originalFigure;
		[SerializeField]
		private LineRenderer _userFigure;
		[SerializeField]
		private ParticleSystem _userInputMarkerPrefab;

		private ParticleSystem _userInputMarker;
		private List<ParticleSystem> _usedInputMarkers = new List<ParticleSystem>();

		private static GameData _instance;

		public static GameData Instance
		{
			get { return _instance; }
		}

		public Vector3 InputMarkerPosition
		{
			get { return _userInputMarker.transform.position; }
			set { _userInputMarker.transform.position = value; }
		}

		public bool ShowMarker
		{
			get { return _userInputMarker.isPlaying; }
			set
			{
				if (value)
				{
					_userInputMarker.Play();
				}
				else
				{
					_userInputMarker.Stop();
					_usedInputMarkers.Add(_userInputMarker);
					_userInputMarker = Instantiate<ParticleSystem>(_userInputMarkerPrefab);
					_userInputMarker.Stop();

					for (int i = _usedInputMarkers.Count - 1; i >= 0; i--)
					{
						if (!_usedInputMarkers[i].IsAlive())
						{
							Destroy(_usedInputMarkers[i].gameObject);
							_usedInputMarkers.RemoveAt(i);
						}
					}
				}
			}
		}

		public GameObject FigureGO { get { return _originalFigure.gameObject; } }
		public GameObject UserFigureGO { get { return _userFigure.gameObject; } }

		public List<Vector3> FigurePoints
		{
			get
			{
				Vector3[] points = new Vector3[_originalFigure.positionCount];
				_originalFigure.GetPositions(points);
				List<Vector3> result = new List<Vector3>(points);
				return result;
			}
			set
			{
				_originalFigure.positionCount = value.Count;
				_originalFigure.SetPositions(value.ToArray());
			}
		}

		public List<Vector3> UserPoints
		{
			get
			{
				Vector3[] points = null;
				_userFigure.GetPositions(points);
				List<Vector3> result = new List<Vector3>(points);
				return result;
			}
			set
			{
				_userFigure.positionCount = value.Count;
				_userFigure.SetPositions(value.ToArray());
			}
		}

		public void ClearMarkers()
		{
			_usedInputMarkers.Add(_userInputMarker);
			for (int i = _usedInputMarkers.Count - 1; i >= 0; i--)
			{
				Destroy(_usedInputMarkers[i].gameObject);
				_usedInputMarkers.RemoveAt(i);
			}
		}

		private void Awake()
		{
			_instance = this;
			_userInputMarker = Instantiate<ParticleSystem>(_userInputMarkerPrefab);
			_userInputMarker.Stop();
		}
	}
}