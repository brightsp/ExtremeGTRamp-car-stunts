using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LoadImageData : MonoBehaviour
{

	public static LoadImageData mee;
	// Use this for initialization
	public bool isNETAVAILABLE = false;

	public string url = "https://docs.unity3d.com/uploads/Main/ShadowIntro.png";
	void Start()
	{
		mee = this;

		StartCoroutine(CheckInternet((connected) =>
	{
		Debug.Log(connected ? "Internet Available" : "No Internet");

		isNETAVAILABLE = connected;
	}));
	}
	public void LoadImageNow(string NewUrl, Sprite SpriteObj)
	{
		url = NewUrl;
		Debug.LogError(SpriteObj + "----- Net? " + isNETAVAILABLE);
		if (isNETAVAILABLE)
		{
			StartCoroutine(StartData(SpriteObj));
		}
		else
		{
			SKAds.mee.logo.SetActive(false);
			Application.LoadLevel(1);

			SKAds.mee.StartIntilizeAds();
		}
	}
	public IEnumerator StartData(Sprite SpriteObj)
	{

		Debug.LogError("start data " + url);
		Texture2D tex;
		tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
		using (WWW www = new WWW(url))
		{
			yield return www;
			www.LoadImageIntoTexture(tex);
			//GetComponent<Renderer>().material.mainTexture = tex;

			//GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
			//Sprite aaSprite = new Sprite();
			Sprite aaSprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

			SKAds.MenuAd_Image = aaSprite;// GetComponent<Image> ().sprite;


			if (Application.internetReachability != NetworkReachability.NotReachable)
			{
				GamePromotion.mee.ShowMenuAd();

			}
			else
			{
				Debug.Log("Error. Check internet connection!");

			}
		}
	}


	public IEnumerator CheckInternet(System.Action<bool> callback)
	{
		using (UnityWebRequest request =
			   UnityWebRequest.Head("https://www.google.com"))
		{
			request.timeout = 5;

			yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
			bool connected = request.result == UnityWebRequest.Result.Success;
#else
            bool connected = !request.isNetworkError && !request.isHttpError;
#endif

			callback?.Invoke(connected);
		}
	}



	// Update is called once per frame
	void Update()
	{

	}
}
