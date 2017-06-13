using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ReDrawer
{
	public class FigureCreator: EditorWindow
	{
		private const string _figuresSubpath = "Assets/ProjectAssets/Figures";
		private const string _jsonExt = ".json";
		private List<Vector3> _points = new List<Vector3>();
		private string _figureName;

		[MenuItem("Figure/Create")]
		static void Init()
		{
			// Get existing open window or if none, make a new one:
			FigureCreator window = (FigureCreator)EditorWindow.GetWindow(typeof(FigureCreator));
			window.Show();
		}

		private void OnGUI()
		{
			GUILayout.BeginHorizontal();
			_figureName = EditorGUILayout.TextField("Name",_figureName);
			GUIStyle buttonStyle = new GUIStyle();
			buttonStyle.fixedWidth = 20;
			//buttonStyle.fixedHeight = 20;
			if (GUILayout.Button("+", buttonStyle))
			{
				Debug.LogWarning("ButtonPressed");
			}
			GUILayout.EndHorizontal();
		}

		private void SaveFigure()
		{

		}
	}
}