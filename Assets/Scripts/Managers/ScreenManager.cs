using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

	[SerializeField] private Screens _screens;

	public void ShowMainScreen(Action playButtonListener, Action beforeShow = null, Action afterShow = null) {
		_screens.MainScreen.Initialize(playButtonListener);
		_screens.MainScreen.Show(beforeShow, afterShow);
	}

	public void ShowPlayScreen(Action leftArrowListener, Action rightArrowListener, Action downArrowListener,
		Action rotateButttonArrowListener,Action beforeShow = null, Action afterShow = null) {

		_screens.PlayScreen.Initialize(leftArrowListener, rightArrowListener, downArrowListener,
			rotateButttonArrowListener);
		
		_screens.PlayScreen.Show(beforeShow, afterShow);
	}

	public void HidePlayScreen(Action beforeHide = null, Action afterHide = null) {
		_screens.PlayScreen.Hide(beforeHide, afterHide);
	}

	public void HideMainScreen(Action beforeHide = null, Action afterHide = null) {
		_screens.MainScreen.Hide(beforeHide,afterHide);
	}
}