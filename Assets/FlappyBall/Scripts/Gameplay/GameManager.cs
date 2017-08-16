using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, PAUSE, END }
public class GameInfo
{
	public float gameWidth = 0f;
	public float gameHeight = 0f;
	public float canvasWidth = 0f;
	public float canvasHeight = 0f;
}

public class GameManager : MonoSingleton<GameManager> {

	public BallManager ballManager;
	public NestManager nestManager;
	public TouchScreen touchScreen;
	public GameObject ground;
	public GameObject border;
	public RectTransform canvas;

	public GameState state = GameState.PAUSE;
	[HideInInspector]
	public GameInfo info;
	public int score;
	[HideInInspector]
	public GamePlayDialog play;
	private bool end;

	private void Awake()
	{
		info = new GameInfo();
		info.gameHeight = Camera.main.orthographicSize * 2;
		info.gameWidth = info.gameHeight * Camera.main.aspect;
		info.canvasWidth = canvas.rect.width;
		info.canvasHeight = canvas.rect.height;

		GameStartDialog start = GUIManager.Instance.OnShowDialog<GameStartDialog>("Start");
	}
	
	public void StartGame()
	{
		end = false;
		score = 0;
		ballManager.OnStart();
		nestManager.OnStart();
		state = GameState.PLAY;
		//ground.SetActive(true);
		border.SetActive(true);
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
		if (state != GameState.END)
		{
			play.Lose();
			StartCoroutine(EndProcess(2f));
		}
	}

	IEnumerator EndProcess(float time)
	{
		state = GameState.END;
		yield return new WaitForSeconds(time);
		ballManager.OnEnd();
		nestManager.OnEnd();
		state = GameState.PAUSE;
		//ground.SetActive(false);
		border.SetActive(false);
		UserProfile.Instance.SetHighScore(score);
		UserProfile.Instance.AddDiamond(score * 10);
		play.GameOver(score);
	}

	public void AddScore()
	{
		if (state == GameState.PLAY)
		{
			score++;
			play.UpdateScore(score);
		}
	}
}
