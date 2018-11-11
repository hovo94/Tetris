using UnityEngine;

public class GameManager : Singleton<GameManager>{
	
	[SerializeField] private GridManager _gridManager;
	[SerializeField] private PopupsManager _popupsManager;
	[SerializeField] private EventSystemManager _eventSystemManager;
	[SerializeField] private ScreenManager _screnManager;
	[SerializeField] private int _columnsCount;
	[SerializeField] private int _rowsCount;

	public EventSystemManager EventSystemManager {
		get { return _eventSystemManager; }
	}

	private void StartGame() {
		_gridManager.Initialize(_columnsCount, _rowsCount,GameOver);
		_gridManager.CreateAndMoveBlock();
	}

	private void BackToMenu() {
		_popupsManager.Hide();
		_gridManager.DestroyGrid();
		_screnManager.HideScreen(()=>{ _screnManager.ShowMainScreen(PlayButtonListener);});
	}

	private void GameOver() {
		_popupsManager.ShowDialogPopup<GameOverPopup>(() => {
			_popupsManager.Hide(() => {
				_gridManager.ResetGrid();
				_gridManager.CreateAndMoveBlock();
			});
		},BackToMenu,_gridManager.Score.ToString());
	}

	private void PlayButtonListener() {
		_screnManager.HideScreen();
		_screnManager.ShowPlayScreen(_gridManager.MoveBlockLeft, _gridManager.MoveBlockRight,
			_gridManager.MoveBlockDown, _gridManager.RotateBlock,StartGame);
	}

	private void Start() {
		_screnManager.ShowMainScreen(PlayButtonListener);
		Debug.LogError("Use arrows to move in editor, and up arrow to ratate");
	}
}