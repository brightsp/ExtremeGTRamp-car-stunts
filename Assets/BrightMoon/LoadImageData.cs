using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadImageData : MonoBehaviour {

	public static LoadImageData mee;
	// Use this for initialization

		public string url = "https://docs.unity3d.com/uploads/Main/ShadowIntro.png";
	void Start(){
		mee = this;
	}
	public void LoadImageNow(string NewUrl,Sprite SpriteObj){
		url = NewUrl;
		Debug.LogError(SpriteObj+"-----");

		StartCoroutine (StartData(SpriteObj));
	}
	public	IEnumerator StartData(Sprite SpriteObj)
		{

		Debug.LogError("start data "+url);
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


            if (JsonReader.IsNetAvailable)
            {
                GamePromotion.mee.ShowMenuAd();

            }
        }
		}


	
	// Update is called once per frame
	void Update () {
		
	}
}
