using UnityEngine;

public class GameManager : Singleton<GameManager> {
	
	[SerializeField] private GridManager _gridManager;
	[SerializeField] private PopupsManager _popupsManager;
	[SerializeField] private EventSystemManager _eventSystemManager;
	[SerializeField] private ScreenManager _screnManager;
	[SerializeField] private int _columnsCount;
	[SerializeField] private int _rowsCount;
	
	private void StartGame() {
		_gridManager.CreateGrid(_columnsCount, _rowsCount);
		_gridManager.CreateAndMoveBlock();
	}
	
	private void BackToMenu() {
		_gridManager.DestroyGrid();
		_screnManager.HidePlayScreen();
		_screnManager.ShowMainScreen(PlayButtonListener);
	}
	
	private void RestartGame() {
		_gridManager.ResetGrid();
		_gridManager.CreateAndMoveBlock();
	}
	
	private void GameOver() {
		_popupsManager.ShowDialogPopup(RestartGame, BackToMenu, _gridManager.Score.ToString(), "Retry", "Go To Menu");
	}

	private void PlayButtonListener() {
		_screnManager.HideMainScreen();
		_screnManager.ShowPlayScreen(_gridManager.MoveBlockLeft, _gridManager.MoveBlockRight,
			_gridManager.MoveBlockDown, _gridManager.RotateBlock, null, StartGame);
	}

	private void InitializeManagers() {
		_popupsManager.Initialize(_eventSystemManager);
		_gridManager.Initialize(GameOver,_screnManager.GridView);
	}

	private void Start() {
		InitializeManagers();
		_screnManager.ShowMainScreen(PlayButtonListener);
	}
}