using System;
using System.Collections;
using UnityEngine;

public abstract class BasePopup : BaseView {

    [SerializeField] private bool _destroyAfterHide = true;
    
    protected override IEnumerator show(Action afterHide) {
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

    protected override IEnumerator hide(Action afterHide) {
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
        
        if (_destroyAfterHide) {
            Destroy(gameObject);
        }
    }
}