using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : BaseScreen {

	[Header("Buttons")] [Space]
	[SerializeField] private Button _leftArrow;
	[SerializeField] private Button _rightArrow;
	[SerializeField] private Button _downArrow;
	[SerializeField] private Button _rotate;

	[Space] [SerializeField] private GridView _gridView;

	public GridView GridView {
		get { return _gridView; }
	}

	public void Initialize(Action leftArrowListener, Action rightArrowListener, Action downArrowListener,
		Action rotateButttonArrowListener) {
		_leftArrow.onClick.AddListener(() => { leftArrowListener(); });
		_rightArrow.onClick.AddListener(() => { rightArrowListener(); });
		_downArrow.onClick.AddListener(() => { downArrowListener(); });
		_rotate.onClick.AddListener(() => { rotateButttonArrowListener(); });
	}

	protected override void RemoveAllButtonListeners() {
		_leftArrow.onClick.RemoveAllListeners();
		_rightArrow.onClick.RemoveAllListeners();
		_downArrow.onClick.RemoveAllListeners();
		_rotate.onClick.RemoveAllListeners();
	}
}