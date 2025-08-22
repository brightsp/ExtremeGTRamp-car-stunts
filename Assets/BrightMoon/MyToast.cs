using UnityEngine;
using System.Collections;
 
public class MyToast : MonoBehaviour {
	public static MyToast mee; 
	string toastString;
	AndroidJavaObject currentActivity;
	void Awake(){
		mee = this;
	}
	public void MyShowToastMethod (string _msg)
	{
		#if UNITY_EDITOR
		Debug.LogError("Toast : "+_msg);

		#endif
		#if !UNITY_EDITOR
		if (Application.platform == RuntimePlatform.Android) {
			showToastOnUiThread (_msg);
		}
		#endif
	}
	 
	void showToastOnUiThread(string toastString){
		AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		 
		currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		this.toastString = toastString;
		 
		currentActivity.Call ("runOnUiThread", new AndroidJavaRunnable (showToast));
	}
	 
	void showToast(){
		Debug.Log ("Running on UI thread");
		AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
		AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
		AndroidJavaObject javaString=new AndroidJavaObject("java.lang.String",toastString);
		AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
		toast.Call ("show");
	}


	public void OpenScene(string _scn){
		Application.LoadLevel (_scn);
	}

	 
}