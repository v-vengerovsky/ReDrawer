using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class GameData : MonoBehaviour
	{
		[SerializeField]
		private LineRenderer _originalFigure;
		[SerializeField]
		private LineRenderer _userFigure;
		[SerializeField]
		private GameObject _userInputMarker;

		private static GameData _instance;

		private Vector3 _targetForInputMarker;

		public static GameData Instance
		{
			get { return _instance; }
		}

		public Vector3 TargetForInputMarker
		{
			//get { return _targetForInputMarker; }
			//set { _targetForInputMarker = value; }
			get { return _userInputMarker.transform.position; }
			set { _userInputMarker.transform.position = value; }
		}

		public GameObject OriginalFigureGO { get { return _originalFigure.gameObject; } }
		public GameObject UserFigureGO { get { return _userFigure.gameObject; } }

		public List<Vector3> OriginalPoints
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

		private void Awake()
		{
			_instance = this;
		}
	}
}