using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen {

	[Header("Buttons")] [Space]
	[SerializeField] private Button _playButton;

	public void Initialize(Action playButtonListener) {
		_playButton.onClick.AddListener(() => { playButtonListener(); });
	}

	public override void Hide(Action afterHide = null) {
		_playButton.onClick.RemoveAllListeners();
		base.Hide(afterHide);
	}
}
