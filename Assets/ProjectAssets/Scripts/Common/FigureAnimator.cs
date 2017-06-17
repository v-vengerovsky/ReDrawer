using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class FigureAnimator : MonoBehaviour,IFigureDisplay
	{
		[SerializeField]
		private LineRenderer _originalFigure;

		private FigureLoader _figureLoader;
		private FigureProcessor _figureProcessor;

		public GameObject FigureGO
		{
			get
			{
				return _originalFigure.gameObject;
			}
		}

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

		private void Awake()
		{
			_figureLoader = new FigureLoader();
			_figureProcessor = new FigureProcessor(this);
		}
	}
}