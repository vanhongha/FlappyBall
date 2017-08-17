using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using ChartboostSDK;
using DG.Tweening;

public class GameStartDialog : BaseDialog {

	public Text gameName;
	public Text highScore;
	public Button noAdsButton;
	public Image ball;
	public GameObject buttons;
	public GameObject buttonPlay;
	public GameObject settings;

	protected virtual void Awake()
	{
		if (!UserProfile.Instance.HasAds())
		{
			noAdsButton.gameObject.SetActive(false);
		}
	}

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		gameName.text = UserProfile.Instance.gameName;
		highScore.text = UserProfile.Instance.GetHighScore().ToString();
		AdManager.Instance.ShowBanner();
		this.EffectShow();
	}

	public void UpdateBall()
	{
		if (ball != null)
		{
			ball.sprite = Storage.Instance.GetBallSprite(UserProfile.Instance.GetCurrentBall());
		}
	}

	public void Update()
	{
		if (ball != null)
		{
			ball.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 50f));
		}
	}

	public virtual void OnClickPlay()
	{
		this.EffectClose<GamePlayDialog>("Play");
	}

	public void OnClickStore()
	{
		this.EffectClose<StoreDialog>("Store");
	}

	public void OnClickShare()
	{
		if (checkClick)
		{
			return;
		}

		FBManager.Instance.ShareLink();
	}

	public void OnClickSettings()
	{
		settings.transform.DOLocalPath(new Vector3[] { settings.transform.localPosition + Vector3.up * 325f }, 0.5f);
		GUIManager.Instance.OnShowDialog<SettingDialog>("Settings", this);
	}

	public void HideSetting()
	{
		settings.transform.DOLocalPath(new Vector3[] { settings.transform.localPosition - Vector3.up * 325f }, 0.5f);
	}

	public void OnClickNoAds()
	{
		NotifyDialog notify = GUIManager.Instance.OnShowNotiFyDialog("Notify", NotifyType.NOADS, noAdsButton);
	}

	public void OnClickCBV()
	{
		if (Chartboost.hasInterstitial(CBLocation.HomeScreen))
		{
			highScore.text = "Video Cached...";
			Chartboost.showInterstitial(CBLocation.HomeScreen);
		}
		else
		{
			highScore.text = "Video Caching...";
			Chartboost.cacheInterstitial(CBLocation.HomeScreen);
		}
	}

	public void OnClickCBR()
	{
		if (Chartboost.hasRewardedVideo(CBLocation.HomeScreen))
		{
			highScore.text = "Reward Cached...";
			Chartboost.hasRewardedVideo(CBLocation.HomeScreen);
		}
		else
		{
			highScore.text = "Reward Caching...";
			Chartboost.cacheRewardedVideo(CBLocation.HomeScreen);
		}
	}

	public virtual void EffectClose<T>(string dialog) where T : BaseDialog
	{
		if (checkClick)
		{
			return;
		}
		checkClick = true;

		buttonPlay.transform.DOScale(0, 0.75f);
		gameName.transform.DOLocalPath(new Vector3[] { gameName.transform.localPosition + Vector3.up * 200 }, 0.75f);
		buttons.transform.DOLocalPath(new Vector3[] { buttons.transform.localPosition + Vector3.down * 200 }, 0.75f)
			.OnComplete(delegate { base.OnCloseDialog(); GUIManager.Instance.OnShowDialog<T>(dialog); });
	}

	public virtual void EffectShow()
	{
		UpdateBall();
		buttonPlay.transform.localScale = Vector3.zero;
		gameName.transform.localPosition += Vector3.up * 200;
		buttons.transform.localPosition += Vector3.down * 200;

		buttonPlay.transform.DOScale(1, 0.75f);
		gameName.transform.DOLocalPath(new Vector3[] { gameName.transform.localPosition - Vector3.up * 200 }, 0.75f);
		buttons.transform.DOLocalPath(new Vector3[] { buttons.transform.localPosition - Vector3.down * 200 }, 0.75f);
	}
}