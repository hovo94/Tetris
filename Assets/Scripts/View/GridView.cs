using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour {

	[SerializeField] private GameObject _gridElementPrefab;
	[SerializeField] private RectTransform _containerRectTransform;
	[SerializeField] private float _horizontalSpacing;
	[SerializeField] private float _verticalSpacing;
	
	private GridElement[,] _gridElements;
	
	public void CreateGridView(int columnsCount,int rowsCount) {
		
		_gridElements = new GridElement[columnsCount, rowsCount];
		
		GridElement gridElement = null;
		
		for (int y = 0; y < rowsCount; y++) {
			for (int x = 0; x < columnsCount; x++) {
				
				_gridElements[x, y] = Instantiate(_gridElementPrefab).GetComponent<GridElement>();
				gridElement = _gridElements[x, y];
				gridElement.transform.SetParent(_containerRectTransform.transform);
				gridElement.transform.localScale = Vector3.one;
				gridElement.RectTransform.anchoredPosition = new Vector2(
					x  * gridElement.RectTransform.rect.width +  _horizontalSpacing * x,
					-y * gridElement.RectTransform.rect.height + _verticalSpacing * -y);
			}
		}

		_containerRectTransform.sizeDelta =
			new Vector2(columnsCount * gridElement.RectTransform.rect.width + (columnsCount - 1) * _horizontalSpacing,
				rowsCount * gridElement.RectTransform.rect.height + (rowsCount - 1) * _verticalSpacing);
	}

	public void SetGridElementEnabled(MatrixVector matrixVector, bool enabled) {
		_gridElements[matrixVector.x,matrixVector.y].SetImageEnabled(enabled);
	}

	public void ClearGrid() {
		foreach (GridElement element in _gridElements) {
			element.SetImageEnabled(false);
		}
	}

	public void SetBlockVisible(List<MatrixVector> blockElementPositions, bool visible) {
		for (int i = 0; i < blockElementPositions.Count; i++) {
			var blockElementPosition = blockElementPositions[i];
			_gridElements[blockElementPosition.x, blockElementPosition.y].SetImageEnabled(visible);
		}
	}

	public void DestroyGrid() {
		foreach (GridElement element in _gridElements) {
			Destroy(element.gameObject);
		}
	}
}