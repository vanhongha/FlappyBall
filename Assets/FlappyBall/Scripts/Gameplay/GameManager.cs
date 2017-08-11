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

	private void Awake()
	{
		info = new GameInfo();
		info.gameHeight = Camera.main.orthographicSize * 2;
		info.gameWidth = info.gameHeight * Camera.main.aspect;
	}

	private void Start()
	{
		StartGame();
	}
	
	public void StartGame()
	{
		score = 0;
		ballManager.OnStart();
		nestManager.OnStart();
		state = GameState.PLAY;
	}

	[ContextMenu("Restart")]
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
	}

	public void AddScore()
	{
		score++;
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
