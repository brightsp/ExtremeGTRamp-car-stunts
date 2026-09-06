using UnityEngine;

public class RateManager : MonoBehaviour
{
    public static RateManager instance;
    public GameObject RatePopup_obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

    }

    public void showRatePopup()
    {
        if (PlayerPrefs.GetString("rated") == "false")
        {
            RatePopup_obj.SetActive(true);
        }
    }
    public void RatetheGame()
    {

        if (PlayerPrefs.GetString("rated") == "false")
        {
            PlayerPrefs.SetString("rated", "true");
            Application.OpenURL("market://details?id=" + Application.identifier);
        }
        else
        {
            Debug.Log("Already Rated");

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
