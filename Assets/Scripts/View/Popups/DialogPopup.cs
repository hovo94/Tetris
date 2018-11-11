
using System;
using UnityEngine;
using UnityEngine.UI;


public class DialogPopup : BasePopup {

	[SerializeField] private Button _onAccept;
	[SerializeField] private Button _onDecline;
	[SerializeField] private Text _messageText;

	public override void Initialize(Action onAccept, Action onDecline,string message) {
		_onAccept.onClick.AddListener(() => { onAccept(); });
		_onDecline.onClick.AddListener(() => { onDecline(); });
		_messageText.text = message;
	}
	
}
