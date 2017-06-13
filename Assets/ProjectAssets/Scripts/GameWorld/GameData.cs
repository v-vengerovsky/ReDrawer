using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	[SerializeField]
	private LineRenderer _originalFigure;
	[SerializeField]
	private LineRenderer _userFigure;

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
}
