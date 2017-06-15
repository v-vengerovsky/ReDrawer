using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReDrawer
{
	public class PlayView : ViewBase
	{
		[SerializeField]
		private Text _scoreText;
		private int _score;

		[SerializeField]
		private EventTrigger _trigger;
		private event Action<BaseEventData> _onDrag;

		public int Score
		{
			get
			{
				return _score;
			}

			set
			{
				_score = value;
				UpdateView();
			}
		}

		public event Action<BaseEventData> OnDrag
		{
			add { _onDrag += value; }
			remove { _onDrag -= value; }
		}

		private void Awake()
		{
			Score = 0;

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(Drag);
			_trigger.triggers.Add(entry);
		}

		private void Drag(BaseEventData data)
		{
			//Debug.LogWarningFormat("Drag {0}",data);
			if (_onDrag != null)
			{
				_onDrag.Invoke(data);
			}
		}

		private void UpdateView()
		{
			_scoreText.text = _score.ToString();
		}
	}
}