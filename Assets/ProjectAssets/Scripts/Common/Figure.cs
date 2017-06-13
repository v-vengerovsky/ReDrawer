using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	[Serializable]
	public class Figure
	{
		private string _name;
		private List<Vector3> _points;

		public string Name
		{
			get { return _name; }
		}

		public List<Vector3> Points
		{
			get { return _points; }
		}

		public Figure(string name, List<Vector3> points)
		{
			_name = name;
			_points = points;
		}
	}
}
