using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    //==============================
    // Interstitial
    //==============================
    private InterstitialAd interstitialAd;

    //==============================
    // Banner
    //==============================
    private BannerView bannerView;

    //==============================
    // Rewarded
    //==============================
    private RewardedAd rewardedAd;

    private Action rewardAction;

    //==============================
    // Ad Unit IDs
    //==============================

    private string interstitialId =
        "ca-app-pub-4208468421244489/3650925807";

    private string bannerId =
        "ca-app-pub-4208468421244489/2580965251";

    private string rewardedId =
        "ca-app-pub-4208468421244489/3337276639";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        MobileAds.Initialize(initStatus =>
        {
            LoadBanner();
            LoadInterstitial();
            LoadRewardedAd();
        });
    }

    //=========================================================
    // Banner
    //=========================================================

    public void LoadBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        bannerView = new BannerView(
            bannerId,
            AdSize.Banner,
            AdPosition.Top);

        AdRequest request = new AdRequest();

        bannerView.LoadAd(request);
    }

    public void ShowBanner()
    {
        if (bannerView != null)
            bannerView.Show();
    }

    public void HideBanner()
    {
        if (bannerView != null)
            bannerView.Hide();
    }

    //=========================================================
    // Interstitial
    //=========================================================

    void LoadInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest request = new AdRequest();

        InterstitialAd.Load(interstitialId, request,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Interstitial failed to load.");
                    return;
                }

                interstitialAd = ad;

                interstitialAd.OnAdFullScreenContentClosed += () =>
                {
                    LoadInterstitial();
                    SceneManager.LoadScene(0);
                };
            });
    }

    public void ShowRetryAd()
    {
        if (interstitialAd != null &&
            interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    //=========================================================
    // Rewarded
    //=========================================================

    void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(rewardedId, request,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Rewarded Ad failed to load.");
                    return;
                }

                rewardedAd = ad;

                rewardedAd.OnAdFullScreenContentClosed += () =>
                {
                    LoadRewardedAd();
                };
            });
    }

    public void ShowRewardedAd(Action onReward)
    {
        rewardAction = onReward;

        if (rewardedAd != null &&
            rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                rewardAction?.Invoke();
            });
        }
        else
        {
            Debug.Log("Rewarded Ad not ready.");

            // Optional: remove this if you don't want to
            // revive the player when no ad is available.
            rewardAction?.Invoke();
        }
    }
}