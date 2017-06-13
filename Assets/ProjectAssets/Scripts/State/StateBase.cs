using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public abstract class StateBase
	{
		private string _sceneName;
		private ViewBase _viewBase;

		public string SceneName
		{
			get { return _sceneName; }
		}

		public StateBase(string sceneName)
		{
			_sceneName = sceneName;
		}

		public virtual void OnActivate()
		{
			_viewBase = GetView();
		}

		public virtual void OnDeactivate()
		{

		}

		public virtual ViewBase GetView()
		{
			return null;
		}

		public virtual void Update()
		{

		}
	}
}