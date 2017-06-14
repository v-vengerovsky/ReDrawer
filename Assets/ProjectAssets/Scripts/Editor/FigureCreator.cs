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
		private const string _saveFigureButtonName = "Save figure";
		private const string _resetFigureButtonName = "Reset figure";
		private const string _removePointButtonName = "-";

		//size
		private static readonly Vector2 _maxWindowSize = new Vector2(500, _figureHeight + _pointlistHeight + _headerHeight);
		private static readonly Vector2 _minWindowSize = new Vector2(500, _figureHeight + _headerHeight);
		private const float _figureHeight = 500f - _headerHeight;
		private const float _pointlistHeight = 500f;
		private const float _headerHeight = 50f;

		//position
		private static readonly Vector2 _scrollStartPos = new Vector2(0, _figureHeight + _headerHeight);

		//auxiliary data
		private Vector2 _scrollPos;

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
			_instance.minSize = _minWindowSize;
			_instance.maxSize = _maxWindowSize;
			_instance._figureImage = _instance.GenerateTexture(_instance._points, _instance.position.width, _figureHeight);
		}

		private void OnGUI()
		{
			_figureName = EditorGUILayout.TextField("Name",_figureName);

			GUILayout.BeginHorizontal();
			if (GUILayout.Button(_saveFigureButtonName))
			{
				SaveFigure();
			}
			if (GUILayout.Button(_resetFigureButtonName))
			{
				ResetFigure();
			}
			GUILayout.EndHorizontal();

			if (_oldPointsCount != _points.Count)
			{
				_figureImage = GenerateTexture(_points,position.width, _figureHeight);
			}

			_oldPointsCount = _points.Count;
			Rect figureImageRect = new Rect(0, _headerHeight, position.width, _figureHeight);
			EditorGUI.DrawTextureTransparent(figureImageRect, _figureImage);

			GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
			GUIStyle scrollViewStyle = new GUIStyle(GUI.skin.scrollView);
			scrollViewStyle.margin = new RectOffset(1,1, (int)(_figureHeight + _headerHeight), 1);

			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, scrollViewStyle);
			for (int i = 0; i < _points.Count; i++)
			{
				GUILayout.BeginHorizontal();
				EditorGUILayout.TextArea(string.Format("point {0} : {1}", i, _points[i]));

				if (GUILayout.Button(_removePointButtonName,buttonStyle,new GUILayoutOption[] {GUILayout.Width(50) }))
				{
					RemovePointAtIndex(i);
				}
				GUILayout.EndHorizontal();
			}
			EditorGUILayout.EndScrollView();

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

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					image.SetPixel(i,j,Color.white);
				}
			}

			for (int t = 0; t < points.Count - 1; t++)
			{
				DrawSegment(image, points[t], points[t+1], Color.black);
			}

			if (points.Count > 2)
			{
				DrawSegment(image, points[points.Count - 1], points[0], Color.black);
			}

			image.Apply();

			return image;
		}

		private void DrawSegment(Texture2D tex, Vector3 start, Vector3 end, Color color)
		{
			float distance = Vector3.Distance(start, end);
			float angle = Vector3.Angle(Vector3.right, end - start);
			Vector3 segment = end - start;
			int steps = Mathf.RoundToInt(Mathf.Max( Mathf.Abs(segment.x ), Mathf.Abs(segment.y)));

			if (segment.y < 0)
			{
				angle = 360 - Vector3.Angle(Vector3.right, end - start);
			}

			for (int i = 0; i < steps; i++)
			{
				float x = (float)i / steps * segment.x + start.x;
				float y = (float)i / steps * segment.y + start.y;
				tex.SetPixel((int)x, (int)y, color);
			}
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

		private void ResetFigure()
		{
			_figureName = string.Empty;
			_points.Clear();
		}

		private void SaveFigure()
		{
			if (string.IsNullOrEmpty(_figureName) || _points.Count < 2)
			{
				return;
			}

			Figure figure = new Figure(_figureName, _points);
			string serializedFigure = JsonUtility.ToJson(figure, true);
			string projectFolder = System.IO.Directory.GetParent(Application.dataPath).FullName;
			string folderPath = string.Format("{0}/{1}", projectFolder, Constants.FiguresSubpath);
			string filePath = string.Format("{0}/{1}{2}", folderPath, _figureName, Constants.JsonExt);

			if (!System.IO.Directory.Exists(folderPath))
			{
				System.IO.Directory.CreateDirectory(folderPath);
			}

			System.IO.File.WriteAllText(filePath, serializedFigure);
		}

		private void RemovePointAtIndex(int index)
		{
			_points.RemoveAt(index);
		}
	}
}