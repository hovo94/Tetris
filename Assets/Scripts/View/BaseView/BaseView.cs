using System;
using System.Collections;
using UnityEngine;

public abstract class BaseView : MonoBehaviour, IView {

	protected abstract IEnumerator show(Action afterShow);

	protected abstract IEnumerator hide(Action afterHide);

	public virtual void Show(Action beforeShow, Action afterShow) {
		if (beforeShow != null) {
			beforeShow();
		}
        
		StartCoroutine(show(afterShow));
	}

	public virtual void Hide(Action beforeHide, Action afterHide) {
		if (beforeHide != null) {
			beforeHide();
		}

		StartCoroutine(hide(afterHide));
	}

}
