using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class GameOverState : StateBase
	{
		private GameOverView _view;

		public GameOverState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "GameOver").SceneName)
		{

		}

		public override ViewBase GetView()
		{
			_view = UiRoot.GetView<GameOverView>();
			return _view;
		}

		public override void Update()
		{
			base.Update();

		}
	}
}