using UnityEngine;
using UnityEngine.UI;

public class GridElement : MonoBehaviour {
	
	[SerializeField] private Image _image;
	[SerializeField] private RectTransform _rectTransform;

	public RectTransform RectTransform {
		get { return _rectTransform; }
	}
	
	public void SetImageEnabled(bool enabled) {
		
		Color color = _image.color;
		color.a = enabled ? 1 : .3f;
		_image.color = color;
	}
}