using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class PlayState : StateBase
	{
		private PlayView _view;
		private GameWorld _gameWorld;

		public PlayState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "Play").SceneName)
		{

		}

		public override void OnActivate()
		{
			base.OnActivate();
			_gameWorld = new GameWorld(GameData.Instance);
			_view.OnDrag += _gameWorld.Input;
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();
			_view.OnDrag -= _gameWorld.Input;
		}

		public override ViewBase GetView()
		{
			_view = UiRoot.GetView<PlayView>();
			return _view;
		}

		public override void Update()
		{
			base.Update();
			_gameWorld.Update();
		}
	}
}