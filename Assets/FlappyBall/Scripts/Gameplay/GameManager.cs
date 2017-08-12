using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, PAUSE }
public class GameInfo
{
	public float gameWidth = 0f;
	public float gameHeight = 0f;
}

public class GameManager : MonoSingleton<GameManager> {

	public BallManager ballManager;
	public NestManager nestManager;
	public TouchScreen touchScreen;
	public GameState state = GameState.PAUSE;
	public GameInfo info;
	public int score;
	[HideInInspector]
	public GamePlayDialog play;

	private void Awake()
	{
		info = new GameInfo();
		info.gameHeight = Camera.main.orthographicSize * 2;
		info.gameWidth = info.gameHeight * Camera.main.aspect;
		GameStartDialog start = GUIManager.Instance.OnShowDialog<GameStartDialog>("Start");
	}
	
	public void StartGame()
	{
		score = 0;
		ballManager.OnStart();
		nestManager.OnStart();
		state = GameState.PLAY;
		play.UpdateScore(score);
	}
	
	public void RestartGame()
	{
		EndGame();
		StartGame();
	}

	public void PauseGame()
	{
		ballManager.OnPause();
		state = GameState.PAUSE;
	}

	public void ContinueGame()
	{
		ballManager.OnContinue();
		state = GameState.PLAY;
	}

	public void EndGame()
	{
		ballManager.OnEnd();
		nestManager.OnEnd();
		state = GameState.PAUSE;

		GUIManager.Instance.OnHideAllDialog();
		GameOverDialog over = GUIManager.Instance.OnShowDialog<GameOverDialog>("Over", score);
	}

	public void AddScore()
	{
		score++;
		play.UpdateScore(score);
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (state != GameState.PAUSE)
			{
				PauseGame();
			}
			else
			{
				ContinueGame();
			}
		}
	}
}
