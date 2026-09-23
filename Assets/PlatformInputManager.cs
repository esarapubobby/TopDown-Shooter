using UnityEngine;

public class PlatformInputManager : MonoBehaviour
{
    public static PlatformInputManager Instance;

    [Header("Mobile Scripts")]
    public PlayerController mobileController;
    public PlayerShoot mobileShoot;

    [Header("PC Scripts")]
    public PlayerController_PC pcController;
    public PlayerShoot_PC pcShoot;
    public MouseControlller mouseController;

    [Header("Mobile Control Panel")]
    public GameObject mobileControlPanel;

    public bool IsMobile { get; private set; }

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

        DetectPlatform();
    }

    void Start()
    {
        ApplyPlatform();
    }

    void DetectPlatform()
    {
        if (Application.isMobilePlatform)
        {
            IsMobile = true;
        }
        else
        {
            IsMobile = false;
        }
    }

    void ApplyPlatform()
    {
        if (IsMobile)
        {
            // MOBILE

            if (mobileController != null)
                mobileController.enabled = true;

            if (mobileShoot != null)
                mobileShoot.enabled = true;

            if (pcController != null)
                pcController.enabled = false;

            if (pcShoot != null)
                pcShoot.enabled = false;

            if (mouseController != null)
                mouseController.enabled = false;
        }
        else
        {
            // PC

            if (mobileController != null)
                mobileController.enabled = false;

            if (mobileShoot != null)
                mobileShoot.enabled = false;

            if (pcController != null)
                pcController.enabled = true;

            if (pcShoot != null)
                pcShoot.enabled = true;

            if (mouseController != null)
                mouseController.enabled = true;

            SetMobileControls(false);
        }
    }

    public void SetMobileControls(bool show)
    {
        if (mobileControlPanel == null)
        {
            Debug.LogError("Mobile Control Panel NOT ASSIGNED!");
            return;
        }

        // NEVER show mobile controls on PC
        if (!IsMobile)
        {
            mobileControlPanel.SetActive(false);
            return;
        }

        // MOBILE
        mobileControlPanel.SetActive(show);
    }

    public void EnableGameplay()
    {
        if (IsMobile)
        {
            // MOBILE GAMEPLAY

            if (mobileController != null)
                mobileController.enabled = true;

            if (mobileShoot != null)
                mobileShoot.enabled = true;

            if (pcController != null)
                pcController.enabled = false;

            if (pcShoot != null)
                pcShoot.enabled = false;

            if (mouseController != null)
                mouseController.enabled = false;

            SetMobileControls(true);
        }
        else
        {
            // PC GAMEPLAY

            if (mobileController != null)
                mobileController.enabled = false;

            if (mobileShoot != null)
                mobileShoot.enabled = false;

            if (pcController != null)
                pcController.enabled = true;

            if (pcShoot != null)
                pcShoot.enabled = true;

            if (mouseController != null)
                mouseController.enabled = true;

            SetMobileControls(false);
        }
    }

    public void DisableGameplay()
    {
        if (mobileController != null)
            mobileController.enabled = false;

        if (mobileShoot != null)
            mobileShoot.enabled = false;

        if (pcController != null)
            pcController.enabled = false;

        if (pcShoot != null)
            pcShoot.enabled = false;

        if (mouseController != null)
            mouseController.enabled = false;

        SetMobileControls(false);
    }
}