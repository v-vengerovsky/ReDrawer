using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public class FigureLoader
	{
		public List<Figure> LoadFigures()
		{
			List<Figure> result = new List<Figure>();
			TextAsset[] textAssets = Resources.LoadAll<TextAsset>(Constants.FiguresResourcesSubpath);

			foreach (var item in textAssets)
			{
				Figure figureItem = JsonUtility.FromJson<Figure>(item.text);

				if (figureItem != null)
				{
					result.Add(figureItem);
				}
			}

			return result;
		}
	}
}
