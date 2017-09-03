using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoSingleton<UserProfile> {

	public string gameName;
	public string androidPath;
	public string iosPath;
    public int ballCount = 10;

	private string KEY_HIGH_SCORE = "KEY_HIGH_SCORE";
	private string KEY_DIAMOND = "KEY_DIAMOND";
	private string KEY_ADS = "KEY_ADS";
	private string KEY_BALLS = "KEY_BALLS";
	private string KEY_CURRENT_BALL = "KEY_CURRENT_BALL";

	private int highScore;
	private int diamond;
	private bool ads; // 0 = no ads, 1 = has ads
	private List<int> balls;
	private int currentBall;

	private void Awake()
	{
		this.LoadProfile();
	}

	// Save - load function
	public void LoadProfile()
	{
		// Init for first play
		this.diamond = 0;
		this.ads = true;
		this.highScore = 0;
		this.balls = new List<int>();
		for (int i = 0; i < ballCount; i++)
		{
			this.balls.Add(0);
		}
		this.balls[0] = 1;
		this.currentBall = 0;

		for (int i = 0; i < ballCount; i++)
		{
			if (PlayerPrefs.HasKey(KEY_BALLS + i))
			{
				this.balls[i] = PlayerPrefs.GetInt(KEY_BALLS + i);
			}
		}
		if (PlayerPrefs.HasKey(KEY_CURRENT_BALL))
		{
			this.currentBall = PlayerPrefs.GetInt(KEY_CURRENT_BALL);
		}

		// Init for second, third, ... play
		if (PlayerPrefs.HasKey(KEY_DIAMOND))
		{
			this.highScore = PlayerPrefs.GetInt(KEY_HIGH_SCORE);
		}
		if (PlayerPrefs.HasKey(KEY_DIAMOND))
		{
			this.diamond = PlayerPrefs.GetInt(KEY_DIAMOND);
		}
		if (PlayerPrefs.HasKey(KEY_ADS))
		{
			this.ads = PlayerPrefs.GetInt(KEY_ADS) == 1 ? true : false;
		}
	}
	public void SaveProfile()
	{
		PlayerPrefs.SetInt(KEY_DIAMOND, this.diamond);
		PlayerPrefs.SetInt(KEY_ADS, HasAds() ? 1 : 0);
		PlayerPrefs.SetInt(KEY_HIGH_SCORE, this.highScore);
		for (int i = 0; i < ballCount; i++)
		{
			PlayerPrefs.SetInt(KEY_BALLS + i, this.balls[i]);
		}
		PlayerPrefs.SetInt(KEY_CURRENT_BALL, this.currentBall);
	}
    [ContextMenu("Clear Data")]
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

	#region HIGHSCORE
	public bool IsHighScore(int newScore)
	{
		if (newScore > this.highScore)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public void SetHighScore(int newScore)
	{
		if (IsHighScore(newScore))
		{
			this.highScore = newScore;
			PlayerPrefs.SetInt(KEY_HIGH_SCORE, this.highScore);
		}
	}
	public int GetHighScore()
	{
		return this.highScore;
	}
	#endregion

	#region DIAMOND
	public void AddDiamond(int addedDiamond)
	{
		this.diamond += addedDiamond;
		PlayerPrefs.SetInt(KEY_DIAMOND, this.diamond);
	}
	public bool ReduceDiamond(int reducedDiamond)
	{
		int temp = this.diamond - reducedDiamond;
		if (temp >= 0)
		{
			this.diamond -= reducedDiamond;
			PlayerPrefs.SetInt(KEY_DIAMOND, this.diamond);
			return true;
		}
		else
		{
			return false;
		}
	}
	public int GetDiamond()
	{
		return this.diamond;
	}
	[ContextMenu("Clear Diamond - test only")]
	public void ClearDiamond()
	{
		ReduceDiamond(GetDiamond());
	}
	#endregion

	#region ADS
	public void RemoveAds()
	{
		if (HasAds())
		{
			this.ads = false;
			AdManager.Instance.RemoveBanner();
			PlayerPrefs.SetInt(KEY_ADS, HasAds() ? 1 : 0);
		}
	}
	public bool HasAds()
	{
		return this.ads;
	}
	#endregion

	#region BALL
	public int GetCurrentBall()
	{
		return this.currentBall;
	}
	public void SetCurrentBall(int index)
	{
		if (index >= 0 && index < this.balls.Count)
		{
			this.currentBall = index;
			PlayerPrefs.SetInt(KEY_CURRENT_BALL, index);
		}
	}
	public void SetBallAvaiable(int index)
	{
		if (index >= 0 && index < this.balls.Count)
		{
			this.balls[index] = 1;
			PlayerPrefs.SetInt(KEY_BALLS + index, this.balls[index]);
		}
	}
	public bool IsBallAvailable(int index)
	{
		if (index >= 0 && index < this.balls.Count && this.balls[index] == 1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	#endregion
}
