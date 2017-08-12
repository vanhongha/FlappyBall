using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
using AudienceNetwork;
using GoogleMobileAds.Api;
using System;

public class AdManager : PluginSingleton<AdManager> {

	//----------------------------- FACEBOOK ---------------------------------//

	public string facebookID = "YOUR_PLACEMENT_ID";
	private AudienceNetwork.InterstitialAd interstitialAdFacebook;
	private AudienceNetwork.AdView adViewFacebook;
	private bool isFacebookLoaded;
	private bool isBannerFacebookLoaded;

	private void LoadBannerFacebook()
	{
#if UNITY_ANDROID || UNITY_IOS
		AudienceNetwork.AdView adView = new AudienceNetwork.AdView(facebookID, AudienceNetwork.AdSize.BANNER_HEIGHT_50);
		this.adViewFacebook = adView;
		this.adViewFacebook.Register(this.gameObject);
		this.adViewFacebook.AdViewDidLoad = (delegate ()
		{
			isBannerFacebookLoaded = true;
		});
		adView.LoadAd();
#endif
	}
	private void LoadInterstitialFacebook()
	{
#if UNITY_ANDROID || UNITY_IOS
		AudienceNetwork.InterstitialAd interstitialAd = new AudienceNetwork.InterstitialAd(facebookID);
		this.interstitialAdFacebook = interstitialAd;
		this.interstitialAdFacebook.Register(this.gameObject);
		this.interstitialAdFacebook.InterstitialAdDidLoad = (delegate ()
		{
			this.isFacebookLoaded = true;
		});
		this.interstitialAdFacebook.InterstitialAdDidClose = (delegate ()
		{
			this.isFacebookLoaded = false;
			this.interstitialAdFacebook.Dispose();
			this.LoadInterstitialFacebook();
		});
		this.interstitialAdFacebook.LoadAd();
#endif
	}
	private void ShowBannerFacebook()
	{
#if UNITY_ANDROID || UNITY_IOS
		if (this.isBannerFacebookLoaded)
		{
			this.adViewFacebook.Show(0);
		}
#endif
	}
	private void ShowInterstitialFacebook()
	{
#if UNITY_ANDROID || UNITY_IOS
		if (this.isFacebookLoaded)
		{
			this.interstitialAdFacebook.Show();
		}
		else
		{
			this.LoadInterstitialFacebook();
		}
#endif
	}
	private void InitFacebook()
	{
#if UNITY_EDITOR
#else
		LoadInterstitialFacebook();
		LoadBannerFacebook();
#endif
	}

	//-------------------------------- ADMOB -----------------------------------//

	[System.Serializable]
	public class AdmobID
	{
		public string banner = "YOUR_PLACEMENT_ID";
		public string instertitial = "YOUR_PLACEMENT_ID";
		public string rewardVideo = "YOUR_PLACEMENT_ID";
	}

	public AdmobID admobAndroid;
	public AdmobID admobIOS;
	private AdmobID admob;
	private bool isBannerAdmobLoaded;
	private GoogleMobileAds.Api.BannerView bannerView;
	private GoogleMobileAds.Api.InterstitialAd interstitial;
	private GoogleMobileAds.Api.RewardBasedVideoAd rewardVideo;

	private void LoadBannerAdmob()
	{
		this.bannerView = new GoogleMobileAds.Api.BannerView(admob.banner, GoogleMobileAds.Api.AdSize.SmartBanner, AdPosition.Top);
		this.bannerView.LoadAd(new AdRequest.Builder().Build());
		this.bannerView.OnAdLoaded += (delegate (System.Object sender, EventArgs args) {
			this.isBannerAdmobLoaded = true;
			this.bannerView.Hide();
		});
	}
	private void LoadInterstitialAdmob()
	{ 
		this.interstitial = new GoogleMobileAds.Api.InterstitialAd(admob.instertitial);
		this.interstitial.OnAdClosed += (delegate (System.Object sender, EventArgs args) {
			this.interstitial.LoadAd(new AdRequest.Builder().Build());
		});
		this.interstitial.LoadAd(new AdRequest.Builder().Build());
	}
	private void LoadRewardVideoAdmob()
	{
		this.rewardVideo = RewardBasedVideoAd.Instance;
		this.rewardVideo.OnAdClosed += (delegate (System.Object sender, EventArgs args) {
			this.rewardVideo.LoadAd(new AdRequest.Builder().Build(), admob.rewardVideo);
		});
		this.rewardVideo.LoadAd(new AdRequest.Builder().Build(), admob.rewardVideo);
	}
	private void ShowBannerAdmob()
	{
		if (isBannerAdmobLoaded)
		{
			this.bannerView.Show();
		}
	}
	private void ShowInterstitialAdmob()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}
	private void ShowRewardVideoAdmob()
	{
		if (this.rewardVideo.IsLoaded())
		{
			this.rewardVideo.Show();
		}
	}
	private void InitAdmob()
	{
#if UNITY_ANDROID
		this.admob = this.admobAndroid;
#elif UNITY_IOS
		this.admob = this.admobIOS;
#else
		this.admob = this.admobAndroid;
#endif
		LoadBannerAdmob();
		LoadInterstitialAdmob();
		LoadRewardVideoAdmob();
	}

	//------------------------------ CHARTBOOST --------------------------------//

	private void ShowInterstitialChartboost()
	{
		if (Chartboost.hasInterstitial(CBLocation.Default))
		{
			Chartboost.showInterstitial(CBLocation.Default);
		}
		else
		{
			Chartboost.cacheInterstitial(CBLocation.Default);
		}
	}
	private void ShowRewardVideoChartboost()
	{
		if (Chartboost.hasRewardedVideo(CBLocation.Default))
		{
			Chartboost.showRewardedVideo(CBLocation.Default);
		}
		else
		{
			Chartboost.cacheRewardedVideo(CBLocation.Default);
		}
	}
	private void InitChartboost()
	{
		Chartboost.cacheInterstitial(CBLocation.Default);
		Chartboost.cacheRewardedVideo(CBLocation.Default);
	}

	//---------------------------- MAIN FUNCTION -------------------------------//

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		this.InitFacebook();
		this.InitAdmob();
		this.InitChartboost();
	}

	public void ShowBanner()
	{
		if (isBannerFacebookLoaded)
		{
			ShowBannerFacebook();
		}
		else if (isBannerAdmobLoaded)
		{
			ShowBannerAdmob();
		}
	}
	public void ShowInterstitial()
	{
		if (isFacebookLoaded)
		{
			ShowInterstitialFacebook();
		}
		else if (interstitial.IsLoaded())
		{
			ShowInterstitialAdmob();
		}
		else if (Chartboost.hasInterstitial(CBLocation.Default))
		{
			Chartboost.showInterstitial(CBLocation.Default);
		}
	}
	public void ShowRewardVideo()
	{
		if (rewardVideo.IsLoaded())
		{
			ShowRewardVideoAdmob();
		}
		else if (Chartboost.hasRewardedVideo(CBLocation.Default))
		{
			ShowRewardVideoChartboost();
		}
	}
	public void HideBanner()
	{
		if (this.adViewFacebook != null)
		{
			this.adViewFacebook.Dispose();
		}
		if (this.bannerView != null)
		{
			this.bannerView.Destroy();
		}
	}
}
