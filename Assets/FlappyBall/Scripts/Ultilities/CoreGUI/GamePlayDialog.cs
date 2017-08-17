using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamePlayDialog : BaseDialog
{
	public Text score;
	public Text effectScore;
	public Text comment;
	public GameObject lose;
	public GameObject content;
	public GameObject highscoreCup;
	public BoxCollider2D boxCollider;
	protected bool highscore = false;

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		GameManager.Instance.play = this;
		boxCollider.size = new Vector2(GameManager.Instance.info.canvasWidth, boxCollider.size.y);
		content.transform.localPosition = Vector3.left * GameManager.Instance.info.canvasWidth * 1.5f;
		content.transform.DOLocalPath(new Vector3[] { Vector3.zero }, 0.75f)
			.OnComplete(() => { GameManager.Instance.StartGame(); });	
	}

	public void UpdateScore(int score)
	{
		this.score.text = score.ToString();

		this.effectScore.text = score.ToString();
		if (score == 0)
		{
			this.effectScore.text = "START";
		}
		else if (!highscore && score > UserProfile.Instance.GetHighScore())
		{
			highscore = true;
			this.effectScore.text = "BREAK";
			highscoreCup.SetActive(true);
			highscoreCup.transform.localScale = Vector3.one * 1.2f;
			highscoreCup.transform.localRotation = Quaternion.Euler(0, 0, -10f);
			highscoreCup.transform.DOShakeScale(1, 1f).OnComplete(delegate ()
			{
				highscoreCup.transform.DOLocalRotate(new Vector3(0, 0, 10f), 2f)
				.SetLoops(-1, LoopType.Yoyo);
			});
			SoundManager.Instance.PlaySfx(SFX.Highscore);
		}
		this.effectScore.transform.localScale = Vector2.one * 0.5f;
		this.effectScore.color = Color.white;
		this.effectScore.transform.DOScale(Vector2.one * 1f, 0.5f);
		this.effectScore.DOColor(new Color(1, 1, 1, 0), 0.5f);
		this.UpdateComment(score);
	}

	public void ScrollBackground(Vector2 offset, float speed = 0.2f)
	{
		//bg.material.mainTextureOffset += offset * Time.deltaTime * speed;;
	}

	public void ChangeBackgroundColor()
	{
		//bg.material.DOBlendableColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 5f);
	}

	public void Lose()
	{
		lose.transform.DOLocalPath(new Vector3[] { new Vector3(0, 0, 0) }, 0.5f);
	}

	public void GameOver(int score)
	{
		content.transform.DOLocalPath(new Vector3[] { content.transform.localPosition 
			+ Vector3.right * GameManager.Instance.info.canvasWidth * 1.5f }, 0.75f)
			.OnComplete(delegate() { OnCloseDialog(); });
		GUIManager.Instance.OnShowDialog<GameOverDialog>("Over", score);
	}

	public void UpdateComment(int score)
	{
		string text = "";
		if (score == 0)
		{
			text = "Hello there";
		}
		else if (score < 5)
		{
			GameManager.Instance.nestManager.moving = true;
			text = "Kid can do this";
		}
		else if (score < 10)
		{
			text = "Still long way";
		}
		else if (score < 15)
		{
			text = "Game kinda slow";
		}
		else if (score == 15)
		{
			text = "Let's pump up";
			GameManager.Instance.ballManager.ChangeSpeed(1.5f);
		}
		else if (score < 20)
		{
			text = "Now it's faster";
		}
		else if (score < 25)
		{
			text = "Still there ?";
		}
		else if (score < 30)
		{
			text = "Maybe this game is easy";
		}
		else if (score == 30)
		{
			text = "Or not";
			GameManager.Instance.ballManager.scaleForce = 1.25f;
		}
		else if (score < 35)
		{
			text = "Ball jump higher, right ?";
		}
		else if (score < 40)
		{
			text = "I know it hard to control";
		}
		else if (score < 45)
		{
			text = "You good, but..";
		}
		else if (score < 50)
		{
			text = "Beware the 50";
		}
		else if (score == 50)
		{
			text = "Faster";
			GameManager.Instance.ballManager.ChangeSpeed(1.55f);
		}
		else if (score == 51)
		{
			text = "Fasterr";
			GameManager.Instance.ballManager.ChangeSpeed(1.6f);
		}
		else if (score == 52)
		{
			text = "Fasterrr";
			GameManager.Instance.ballManager.ChangeSpeed(1.65f);
		}
		else if (score == 53)
		{
			text = "Fasterrrr";
			GameManager.Instance.ballManager.ChangeSpeed(1.7f);
		}
		else if (score == 54)
		{
			text = "Fasterrrrr";
			GameManager.Instance.ballManager.ChangeSpeed(1.75f);
		}
		else
		{
			text = "Feel like F1, hah ?";
		}

		if (score % 5 == 0 && score > 0)
		{
			ChangeBackgroundColor();
		}
		this.comment.text = text;
	}

	public void UpdateComment(string comment)
	{
		this.comment.text = comment;
	}

	public void OnClickPause()
	{
		if (GameManager.Instance.state == GameState.PLAY)
		{
			GameManager.Instance.PauseGame();
			GUIManager.Instance.OnShowDialog<PauseDialog>("Pause");
		}
	}
}

