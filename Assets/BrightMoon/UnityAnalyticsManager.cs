using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UnityConsent;

public class UnityAnalyticsManager : MonoBehaviour
{
    public static UnityAnalyticsManager instance;
    private bool isInitialized = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isInitialized)
        {
            InitializeAnalytics();
        }
    }

    async void InitializeAnalytics()
    {
        await UnityServices.InitializeAsync();
        // AnalyticsService.Instance.StartDataCollection();

        // Initialize Unity Analytics
        UnityEngine.Analytics.Analytics.enabled = true;
        isInitialized = true;
        Debug.Log("Unity Analytics Initialized");

        EndUserConsent.SetConsentState(new ConsentState
        {
            AnalyticsIntent = ConsentStatus.Granted,
            AdsIntent = ConsentStatus.Denied
        });
    }


    //level_complete , level_fail ,level_start
    public void CustomEvent(string eventName, int currentLevel)
    {
        if (isInitialized)
        {
            Debug.Log("Sending Custom Event: " + eventName + " with currentLevel: " + currentLevel);
            CustomEvent myEvent = new CustomEvent(eventName)
            {
                { "userLevel", currentLevel }
            };
            AnalyticsService.Instance.RecordEvent(myEvent);
            //  AnalyticsService.Instance.Flush();
        }
        else
        {
            Debug.LogWarning("Unity Analytics is not initialized. Event not sent.");
        }
    }

    //noads,fullgame_inapps,rate_click
    public void CustomEvent(string eventName)
    {
        if (isInitialized)
        {

            AnalyticsService.Instance.RecordEvent(eventName);
            //  AnalyticsService.Instance.Flush();
        }
        else
        {
            Debug.LogWarning("Unity Analytics is not initialized. Event not sent.");
        }
    }

}
