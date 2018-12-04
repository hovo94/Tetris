using System;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : BaseScreen {

	[Header("Buttons")] [Space]
	[SerializeField] private Button _playButton;

	public void Initialize(Action playButtonListener) {
		_playButton.onClick.AddListener(() => { playButtonListener(); });
	}

	protected override void RemoveAllButtonListeners() {
		_playButton.onClick.RemoveAllListeners();
	}
}
