using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public abstract class StateBase
	{
		private string _sceneName;

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

		}

		public virtual void OnDeactivate()
		{

		}

		public virtual void Update()
		{

		}
	}
}