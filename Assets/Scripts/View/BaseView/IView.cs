using System;

public interface IView {
    
    void Show(Action beforeShow, Action afterShow);
        
    void Hide(Action beforeHide, Action afterHide);
    
}