using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using DG.Tweening;

public class GameOverDialog : GameStartDialog {

	public Text scoreText;
	public Text addedDiamond;
	public GameObject content;

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

	public override void EffectClose<T>(string dialog)
	{
		if (checkClick)
		{
			return;
		}
		checkClick = true;

		if (typeof(T).Equals(typeof(StoreDialog)))
		{
			content.transform.DOLocalPath( new Vector3[] {
				transform.localPosition + Vector3.up * GameManager.Instance.info.canvasHeight }, 0.75f)
				.OnComplete(delegate { base.OnCloseDialog(); });
			GUIManager.Instance.OnShowDialog<T>(dialog);
		}

		else if (typeof(T).Equals(typeof(GamePlayDialog)))
		{
			content.transform.DOLocalPath(new Vector3[] {
				transform.localPosition + Vector3.right * GameManager.Instance.info.canvasWidth * 1.5f }, 0.75f)
				.OnComplete(delegate { base.OnCloseDialog(); });
			GUIManager.Instance.OnShowDialog<T>(dialog);
		}
	}

	public override void EffectShow()
	{
		content.transform.localPosition = Vector3.left * 1200;
		content.transform.DOLocalPath(new Vector3[] { Vector3.zero }, 0.75f);
	}

    public override void OnClickPlay()
    {
        checkClick = true;
        buttonPlay.GetComponent<Button>().interactable = false;
        buttonPlay.transform.DOLocalRotate(new Vector3(0, 0, -180), 0.5f)
            .OnComplete(() => { checkClick = false; base.OnClickPlay(); });
    }
}
