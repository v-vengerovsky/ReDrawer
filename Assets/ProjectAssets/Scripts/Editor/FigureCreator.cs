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
		private static readonly Vector2 _windowSize = new Vector2(500, 1000);

		//figure data
		private List<Vector3> _points = new List<Vector3>();
		private string _figureName;

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

			Debug.LogWarning(string.Format("clickCount:{0} type:{1} keyCode:{2} mousePos:{3}", Event.current.clickCount, Event.current.type, Event.current.keyCode, Event.current.mousePosition));

			switch (Event.current.type)
			{
				case EventType.MouseUp:
					PlacePoint(Event.current.mousePosition);
					break;
			}
		}

		private void PlacePoint(Vector2 point)
		{

		}

		private void SaveFigure()
		{

		}
	}
}