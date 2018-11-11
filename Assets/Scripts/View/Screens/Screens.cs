using System;
using UnityEngine;

[Serializable]
public class Screens {

	[SerializeField] private MainScreen _mainScreen;
	[SerializeField] private PlayScreen _playScreen;

	public MainScreen MainScreen {
		get { return _mainScreen; }
	}

	public PlayScreen PlayScreen {
		get { return _playScreen; }
	}
}
