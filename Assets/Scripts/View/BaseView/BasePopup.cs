using System;
using System.Collections;
using UnityEngine;

public abstract class BasePopup : MonoBehaviour, IView {

    [SerializeField] private bool _destroyAfterHide = true;

    
    public abstract void Initialize(Action onAccept, Action onDecline, string message);

    public virtual void Show(Action afteShow = null) {
        StartCoroutine(show(afteShow));
    }

    public virtual void Hide(Action afterHide = null) {

        afterHide += () => {
            if (_destroyAfterHide) {
                Destroy(gameObject);
            }
        };

        StartCoroutine(hide(afterHide));
    }

    private IEnumerator show(Action afterHide = null) {
        float t = 0;

        while (t < 1) {
            t += Time.deltaTime * 2;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }
        
        transform.localScale = Vector3.one;

        if (afterHide != null) {
            afterHide();
        }
    }

    private IEnumerator hide(Action afterHide = null) {
        float t = 0;

        while (t < 1) {
            t += Time.deltaTime * 2;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return null;
        }

        transform.localScale = Vector3.zero;

        if (afterHide != null) {
            afterHide();
        }
    }
}