using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class FigureAnimator : MonoBehaviour,IFigureDisplay
	{
		private const float _segmentAnimationCompleteTreshold = 0.1f;
		private const float _minAnimationSpeed = 1f;
		private const float _maxAnimationSpeed = 7f;
		private const float _minAnimationSpeedChangeInterval = 0.5f;
		private const float _maxAnimationSpeedChangeInterval = 3f;

		[SerializeField]
		private LineRenderer _originalFigure;

		private FigureLoader _figureLoader;
		private FigureProcessor _figureProcessor;

		private List<Figure> _figures;
		private List<Vector3> _pointsFitted;
		private int _animatingPointIndex;
		private float _time;
		private float _animationSpeed = 1;

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
				return _pointsFitted;
			}
			set
			{
				_pointsFitted = value;
				_time = 0;
				_animatingPointIndex = 0;
			}
		}

		private void Awake()
		{
			_figureLoader = new FigureLoader();
			_figures = _figureLoader.LoadFigures();
			_figureProcessor = new FigureProcessor(this);

			ShowRandomFigure();
		}

		private void Start()
		{
			StartCoroutine(SpeedChangeCoroutine());
		}

		private void ShowRandomFigure()
		{
			if (_figures.Count == 0)
			{
				return;
			}

			int index = UnityEngine.Random.Range(0, _figures.Count - 1);
			_figureProcessor.ShowFigureToDraw(_figures[index]);
		}

		private void Update()
		{
			if (FigurePoints.Count == 0)
			{
				return;
			}

			if (_animatingPointIndex >= FigurePoints.Count)
			{
				ShowRandomFigure();
				return;
			}

			_time += Time.deltaTime * _animationSpeed;
			List<Vector3> points = new List<Vector3>();
			points.AddRange(FigurePoints.GetRange(0,_animatingPointIndex + 1));

			int startAnimatingSegment = _animatingPointIndex;
			int endAnimatingIndex = _animatingPointIndex + 1;

			if (_animatingPointIndex >= FigurePoints.Count - 1)
			{
				endAnimatingIndex = 0;
			}

			float distance = Vector3.Distance(FigurePoints[endAnimatingIndex], FigurePoints[startAnimatingSegment]);
			Vector3 lerpPoint =  Vector3.Lerp(FigurePoints[startAnimatingSegment], FigurePoints[endAnimatingIndex],_time/ distance);
			points.Add(lerpPoint);

			if (Vector3.Distance(FigurePoints[endAnimatingIndex], lerpPoint) < _segmentAnimationCompleteTreshold)
			{
				_animatingPointIndex++;
				_time = 0;
			}

			SetFigurePoints(points);
		}

		private IEnumerator SpeedChangeCoroutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(_minAnimationSpeedChangeInterval, _maxAnimationSpeedChangeInterval));
				_animationSpeed = UnityEngine.Random.Range(_minAnimationSpeed, _maxAnimationSpeed);
			}
		}

		private void SetFigurePoints(List<Vector3> points)
		{
			_originalFigure.positionCount = points.Count;
			_originalFigure.SetPositions(points.ToArray());
		}
	}
}