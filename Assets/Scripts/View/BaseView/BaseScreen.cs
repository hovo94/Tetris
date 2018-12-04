using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseScreen : BaseView {

	[SerializeField] protected Canvas _canvas;
	[SerializeField] protected CanvasGroup _canvasGroup;
	[SerializeField] protected GraphicRaycaster _graphicRaycaster;

	protected abstract void RemoveAllButtonListeners();

	public override void Hide(Action beforeHide, Action afterHide) {
		RemoveAllButtonListeners();
		base.Hide(beforeHide, afterHide);
	}

	protected override IEnumerator show(Action afterShow) {

		_canvas.enabled = true;
		
		float t = 0;

		while (t < 1) {
			t += Time.deltaTime;
			_canvasGroup.alpha = t;
			yield return null;
		}

		_canvasGroup.alpha = 1;
		_graphicRaycaster.enabled = true;

		if (afterShow != null) {
			afterShow();
		}
	}

	protected override IEnumerator hide(Action afterHide) {
		_graphicRaycaster.enabled = false;
		
		float t = 1;

		while (t > 0) {
			t -= Time.deltaTime;
			_canvasGroup.alpha = t;
			yield return null;
		}

		_canvasGroup.alpha = 0;
		_canvas.enabled = false;

		if (afterHide != null) {
			afterHide();
		}
	}
}
