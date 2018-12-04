using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : BasePopup {

	[SerializeField] private Button _acceptButton;
	[SerializeField] private Button _declineButton;
	[SerializeField] private Text _acceptText;
	[SerializeField] private Text _declineText;
	[SerializeField] private Text _messageText;


	public void Initialize(Action onAccept, Action onDecline, string message, string acceptText, string declineText) {
		_acceptText.text = acceptText;
		_declineText.text = declineText;
		_acceptButton.onClick.AddListener(() => { onAccept(); });
		_declineButton.onClick.AddListener(() => { onDecline(); });
		_messageText.text = message;
	}
	
}
