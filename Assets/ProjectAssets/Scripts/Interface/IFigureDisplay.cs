using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ReDrawer
{
	public interface IFigureDisplay
	{
		GameObject FigureGO { get; }
		List<Vector3> FigurePoints { get; set; }
	}
}