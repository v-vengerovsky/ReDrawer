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
		private event Action<BaseEventData> _onPointerUp;

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

		public event Action<BaseEventData> OnPointerUp
		{
			add { _onPointerUp += value; }
			remove { _onPointerUp -= value; }
		}

		public void SetScore(int score)
		{
			Score = score;
		}

		private void Awake()
		{
			Score = 0;

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(Drag);
			_trigger.triggers.Add(entry);

			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(PointerUp);
			_trigger.triggers.Add(entry);
		}

		private void Drag(BaseEventData data)
		{
			if (_onDrag != null)
			{
				_onDrag.Invoke(data);
			}
		}

		private void PointerUp(BaseEventData data)
		{
			if (_onPointerUp != null)
			{
				_onPointerUp.Invoke(data);
			}
		}

		private void UpdateView()
		{
			_scoreText.text = _score.ToString();
		}
	}
}