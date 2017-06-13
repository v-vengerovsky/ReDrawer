using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class GameOverState : StateBase
	{
		public GameOverState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "GameOver").SceneName)
		{

		}

		public override void Update()
		{
			base.Update();

		}
	}
}