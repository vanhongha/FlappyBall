using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayDialog : BaseDialog
{
	public Text score;

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		GameManager.Instance.play = this;
		GameManager.Instance.StartGame();
	}

	public void UpdateScore(int score)
	{
		this.score.text = score.ToString();
	}

	public void OnClickPause()
	{
		GameManager.Instance.PauseGame();
		GUIManager.Instance.OnShowDialog<PauseDialog>("Pause");
	}
}

