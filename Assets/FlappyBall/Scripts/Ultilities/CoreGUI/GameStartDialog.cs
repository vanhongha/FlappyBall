using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using ChartboostSDK;

public class GameStartDialog : BaseDialog {

	public Text highScore;
	public Button noAdsButton;
	public Button soundButton;

	public void Awake()
	{
		if (!UserProfile.Instance.HasAds())
		{
			UserProfile.Instance.SetupNoAds(noAdsButton);
		}
		UserProfile.Instance.SetupSound(soundButton, 0);
	}

	public override void OnShow(Transform transf, object data)
	{
		base.OnShow(transf, data);
		highScore.text = UserProfile.Instance.GetHighScore().ToString();
		AdManager.Instance.ShowBanner();
	}

	public void OnClickPlay()
	{
		GUIManager.Instance.OnShowDialog<GamePlayDialog>("Play");
		OnCloseDialog();
	}

	public void OnClickSound()
	{
		SoundManager.Instance.ToggleMusic(!SoundManager.Instance.IsBackgroundPlaying());
		UserProfile.Instance.SetupSound(soundButton, 0);
	}

	public void OnClickStore()
	{
		StoreDialog store = GUIManager.Instance.OnShowDialog<StoreDialog>("Store");
		OnCloseDialog();
	}

	public void OnClickShare()
	{
		FBManager.Instance.ShareLink();
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
}