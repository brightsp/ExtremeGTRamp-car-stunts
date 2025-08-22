using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Settingspage : MonoBehaviour {

	// Use this for initialization
	public static Settingspage Obj;
	public static bool Ismute=false;
	void Awake()
	{
		Obj = this;
		gameObject.SetActive (false);
	}

	public void ShowSettingsPage()
	{
		gameObject.SetActive (true);
		CheckCash ();

		if (!Ismute)
		{
			soundoff.GetComponent<Button>().gameObject.SetActive(true);
			soundon.GetComponent<Button>().gameObject.SetActive(false);
		}

		else
		{
			soundon.GetComponent<Button>().gameObject.SetActive(true);
			soundoff.GetComponent<Button>().gameObject.SetActive(false);
		}
	}

	public void ShowMenupage()
	{

		Invoke ("Hidepage", 0.5f);
		Menupage.ShowFadeimage ();
		Menupage.Obj.ShowMenupagenow ();
	}


	void Hidepage()
	{
		gameObject.SetActive(false);
	}


	public Text Cashtext;

	void CheckCash()
	{
		Cashtext.text = Menupage.Getcash ().ToString ();
	}

	public GameObject soundon, soundoff;
	public void Soundonclicked()
	{
		soundon.GetComponent<Button>().gameObject.SetActive (false);
		soundoff.GetComponent<Button>().gameObject.SetActive (false);
		AudioListener.volume = 1;
		Ismute = false;
		soundoff.GetComponent<Button>().gameObject.SetActive (true);

	}

	public void Soundoffclicked()
	{
		soundon.GetComponent<Button>().gameObject.SetActive(false);
		soundoff.GetComponent<Button>().gameObject.SetActive (false);
		AudioListener.volume =0;

		Ismute = true;
		soundon.GetComponent<Button>().gameObject.SetActive (true);

	}

}
