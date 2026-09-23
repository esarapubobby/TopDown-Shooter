using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#if !UNITY_WEBGL
using GoogleMobileAds.Api;
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
using CrazyGames;
#endif

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private Action rewardAction;
    private static int RetryCount = 0;

#if !UNITY_WEBGL

    private InterstitialAd interstitialAd;
    private BannerView bannerView;
    private RewardedAd rewardedAd;

    private string interstitialId =
        "ca-app-pub-9565881819222312/2316581261";

    private string bannerId =
        "ca-app-pub-9565881819222312/5134316292";

    private string rewardedId =
        "ca-app-pub-9565881819222312/8690417926";

#endif

#if UNITY_WEBGL && !UNITY_EDITOR

    private bool crazyGamesInitialized = false;

#endif

    // =========================================
    // PLATFORM
    // =========================================

    private bool IsCrazyGames()
    {
#if UNITY_WEBGL && !UNITY_EDITOR

        string url = Application.absoluteURL;

        if (!string.IsNullOrEmpty(url))
        {
            if (url.Contains("isCrazyGames=true"))
                return true;

            if (url.Contains("crazygames.com") ||
                url.Contains("crazygames."))
                return true;
        }

        return CrazySDK.IsAvailable;

#else

        return false;

#endif
    }

    private bool IsMobileAdPlatform()
    {
#if UNITY_ANDROID || UNITY_IOS

        return true;

#else

        return false;

#endif
    }

    // =========================================
    // AWAKE
    // =========================================

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

#if UNITY_WEBGL && !UNITY_EDITOR

        if (IsCrazyGames())
        {
            CrazySDK.Init(() =>
            {
                crazyGamesInitialized = true;
                Debug.Log("CrazyGames SDK initialized.");
            });

            return;
        }

        Debug.Log("WebGL outside CrazyGames. Ads disabled.");

#elif UNITY_ANDROID || UNITY_IOS

        MobileAds.Initialize(initStatus =>
        {
            LoadInterstitial();
            LoadRewardedAd();
        });

#else

        Debug.Log("Ads disabled in this platform.");

#endif
    }

    // =========================================
    // BANNER
    // =========================================

    public void LoadBanner()
    {
#if !UNITY_WEBGL

        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

        bannerView = new BannerView(
            bannerId,
            AdSize.Banner,
            AdPosition.Top
        );

        AdRequest request = new AdRequest();

        bannerView.LoadAd(request);

#endif
    }

    public void ShowBanner()
    {
#if !UNITY_WEBGL

        if (bannerView == null)
        {
            LoadBanner();
            return;
        }

        bannerView.Show();

#endif
    }

    public void HideBanner()
    {
#if !UNITY_WEBGL

        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

#endif
    }

    // =========================================
    // INTERSTITIAL
    // =========================================

#if !UNITY_WEBGL

    void LoadInterstitial()
    {
        if (!IsMobileAdPlatform())
            return;

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest request = new AdRequest();

        InterstitialAd.Load(
            interstitialId,
            request,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Interstitial failed to load.");
                    return;
                }

                interstitialAd = ad;

                ad.OnAdFullScreenContentClosed += () =>
                {
                    LoadInterstitial();

                    SceneManager.LoadScene(0);
                };

                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    LoadInterstitial();

                    SceneManager.LoadScene(0);
                };
            }
        );
    }

#endif

    // =========================================
    // RETRY AD
    // =========================================

    public void ShowRetryAd()
    {
        RetryCount++;

        // Show ad every 4th retry
        if (RetryCount % 4 != 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

#if UNITY_WEBGL && !UNITY_EDITOR

        if (IsCrazyGames())
        {
            if (!crazyGamesInitialized)
            {
                SceneManager.LoadScene(0);
                return;
            }

            CrazySDK.Ad.RequestAd(
                CrazyAdType.Midgame,

                // AD STARTED
                () =>
                {
                },

                // AD ERROR
                error =>
                {
                    SceneManager.LoadScene(0);
                },

                // AD FINISHED
                () =>
                {
                    SceneManager.LoadScene(0);
                }
            );

            return;
        }

        // WebGL but not CrazyGames
        SceneManager.LoadScene(0);
        return;

#endif

#if UNITY_ANDROID || UNITY_IOS

        if (interstitialAd != null &&
            interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            SceneManager.LoadScene(0);
        }

        return;

#endif

        SceneManager.LoadScene(0);
    }

    // =========================================
    // REWARDED LOAD
    // =========================================

#if !UNITY_WEBGL

    void LoadRewardedAd()
    {
        if (!IsMobileAdPlatform())
            return;

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(
            rewardedId,
            request,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Rewarded failed to load.");
                    return;
                }

                rewardedAd = ad;

                ad.OnAdFullScreenContentClosed += () =>
                {
                    rewardAction?.Invoke();

                    rewardAction = null;

                    LoadRewardedAd();
                };

                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    rewardAction = null;

                    LoadRewardedAd();
                };
            }
        );
    }

#endif

    // =========================================
    // REWARDED AD
    // =========================================

    public void ShowRewardedAd(Action onReward)
    {
        rewardAction = onReward;

#if UNITY_WEBGL && !UNITY_EDITOR

        if (IsCrazyGames())
        {
            if (!crazyGamesInitialized)
            {
                rewardAction = null;

                UiManager ui = FindObjectOfType<UiManager>();

                if (ui != null)
                    ui.ShowNoInternetMessage();

                return;
            }

            CrazySDK.Ad.RequestAd(
                CrazyAdType.Rewarded,

                // AD STARTED
                () =>
                {
                },

                // AD ERROR
                error =>
                {
                    rewardAction = null;

                    UiManager ui =
                        FindObjectOfType<UiManager>();

                    if (ui != null)
                        ui.ShowNoInternetMessage();
                },

                // AD FINISHED
                () =>
                {
                    rewardAction?.Invoke();

                    rewardAction = null;
                }
            );

            return;
        }

        // WebGL outside CrazyGames
        rewardAction = null;

        UiManager webGLUI =
            FindObjectOfType<UiManager>();

        if (webGLUI != null)
            webGLUI.ShowNoInternetMessage();

        return;

#endif

#if UNITY_ANDROID || UNITY_IOS

        if (rewardedAd != null &&
            rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // Reward is given after the ad closes
            });
        }
        else
        {
            rewardAction = null;

            UiManager ui =
                FindObjectOfType<UiManager>();

            if (ui != null)
                ui.ShowNoInternetMessage();
        }

        return;

#endif

        rewardAction = null;
    }
}