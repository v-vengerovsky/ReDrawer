using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDrawer
{
	public class PlayState : StateBase
	{
		public PlayState() : base(Approot.Instance.SceneLoader.Scenes.Find(scene => scene.SceneName == "Play").SceneName)
		{

		}

		public override void Update()
		{
			base.Update();

		}
	}
}