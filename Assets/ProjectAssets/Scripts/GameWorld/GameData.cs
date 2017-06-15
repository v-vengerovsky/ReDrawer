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

		private static GameData _instance;

		public static GameData Instance
		{
			get { return _instance; }
		}

		public GameObject OriginalFigureGO { get { return _originalFigure.gameObject; } }
		public GameObject UserFigureGO { get { return _userFigure.gameObject; } }

		public List<Vector3> OriginalPoints
		{
			get
			{
				Vector3[] points = null;
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
				_userFigure.SetPositions(value.ToArray());
			}
		}

		private void Awake()
		{
			_instance = this;
		}
	}
}