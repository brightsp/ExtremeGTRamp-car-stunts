using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Storepage : MonoBehaviour {

	// Use this for initialization


	public static Storepage Obj;
	void Awake()
	{
		Obj = this;
		gameObject.SetActive (false);
	}

	public void ShowStorepage()
	{
		gameObject.SetActive (true);
		CheckCash ();
	}
	void Start () {
		
	}
	
	public Text Cashtext;

	public void CheckCash()
	{
		Cashtext.text = Menupage.Getcash ().ToString ();
	}

	void Hidepage()
	{
		gameObject.SetActive (false);
	}

	public void ShowUpgradepage()
	{

		Invoke ("Hidepage", 0.5f);
		Menupage.ShowFadeimage ();
		Upgradepage.Obj.Invoke("ShowUpgradepagenow",0.5f);
	}

    public void Restorebtnclicked()
    {
        //sr InAppController.instance.RestorePurchases();
    }
}
