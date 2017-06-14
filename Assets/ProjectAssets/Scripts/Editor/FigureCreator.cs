using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ReDrawer
{
	public class FigureCreator: EditorWindow
	{
		//files
		private const string _figuresSubpath = "Assets/ProjectAssets/Figures";
		private const string _jsonExt = ".json";

		//names
		private const string _addPointButtonName = "Add Point";

		//size
		private static readonly Vector2 _windowSize = new Vector2(500, _figureHeight + _pointlistHeight + _nameHeight);
		private const float _figureHeight = 500f - _nameHeight;
		private const float _pointlistHeight = 500f;
		private const float _nameHeight = 30f;

		//figure image
		private Texture _figureImage;

		//figure data
		private List<Vector3> _points = new List<Vector3>();
		private string _figureName;
		//aux figure data
		private int _oldPointsCount;

		private static FigureCreator _instance;

		[MenuItem("Figure/Create")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
			if (_instance == null)
			{
				_instance = (FigureCreator)EditorWindow.GetWindow(typeof(FigureCreator));
			}

			_instance.Show();
			_instance.minSize = _windowSize;
			_instance.maxSize = _windowSize;
			_instance._figureImage = _instance.GenerateTexture(_instance._points, _instance.position.width, _figureHeight);
		}

		private void OnGUI()
		{
			_figureName = EditorGUILayout.TextField("Name",_figureName);
			//GUIStyle buttonStyle = GUI.skin.button;
			//buttonStyle.fixedWidth = 20;
			////buttonStyle.fixedHeight = 20;
			//if (GUILayout.Button(""))
			//{
			//	Debug.LogWarning("ButtonPressed");
			//}

			if (_oldPointsCount != _points.Count)
			{
				_figureImage = GenerateTexture(_points,position.width, _figureHeight);
			}
			_oldPointsCount = _points.Count;
			Rect figureImageRect = new Rect(0, _nameHeight, position.width, _figureHeight);
			EditorGUI.DrawTextureTransparent(figureImageRect, _figureImage);

			//Debug.LogWarning(string.Format("clickCount:{0} type:{1} keyCode:{2} mousePos:{3}", Event.current.clickCount, Event.current.type, Event.current.keyCode, Event.current.mousePosition));

			switch (Event.current.type)
			{
				case EventType.MouseUp:
					PlacePoint(Event.current.mousePosition, figureImageRect);
					break;
			}
		}

		private Texture2D GenerateTexture(List<Vector3> points, float widthF, float heightF)
		{
			int width = (int)widthF;
			int height = (int)heightF;
			Texture2D image = new Texture2D(width, height);

			//image.SetPixels(0,0,width,height,new Color[] { Color.white });

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					image.SetPixel(i,j,Color.white);
				}
			}

			for (int t = 0; t < points.Count - 1; t++)
			{
				image.SetPixel((int)points[t].x, (int)points[t].y, Color.black);
				Debug.LogWarning(points[t]);
			}

			image.Apply();

			return image;
		}

		private void PlacePoint(Vector2 point, Rect figureImageRect)
		{
			if (point.x >= figureImageRect.x && point.x <= figureImageRect.width + figureImageRect.x 
				&& 
				point.y >= figureImageRect.y && point.y <= figureImageRect.height + figureImageRect.y)
			{
				point.x -= figureImageRect.x;
				point.y = figureImageRect.height + figureImageRect.y - point.y;
				_points.Add(point);
			}
		}

		private void SaveFigure()
		{

		}
	}
}