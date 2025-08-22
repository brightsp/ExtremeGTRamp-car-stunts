using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;


public class GameAnalyticsData : MonoBehaviour
{
    [SerializeField]
    public enum GA_Events
    {
        car50D_Clicked,
        Car2_watch,
        Watchto_reset,
        Normal_reset,
        close_menuad,
        click_menuad,
        policy

    }

    public GA_Events Gevent;



    // Start is called before the first frame update
    void Start()
    {
      

    }
    public void RaiseEvent()
    {
      //  GameAnalytics.NewDesignEvent(Gevent.ToString());
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Undefined, Gevent.ToString(), 0, "", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
