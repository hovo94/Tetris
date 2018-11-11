﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : BaseScreen {

	[Header("Buttons")] [Space]
	[SerializeField] private Button _leftArrow;
	[SerializeField] private Button _rightArrow;
	[SerializeField] private Button _downArrow;
	[SerializeField] private Button _rotate;

	public void Initialize(Action leftArrowListener, Action rightArrowListener, Action downArrowListener,
		Action rotateButttonArrowListener) {
		_leftArrow.onClick.AddListener(() => { leftArrowListener(); });
		_rightArrow.onClick.AddListener(() => { rightArrowListener(); });
		_downArrow.onClick.AddListener(() => { downArrowListener(); });
		_rotate.onClick.AddListener(() => { rotateButttonArrowListener(); });
	}

}