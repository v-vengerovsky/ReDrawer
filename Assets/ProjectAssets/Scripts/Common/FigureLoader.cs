using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class FigureLoader
	{
		private const string _figuresSubpath = "Assets/ProjectAssets/Figures";
		private const string _jsonExt = ".json";

		public List<Figure> LoadFigures()
		{
			List<Figure> result = new List<Figure>();
			TextAsset[] textAssets = Resources.LoadAll<TextAsset>(_figuresSubpath);

			foreach (var item in textAssets)
			{
				if (item.name.Contains(_jsonExt))
				{
					Figure figureItem = JsonUtility.FromJson<Figure>(item.text);

					if (figureItem != null)
					{
						result.Add(figureItem);
					}
				}
			}

			return result;
		}
	}
}
