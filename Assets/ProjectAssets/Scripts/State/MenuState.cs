using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class MenuState : StateBase
	{
		public MenuState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "Menu").SceneName)
		{

		}

		public override void Update()
		{
			base.Update();

		}
	}
}