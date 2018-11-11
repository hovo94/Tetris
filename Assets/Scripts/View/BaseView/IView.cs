
using System;

public interface IView {

	void Show(Action afterShow = null);

	void Hide(Action afterHide = null);

}
