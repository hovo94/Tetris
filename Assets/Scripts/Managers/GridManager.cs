using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour{
	
	private readonly Vector2Int _startPosition = new Vector2Int(4, 1);

	[SerializeField] private GridView _gridView;
	
	private int[,] _grid;
	private Block _currentBlock;

	private int _columnsCount;
	private int _rowsCount;
	
	public int Score { get; private set; }

	private Action _onGridFull;

	public void Initialize(int columnsCount, int rowsCount, Action onGridFull) {
		_grid = new int[columnsCount, rowsCount];
		_gridView.CreateGridView(columnsCount, rowsCount);
		_columnsCount = columnsCount;
		_rowsCount = rowsCount;
		_onGridFull += onGridFull;
	}
	
	public void CreateAndMoveBlock() {
		_currentBlock = CreateRandomBlock();
		_gridView.SetBlockVisible(_currentBlock.blockElementPositions, true);
		StartCoroutine(moveBlockDown());
	} 
	
	public void DestroyGrid() {
		_currentBlock = null;
		_grid = null;
		_gridView.ClearGrid();
		_gridView.DestroyGrid();
		Score = 0;
	}

	public void ResetGrid() {
		for (int x = 0; x < _columnsCount; x++) {
			for (int y = 0; y < _rowsCount; y++) {
				_grid[x, y] = 0;
				_gridView.SetGridElementEnabled(new MatrixVector(x, y),false);
			}
		}

		_currentBlock = null;
		Score = 0;
	}

	public void MoveBlockLeft() {
		MoveBlock(MatrixVector.left);
	}

	public void MoveBlockRight() {
		MoveBlock(MatrixVector.right);	
	}

	public void MoveBlockDown() {
		MoveBlock(MatrixVector.down);
	}
	
	public void RotateBlock() {

		if (!_currentBlock.Rotatable) {
			return;
		}

		List<MatrixVector> nextPositions = new List<MatrixVector>();
		MatrixVector offset = default(MatrixVector);

			bool canRotateAroundAnyElement = false;
			
			for (int i = 0; i < _currentBlock.blockElementPositions.Count; i++) {
				int xOffset = 0;
				int yOffset = 0;
				nextPositions.Clear();
				
				for (int j = 0; j < _currentBlock.blockElementPositions.Count; j++) {
			
					MatrixVector Vr = MatrixVector.Substract(_currentBlock.blockElementPositions[j], _currentBlock.blockElementPositions[i]);
					MatrixVector Vt = MatrixVector.RotateHalfPi(Vr);
					MatrixVector nextPosition = MatrixVector.Sum(_currentBlock.blockElementPositions[i], Vt);
			
					nextPositions.Add(nextPosition);
					
					if (nextPosition.x < 0 && nextPosition.x < xOffset) {
						xOffset = nextPosition.x;
					}

					if (nextPosition.x >= _columnsCount && nextPosition.x > xOffset) {
						xOffset = nextPosition.x - (_columnsCount - 1);
					}

					if (nextPosition.y < 0 && nextPosition.y < yOffset) {
						yOffset = nextPosition.y;
					}

					if (nextPosition.y >= _rowsCount && nextPosition.y > yOffset) {
						yOffset = nextPosition.y - (_rowsCount - 1);
					}	
				}

				foreach (MatrixVector position in _currentBlock.blockElementPositions) {
					_gridView.SetGridElementEnabled(position, true);
				}
				
				offset = new MatrixVector(-xOffset,-yOffset);

				if (IsPositionsAvailable(nextPositions, offset)) {
					canRotateAroundAnyElement = true;
					break;
				}
			}

			if (!canRotateAroundAnyElement) {
				return;
			}
		
		foreach (var blockPosition in _currentBlock.blockElementPositions) {
			_gridView.SetGridElementEnabled(blockPosition, false);
			_grid[blockPosition.x, blockPosition.y] = 0;
		}

		for (int i = 0; i < _currentBlock.blockElementPositions.Count; i++) {
			_currentBlock.blockElementPositions[i] = MatrixVector.Sum(nextPositions[i], offset);
			_gridView.SetGridElementEnabled(_currentBlock.blockElementPositions[i], true);
			_grid[_currentBlock.blockElementPositions[i].x, _currentBlock.blockElementPositions[i].y] = 1;
		}
	}

	private void MoveBlock(MatrixVector direction) {
		if (!IsPositionsAvailable(_currentBlock.blockElementPositions, direction)) {
			return;
		}
		
		for (int i = 0; i < _currentBlock.blockElementPositions.Count; i++) {
			_grid[_currentBlock.blockElementPositions[i].x, _currentBlock.blockElementPositions[i].y] = 0;
			_gridView.SetGridElementEnabled(_currentBlock.blockElementPositions[i], false);
			
			_currentBlock.blockElementPositions[i] =
				MatrixVector.Sum(_currentBlock.blockElementPositions[i], direction);
		}
		
		foreach (MatrixVector position in _currentBlock.blockElementPositions) {
			_grid[position.x, position.y] = 1;
			_gridView.SetGridElementEnabled(position, true);
		}
	}
	
	private void CheckAndRemoveRows() {
		for (int y = _rowsCount - 1; y > 0; y--) {
			if (IsRowFull(y)) {
				Score += _columnsCount;
				DeleteRow(y);
				ShiftRows(y);
				++y;
			}
		}
	}

	private void DeleteRow(int y) {
		for (int x = 0; x < _columnsCount; x++) {
			_grid[x, y] = 0;
		}
	}

	private void ShiftRows(int y) {
		for (int i = y - 1; i > 0; i--) {
			for (int x = 0; x < _columnsCount; x++) {
				_grid[x, i + 1] = _grid[x, i];
				_grid[x, i] = 0;
				
				_gridView.SetGridElementEnabled(new MatrixVector(x, i + 1), _grid[x, i + 1] == 1);
				_gridView.SetGridElementEnabled(new MatrixVector(x, i), false);
			}
		}
	}
	
	private bool IsRowFull(int y) {
		for (int x = 0; x < _columnsCount; x++) {
			if (_grid[x, y] == 0) {
				return false;
			}
		}

		return true;
	}
	
	private bool IsPositionsAvailable(List<MatrixVector> elementPositions, MatrixVector direction) {
		
		foreach (MatrixVector position in _currentBlock.blockElementPositions) {
			_grid[position.x, position.y] = 0;
		}
		
		foreach (MatrixVector position in elementPositions) {
				
			MatrixVector nextVector = MatrixVector.Sum(position, direction);
			
			bool overLowerBound = nextVector.y >= _rowsCount;
			bool overSideBounds = nextVector.x < 0 || nextVector.x >= _columnsCount;
				
			if (overLowerBound || overSideBounds) {
				foreach (MatrixVector p in _currentBlock.blockElementPositions) {
					_grid[p.x, p.y] = 1;
				}

				return false;
			}
			
			bool gridIsNotEmpty = _grid[nextVector.x, nextVector.y] == 1;
			
			if (gridIsNotEmpty) {
				foreach (MatrixVector p in _currentBlock.blockElementPositions) {
					_grid[p.x, p.y] = 1;
				}

				return false;
			}
		}

		return true;
	}
	
	private Block CreateRandomBlock() {
		int randomIndex = Random.Range(0, Config.BlockElementOffsets.Count);
		return CreateBlock((BlockType) randomIndex);
	}

	private Block CreateBlock(BlockType blockType) {
		
		Block block = new Block {
			blockType = blockType,
			blockElementPositions = new List<MatrixVector>()
		};
		
		block.blockElementPositions.Add(new MatrixVector(_startPosition.x,_startPosition.y));
		
		_grid[_startPosition.x,_startPosition.y] = 1;
		
		for (int i = 0; i < Config.BlockElementOffsets[block.blockType].Count; i++) {
			
			int x = _startPosition.x + Config.BlockElementOffsets[block.blockType][i].x;
			int y = _startPosition.y + Config.BlockElementOffsets[block.blockType][i].y;
			
			block.blockElementPositions.Add(new MatrixVector(x, y));
			
			_grid[x, y] = 1;
		}
		
		return block;
	}

	private IEnumerator moveBlockDown() {

		while (true) {

			yield return YieldInstructions.GravityForce;

			if (!IsPositionsAvailable(_currentBlock.blockElementPositions, MatrixVector.down)) {

				CheckAndRemoveRows();

				_currentBlock = CreateRandomBlock();

				if (IsPositionsAvailable(_currentBlock.blockElementPositions, MatrixVector.down)) {
					StartCoroutine(moveBlockDown());
					yield break;
				}

				_onGridFull();
				
				yield break;
			}

			MoveBlock(MatrixVector.down);
		}
	}

	private void Update() {
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			MoveBlock(MatrixVector.left);
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			MoveBlock(MatrixVector.right);
		}
		
		if (Input.GetKey(KeyCode.DownArrow)) {
			MoveBlock(MatrixVector.down);
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			RotateBlock();
		}
		
	}
}