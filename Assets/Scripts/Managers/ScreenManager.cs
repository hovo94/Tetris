using System;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

	[SerializeField] private Screens _screens;

	private BaseScreen _currentScreen;

	public void ShowMainScreen(Action playButtonListener, Action afterShow = null) {
		GameManager.Instance.EventSystemManager.TurnOff();
		afterShow += () => { GameManager.Instance.EventSystemManager.TurnOn();};
		
		_screens.MainScreen.Initialize(playButtonListener);
		_currentScreen = _screens.MainScreen;
		_currentScreen.Show(afterShow);		

	}

	public void ShowPlayScreen(Action leftArrowListener, Action rightArrowListener, Action downArrowListener,
		Action rotateButttonArrowListener, Action afterShow = null) {
		GameManager.Instance.EventSystemManager.TurnOff();
		afterShow += () => { GameManager.Instance.EventSystemManager.TurnOn();};

		_screens.PlayScreen.Initialize(leftArrowListener, rightArrowListener, downArrowListener,
			rotateButttonArrowListener);
		
		_currentScreen = _screens.PlayScreen;
		_screens.PlayScreen.Show(afterShow);		
	}

	public void HideScreen(Action afterHide = null) {
		GameManager.Instance.EventSystemManager.TurnOff();
		afterHide += () => { GameManager.Instance.EventSystemManager.TurnOn();};
		
		_currentScreen.Hide(afterHide);
	}
}