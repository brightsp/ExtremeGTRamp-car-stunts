using UnityEngine;

public class SalePopupHandler : MonoBehaviour
{
    public GameObject Sale_Popup;
    public static int salePopupCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string isProUSer = PlayerPrefs.GetString("pro_USER");
        Debug.Log("Menu == isProUSer-> " + isProUSer + " ,isinSalePopup-> " + SKAds.isinSalePopup + " ,adsbuyed-> " + PlayerPrefs.GetString("adsbuyed"));

        if (salePopupCount == 0 && isProUSer == "no" && SKAds.isinSalePopup == "yes" && PlayerPrefs.GetString("adsbuyed") == "no")
        {
            Sale_Popup.SetActive(true);
        }

        salePopupCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
