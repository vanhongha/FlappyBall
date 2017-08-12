using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class GameOverDialog : GameStartDialog {

	public Text scoreText;
	public Text addedDiamond;

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		int score = (int)data;
		scoreText.text = score.ToString();
		addedDiamond.text = "+" + (score * 10).ToString();
		int rand = Random.Range(0, 5);
		if (rand == 0)
		{
			AdManager.Instance.ShowInterstitial();
		}
	}
}
