using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class MenuState : StateBase
	{
		private MenuView _view;

		public MenuState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "Menu").SceneName)
		{

		}

		public override ViewBase GetView()
		{
			_view = UiRoot.GetView<MenuView>();
			return _view;
		}

		public override void Update()
		{
			base.Update();

		}
	}
}