using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopupsManager : MonoBehaviour {
    private const string PopupsPath = "Popups";

    [SerializeField] private Transform _containter;
    [SerializeField] private Image _backdrop;

    private BasePopup _currentPopup;
    private EventSystemManager _eventSystemManager;

    public void Initialize(EventSystemManager eventSystemManager) {
        _eventSystemManager = eventSystemManager;
    }

    public void ShowDialogPopup(Action onAccept, Action onDecline, string message, string acceptText = "Accept",
        string declineText = "Decline", Action beforeShow = null,
        Action afterShow = null) {
        DialogPopup popup =
            Instantiate(Resources.Load<DialogPopup>(Path.Combine(PopupsPath, typeof(DialogPopup).ToString())));

        _currentPopup = popup;
        _backdrop.enabled = true;

        beforeShow += _eventSystemManager.TurnOff;
        afterShow += _eventSystemManager.TurnOn;
        onAccept += HideCurrentPopup;
        onDecline += HideCurrentPopup;

        popup.transform.SetParent(_containter, false);
        popup.Initialize(onAccept, onDecline, message, acceptText, declineText);
        popup.Show(beforeShow, afterShow);
    }

    private void HideCurrentPopup() {
        _currentPopup.Hide(() => { _eventSystemManager.TurnOff(); }, () => {
            _backdrop.enabled = false;
            _eventSystemManager.TurnOn();
        });
    }
}