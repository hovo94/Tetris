using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopupsManager : MonoBehaviour {

	private readonly string POPUPS_PATH = "Popups";

	[SerializeField] private Transform _containter;
	[SerializeField] private Image _backdrop;

	private BasePopup _currentPopup;

	public void ShowDialogPopup<T>(Action onAccept, Action onDecline, string message,
		Action afterShow = null) where T : BasePopup {
		BasePopup popup = Instantiate(Resources.Load<T>(Path.Combine(POPUPS_PATH, typeof(T).ToString())));
		
		_currentPopup = popup;
		_backdrop.enabled = true;
		
		GameManager.Instance.EventSystemManager.TurnOff();

		afterShow += () => { GameManager.Instance.EventSystemManager.TurnOn(); };

		popup.transform.SetParent(_containter, false);
		popup.Initialize(onAccept, onDecline, message);
		popup.Show(afterShow);
	}

	public void Hide(Action afterHide = null) {
		GameManager.Instance.EventSystemManager.TurnOff();
		
		afterHide += () => {
			_backdrop.enabled = false;
			GameManager.Instance.EventSystemManager.TurnOn();
		};
		
		_currentPopup.Hide(afterHide);
	}
}